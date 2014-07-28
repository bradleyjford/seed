using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Seed.Common.Domain;

namespace Seed.Common.Data.Testing
{
    public class TestEntityDbSet<TEntity, TId> : TestDbSet<TEntity>
        where TEntity : Entity<TId>
    {
        public override TEntity Find(params object[] keyValues)
        {
            var id = (TId)keyValues.Single();

            return this.SingleOrDefault(e => e.Id.Equals(id));
        }

        public override Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }

        public override Task<TEntity> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }
}
