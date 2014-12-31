using System;

namespace Seed.Common.Domain
{
    public class GuidCombIdGenerator
    {
        private static readonly DateTime _baseDate = new DateTime(1900, 1, 1);

        /// <remarks>
        /// https://github.com/nhibernate/nhibernate-core/blob/master/src/NHibernate/Id/GuidCombGenerator.cs
        /// </remarks>>
        public static Guid GenerateId()
        {
            var guidArray = Guid.NewGuid().ToByteArray();

            var now = DateTime.UtcNow;

            // Get the days and milliseconds which will be used to build the byte string 
            var days = (now - _baseDate).Days;
            var msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            var daysArray = BitConverter.GetBytes(days);
            var msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(daysArray);
                Array.Reverse(msecsArray);
            }

            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }
    }
}
