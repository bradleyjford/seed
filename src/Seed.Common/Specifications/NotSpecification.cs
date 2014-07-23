using System;

namespace Seed.Common.Specifications
{
    public class NotSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _specification;

        public NotSpecification(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public bool IsSatisfiedBy(T target)
        {
            return !_specification.IsSatisfiedBy(target);
        }
    }
}
