using System;

namespace Seed.Infrastructure.Domain
{
    public interface ILookupEntity
    {
        int Id { get; }
        string Name { get; set; }
        DateTime CreatedUtcDate { get; }
        DateTime ModifiedUtcDate { get; }
        bool IsActive { get; }
    }
}
