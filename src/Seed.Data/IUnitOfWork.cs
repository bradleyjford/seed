using System;
using System.Data.Entity;

namespace Seed.Data
{
    public interface IUnitOfWork<out TDbContext> : IDisposable
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
    }
}
