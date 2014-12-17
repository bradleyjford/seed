using System;
using Seed.Common.Domain;

namespace Seed.Common.Tests.Data
{
    public class Item : Entity<int>
    {
        public Item(int id)
        {
            Id = id;
        }

        public string Name { get; set; }
    }
}
