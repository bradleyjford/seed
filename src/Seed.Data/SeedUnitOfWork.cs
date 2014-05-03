using System;

namespace Seed.Data
{
    public class SeedUnitOfWork : ISeedUnitOfWork
    {
        private readonly SeedDbContext _context;

        public SeedUnitOfWork()
        {
            _context = new SeedDbContext();
        }

        public SeedDbContext DbContext 
        { 
            get { return _context; }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
