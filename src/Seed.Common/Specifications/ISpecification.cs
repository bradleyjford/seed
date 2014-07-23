using System;

namespace Seed.Common.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T target);
    }
}
