using System;
using System.Text;
using Seed.Common.Text;
using Xunit;

namespace Seed.Common.Tests.Text
{
    public class Base32EncodingTests
    {
        private string Encode(string value)
        {
            return Base32Encoding.Encode(Encoding.ASCII.GetBytes(value));
        }

        private string Decode(string value)
        {
            return Encoding.ASCII.GetString(Base32Encoding.Decode(value));
        }

        [Fact]
        public void TestEncodeEmptyString()
        {
            Assert.Equal("", Encode(""));
        }

        [Fact]
        public void TestEncodeSingleCharacter()
        {
            Assert.Equal("MY======", Encode("f"));
        }

        [Fact]
        public void TestEncodeTwoCharacters()
        {
            Assert.Equal("MZXQ====", Encode("fo"));
        }

        [Fact]
        public void TestEncodeThreeCharacters()
        {
            Assert.Equal("MZXW6===", Encode("foo"));
        }

        [Fact]
        public void TestEncodeFourCharacters()
        {
            Assert.Equal("MZXW6YQ=", Encode("foob"));
        }

        [Fact]
        public void TestEncodeFiveCharacters()
        {
            Assert.Equal("MZXW6YTB", Encode("fooba"));
        }

        [Fact]
        public void TestEncodeSixCharacters()
        {
            Assert.Equal("MZXW6YTBOI======", Encode("foobar"));
        }

        [Fact]
        public void TestDecodeEmptyString()
        {
            Assert.Equal("", Decode(""));
        }

        [Fact]
        public void TestDecodeSingleCharacter()
        {
            Assert.Equal("f", Decode("MY======"));
        }

        [Fact]
        public void TestDecodeTwoCharacters()
        {
            Assert.Equal("fo", Decode("MZXQ===="));
        }

        [Fact]
        public void TestDecodeThreeCharacters()
        {
            Assert.Equal("foo", Decode("MZXW6==="));
        }

        [Fact]
        public void TestDecodeFourCharacters()
        {
            Assert.Equal("foob", Decode("MZXW6YQ="));
        }

        [Fact]
        public void TestDecodeFiveCharacters()
        {
            Assert.Equal("fooba", Decode("MZXW6YTB"));
        }

        [Fact]
        public void TestDecodeSixCharacters()
        {
            Assert.Equal("foobar", Decode("MZXW6YTBOI======"));
        }
    }
}
