using System;
using System.Text;
using Seed.Common.Security;
using Xunit;
using Xunit.Extensions;

namespace Seed.Common.Tests.Security
{
    public class TimeBasedOneTimePasswordGeneratorTests
    {
        // As documented in RFC6238 - http://tools.ietf.org/html/rfc6238
        public static object[] TestCases 
        {
            get
            {
                return new[]
                {
                    new object[] { new DateTime(1970, 1, 1, 0, 0, 59), "94287082" },
                    new object[] { new DateTime(2005, 3, 18, 1, 58, 29), "07081804" },
                    new object[] { new DateTime(2005, 3, 18, 1, 58, 31), "14050471" },
                    new object[] { new DateTime(2009, 2, 13, 23, 31, 30), "89005924" },
                    new object[] { new DateTime(2033, 05, 18, 3, 33, 20), "69279037" },
                    new object[] { new DateTime(2603, 10, 11, 11, 33, 20), "65353130" }
                };
            }
        }

        [Theory]
        [PropertyData("TestCases")]
        public void TestOneTimePassword(DateTime dateTime, string expected)
        {
            var secret = Encoding.ASCII.GetBytes("12345678901234567890");

            var generator = new TimeBasedOneTimePasswordGenerator(8);

            Assert.Equal(expected, generator.Generate(secret, dateTime));
        }
    }
}
