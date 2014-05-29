using System;
using Seed.Common.Domain;

namespace Seed.Lookups
{
    public abstract class LookupEntity : AggregateRoot<int>, ILookupEntity
    {
        public string Name { get; set; }

        public bool IsActive { get; private set; }

        public void Activate()
        {
            if (IsActive)
            {
                throw new InvalidOperationException("Entity is already activated");
            }

            IsActive = true;
        }

        public void Deactivate()
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Entity is already deactivated");
            }

            IsActive = false;
        }
    }
}
