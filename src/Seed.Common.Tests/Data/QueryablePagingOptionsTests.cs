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

            for (var i = 0; i < 107; i++)
            {
                items.Add(new Item(i + 1));
            }

            return items.AsQueryable();
        }

        [Fact]
        public async Task ToPagedResultAsync_RequestingFirstPage_ReturnsFirstPageOfResults()
        {
            var items = GenerateItems(107);

            var pagingOptions = new PagingOptionsInputModel
            {
                PageNumber = 1,
                PageSize = 20
            };

            var result = await items.ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            Assert.Equal(1, result.Items[0].Id);
            Assert.Equal(20, result.Items.Last().Id);
        }
    }
}
