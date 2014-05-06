using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Domain;

namespace Seed.Admin.Lookups
{
    public interface ILookupRepository<TLookupEntity>
        where TLookupEntity : class, ILookupEntity
    {
        Task<TLookupEntity> Get(int id);
    }
}
