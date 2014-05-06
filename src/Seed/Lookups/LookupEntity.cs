using System;
using Seed.Infrastructure.Domain;

namespace Seed.Lookups
{
    public abstract class LookupEntity : AggregateRoot<int>, ILookupEntity
    {
        public string Name { get; set; }

        public bool IsActive { get; private set; }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
