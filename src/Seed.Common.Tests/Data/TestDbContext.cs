using System;
using System.Data.Entity;
using Seed.Common.Data.Testing;

namespace Seed.Common.Tests.Data
{
    public class TestDbContext
    {
        public TestDbContext()
        {
            Items = new TestEntityDbSet<Item, int>();

        }

        public DbSet<Item> Items { get; private set; }
    }
}
