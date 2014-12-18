using System;
using System.Linq;
using System.Threading.Tasks;
using Seed.Common.Data;
using Xunit;

namespace Seed.Common.Tests.Data
{
    public class QueryablePagingOptionsTests
    {
        private IQueryable<Item> GenerateItems(int count)
        {
            var dbContext = new TestDbContext();

            var items = dbContext.Items;

            for (var i = 0; i < count; i++)
            {
                items.Add(new Item(i + 1, String.Format("Item  {0}", count - i)));
            }

            return items.AsQueryable();
        }

        [Fact]
        public async Task ToPagedResultAsync_ListOf100ItemsWithAPageSizeOf20_ResultsIn5Pages()
        {
            var items = GenerateItems(100);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 20
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(5, result.PageCount);
        }

        [Fact]
        public async Task ToPagedResultAsync_ListOf101ItemsWithAPageSizeOf20_ResultsIn6Pages()
        {
            var items = GenerateItems(101);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 20
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(6, result.PageCount);
        }

        [Fact]
        public async Task ToPagedResultAsync_RequestingFirstPage_ReturnsExpectedItemsOnPage()
        {
            var items = GenerateItems(107);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 20
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(1, result.Items[0].Id);
            Assert.Equal(20, result.Items.Last().Id);
        } 
        
        [Fact]
        public async Task ToPagedResultAsync_RequestingSecondPage_ReturnsExpectedItemsOnPage()
        {
            var items = GenerateItems(107);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 2,
                PageSize = 20
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(21, result.Items[0].Id);
            Assert.Equal(40, result.Items.Last().Id);
        }

        [Fact]
        public async Task ToPagedResultAsync_RequestingLastPage_ReturnsExpectedItemsOnPage()
        {
            var items = GenerateItems(107);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 6,
                PageSize = 20
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(101, result.Items[0].Id);
            Assert.Equal(107, result.Items.Last().Id);
        }

        [Fact]
        public async Task ToPagedResultAsync_RequestingAPageNumberLessThan1_ReturnsTheFirstPage()
        {
            var items = GenerateItems(107);

            var pagingOptions = new PagingOptions
            {
                PageNumber = -4,
                PageSize = 20
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(1, result.Items[0].Id);
            Assert.Equal(20, result.Items.Last().Id);
        }

        [Fact]
        public async Task ToPagedResultAsync_RequestingAPageNumberThatDoesntExist_ReturnsNoResults()
        {
            var items = GenerateItems(107);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 100,
                PageSize = 20
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.False(result.Items.Any());
        }

        [Fact]
        public async Task ToPagedResultAsync_RequestingPageSizeThatEqualsNumberOfResults_ReturnsAllItems()
        {
            var items = GenerateItems(100);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 100
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(1, result.PageCount);
            Assert.Equal(100, result.Items.Count);
        }

        [Fact]
        public async Task ToPagedResultAsync_OrderingByIdAscending_ReturnsResultsOrderedByIdAscending()
        {
            var items = GenerateItems(107);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 100,
                SortOrder = "Id asc"
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(1, result.Items[0].Id);
            Assert.Equal(100, result.Items.Last().Id);
        }

        [Fact]
        public async Task ToPagedResultAsync_OrderingByIdDescending_ReturnsResultsOrderedByIdDescending()
        {
            var items = GenerateItems(107);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 100,
                SortOrder = "Id desc"
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(107, result.Items[0].Id);
            Assert.Equal(8, result.Items.Last().Id);
        }
    }
}
