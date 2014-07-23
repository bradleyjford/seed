using System;
using System.Collections.Generic;

namespace Seed.Common.Specifications
{
    public class OrSpecification<T> : ISpecification<T>
    {
        private readonly List<ISpecification<T>> _specifications;

        public OrSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _specifications = new List<ISpecification<T>>(2) 
            { 
                first, 
                second 
            };
        }

        public OrSpecification(ISpecification<T> first, ISpecification<T> second, params ISpecification<T>[] additional)
        {
            _specifications = new List<ISpecification<T>>(2 + additional.Length) 
            { 
                first, 
                second 
            };

            _specifications.AddRange(additional);
        }

        public bool IsSatisfiedBy(T target)
        {
            for (var i = 0; i < _specifications.Count; i++)
            {
                if (_specifications[i].IsSatisfiedBy(target))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
