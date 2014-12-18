using System;
using System.Linq;
using Seed.Common.Data;
using Xunit;

namespace Seed.Common.Tests.Data
{
    public class SortDescriptorParserTests
    {
        [Fact]
        public void Parse_NoSortParameters_ReturnsNoSortDescriptors()
        {
            var result = SortDescriptorParser.Parse(String.Empty);

            Assert.False(result.Any());
        }

        [Fact]
        public void Parse_SingleSortParameters_ReturnsOneSortDescriptor()
        {
            var result = SortDescriptorParser.Parse("Name desc");   

            Assert.Equal(1, result.Count());

            Assert.Equal("Name", result.Single().PropertyName);
            Assert.Equal(SortDirection.Descending, result.Single().Direction);
        }

        [Fact]
        public void Parse_TwoSortParameters_ReturnsTwoSortDescriptors()
        {
            var result = SortDescriptorParser.Parse("Name desc, Id asc");

            Assert.Equal(2, result.Count());

            Assert.Equal("Name", result.First().PropertyName);
            Assert.Equal(SortDirection.Descending, result.First().Direction);

            Assert.Equal("Id", result.Last().PropertyName);
            Assert.Equal(SortDirection.Ascending, result.Last().Direction);
        }

        [Fact]
        public void Parse_OmmittingSortDirection_SortsAscendinglyByDefault()
        {
            var result = SortDescriptorParser.Parse("Name");

            Assert.Equal(SortDirection.Ascending, result.First().Direction);
        }

        [Fact]
        public void Parse_OmmittingSortDirectionWithTwoSortParameters_SortsAscendinglyByDefault()
        {
            var result = SortDescriptorParser.Parse("Name, Id");

            Assert.Equal(2, result.Count());

            Assert.Equal(SortDirection.Ascending, result.First().Direction);
            Assert.Equal(SortDirection.Ascending, result.Last().Direction);
            
        }
    }
}
