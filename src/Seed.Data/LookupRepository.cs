using System;
using System.Threading.Tasks;
using Seed.Admin.Lookups;
using Seed.Common.Domain;

namespace Seed.Data
{
    public class LookupRepository<TLookup> : ILookupRepository<TLookup>
        where TLookup : class, ILookupEntity
    {
        private readonly SeedDbContext _dbContext;

        public LookupRepository(ISeedUnitOfWork unitOfWork)
        {
            _dbContext = unitOfWork.DbContext;
        }

        public Task<TLookup> Get(int id)
        {
            return _dbContext.Set<TLookup>().FindAsync(id);
        }
    }
}
