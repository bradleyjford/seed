using System;
using System.Linq;
using Seed.Common.Data;
using Seed.Common.Domain;
using Xunit;

namespace Seed.Tests.Data
{
    public class QueryableOrderByTests
    {
        private static readonly OrderableEntity[] Items =
        {
            new OrderableEntity(0, "Fred"),
            new OrderableEntity(1, "Wilma"),
            new OrderableEntity(2, "Dino"),
            new OrderableEntity(3, "Joe"),
            new OrderableEntity(4, "Arnold"),
            new OrderableEntity(5, "Mr Slate"),
            new OrderableEntity(6, "Sam"),
            new OrderableEntity(7, "Bamm-bamm"),
            new OrderableEntity(8, "Barney"),
            new OrderableEntity(9, "Betty")
        };

        [Fact]
        public void OrderByProperty_GivenAnUnorderedSet_OrdersSetAscendinglyByInteger()
        {
            var input = CreateQueryable(0, 9, 1, 8, 2, 7, 3, 6, 4, 5);
            
            var actual = input.OrderByProperty("Value");

            var expected = CreateQueryable(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);

            Assert.Equal(expected, actual);
        }    

        [Fact]
        public void OrderByProperty_GivenAnUnorderedSet_OrdersSetAscendinglyByString()
        {
            var input = CreateQueryable(0, 9, 1, 8, 2, 7, 3, 6, 4, 5);
            
            var actual = input.OrderByProperty("Name");

            var expected = CreateQueryable(4, 7, 8, 9, 2, 0, 3, 5, 6, 1);

            Assert.Equal(expected, actual);
        }       

        [Fact]
        public void OrderByPropertyDescending_GivenAnUnorderedSet_OrdersSetDescendinglyByInteger()
        {
            var input = CreateQueryable(0, 9, 1, 8, 2, 7, 3, 6, 4, 5);

            var actual = input.OrderByPropertyDescending("Value");

            var expected = CreateQueryable(9, 8, 7, 6, 5, 4, 3, 2, 1, 0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByPropertyDescending_GivenAnUnorderedSet_OrdersSetDescendinglyByString()
        {
            var input = CreateQueryable(0, 9, 1, 8, 2, 7, 3, 6, 4, 5);

            var actual = input.OrderByPropertyDescending("Name");

            var expected = CreateQueryable(1, 6, 5, 3, 0, 2, 9, 8, 7, 4);

            Assert.Equal(expected, actual);
        }   

        private IQueryable<OrderableEntity> CreateQueryable(params int[] index)
        {
            var result = index.Select(value => Items.ElementAt(value)).ToList();

            return result.AsQueryable();
        }

        [Fact]
        public void OrderByProperty_GivenAnUnorderedSet_OrdersSetAscendinglyByNestedProperty()
        {
            var input = CreateQueryable(0, 9, 1, 8, 2, 7, 3, 6, 4, 5);

            var actual = input.OrderByProperty("Nested.Value");

            var expected = CreateQueryable(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);

            Assert.Equal(expected, actual);
        }    


        private class OrderableEntity
        {
            public int Value { get; private set; }
            public string Name { get; private set; }

            public Nested Nested { get; private set; }

            public OrderableEntity(int value, string name)
            {
                Value = value;
                Name = name;

                Nested = new Nested(value, name);
            }

            public override bool Equals(object obj)
            {
                var other = (OrderableEntity)obj;

                return other.Value == Value && other.Name == Name;
            }

            public override int GetHashCode()
            {
                return HashCodeUtility.Hash(Name.GetHashCode(), Value);
            }
        }

        private class Nested
        {
            public int Value { get; set; }
            public string Name { get; set; }

            public Nested(int value, string name)
            {
                Value = value;
                Name = name;
            }
        }
    }
}
