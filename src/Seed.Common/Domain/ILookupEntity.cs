using System;

namespace Seed.Common.Domain
{
    public interface ILookupEntity
    {
        int Id { get; }
        string Name { get; set; }
        DateTime CreatedUtcDate { get; }
        DateTime ModifiedUtcDate { get; }
        bool IsActive { get; }

        void Activate();
        void Deactivate();
    }
}
