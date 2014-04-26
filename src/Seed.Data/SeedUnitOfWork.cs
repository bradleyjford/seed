using System;
using Seed.Infrastructure.Domain;

namespace Seed.Data
{
    public class SeedUnitOfWork : ISeedUnitOfWork
    {
        private readonly IUserContext _userContext;
        private readonly SeedDbContext _context;

        public SeedUnitOfWork(IUserContext userContext)
        {
            _userContext = userContext;
            _context = new SeedDbContext();
        }

        public SeedDbContext DbContext 
        { 
            get { return _context; }
        }

        public void Dispose()
        {
            _context.SaveChanges(_userContext);
            _context.Dispose();
        }
    }
}
