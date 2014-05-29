using System;
using System.Data.Entity;

namespace Seed.Common.Data
{
    public interface IUnitOfWork2<out TDbContext> : IDisposable
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
    }
}
