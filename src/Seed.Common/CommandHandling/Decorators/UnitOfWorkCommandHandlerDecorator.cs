using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Seed.Common.CommandHandling.Decorators
{
    public class UnitOfWorkCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : class
    {
        private readonly ICommandHandler<TCommand, TResult> _decorated;
        private readonly DbContext _dbContext;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<TCommand, TResult> decorated,
            DbContext dbContext)
        {
            _decorated = decorated;
            _dbContext = dbContext;
        }

        public async Task<TResult> Handle(TCommand command)
        {
            var result = await _decorated.Handle(command);

            RestoreRowVersions();

            await _dbContext.SaveChangesAsync();

            return result;
        }

        private void RestoreRowVersions()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    continue;
                }

                if (entry.CurrentValues.PropertyNames.Contains("RowVersion"))
                {
                    var property = entry.Property("RowVersion");

                    property.OriginalValue = property.CurrentValue;
                    property.IsModified = false;
                }
            }
        }
    }
}
