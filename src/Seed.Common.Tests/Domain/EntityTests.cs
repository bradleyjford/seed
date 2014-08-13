using System;
using Xunit;

namespace Seed.Common.Tests.Domain
{
    public partial class EntityTests
    {
        [Fact]
        public void Equals_EntitiesOfSameTypeWithSameId_AreEqual()
        {
            var a = new Person(5);
            var b = new Person(5);

            Assert.True(a.Equals(b));
        }

        [Fact]
        public void Equals_EntitiesOfSameTypeWithDifferentIds_AreNotEqual()
        {
            var a = new Person(1);
            var b = new Person(2);

            Assert.False(a.Equals(b));
        }

        [Fact]
        public void Equals_ComparingEntityWithNull_IsNotEqual()
        {
            var a = new Person(1);
            
            Assert.False(a.Equals(null));
        }

        [Fact]
        public void Equals_EntitiesOfDifferentTypesWithSameIds_AreNotEqual()
        {
            var person = new Person(1);
            var dog = new Dog(1);

// ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(person.Equals(dog));
        }
    }
}
