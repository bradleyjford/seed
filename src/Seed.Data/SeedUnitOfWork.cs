using System;

namespace Seed.Data
{
    public class SeedUnitOfWork2 : ISeedUnitOfWork2
    {
        private readonly SeedDbContext _context;

        public SeedUnitOfWork2()
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
