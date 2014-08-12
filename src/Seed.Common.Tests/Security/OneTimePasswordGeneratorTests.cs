using System;
using System.Text;
using Seed.Common.Security;
using Xunit;
using Xunit.Extensions;

namespace Seed.Common.Tests.Security
{
    public class OneTimePasswordGeneratorTests
    {
        [Theory]
        [InlineData(0, "755224")]
        [InlineData(1, "287082")]
        [InlineData(2, "359152")]
        [InlineData(3, "969429")]
        [InlineData(4, "338314")]
        [InlineData(5, "254676")]
        [InlineData(6, "287922")]
        [InlineData(7, "162583")]
        [InlineData(8, "399871")]
        [InlineData(9, "520489")]
        public void CanGenerateOneTimePasswords(long sequence, string expected)
        {
            var secret = Encoding.ASCII.GetBytes("12345678901234567890");

            var generator = new OneTimePasswordGenerator(6);

            Assert.Equal(expected, generator.Generate(secret, sequence));
        }
    }
}
