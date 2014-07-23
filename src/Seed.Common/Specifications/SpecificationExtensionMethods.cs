using System;

namespace Seed.Common.Specifications
{
    public static class SpecificationExtensionMethods
    {
        public static AndSpecification<T> And<T>(this ISpecification<T> specification, ISpecification<T> second)
        {
            return new AndSpecification<T>(specification, second);
        }

        public static OrSpecification<T> Or<T>(this ISpecification<T> specification, ISpecification<T> second)
        {
            return new OrSpecification<T>(specification, second);
        }

        public static NotSpecification<T> Not<T>(this ISpecification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }
    }
}
