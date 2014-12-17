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

        public Item(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
    }
}
