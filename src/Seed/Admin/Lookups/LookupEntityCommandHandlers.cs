using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;
using Seed.Data;

namespace Seed.Admin.Lookups
{
    public class LookupEntityCommandHandlers<TLookupEntity> : 
        ICommandHandler<CreateLookupCommand<TLookupEntity>, CommandResult>,
        ICommandHandler<EditLookupCommand<TLookupEntity>, CommandResult>,
        ICommandHandler<ActivateLookupCommand<TLookupEntity>, CommandResult>,
        ICommandHandler<DeactivateLookupCommand<TLookupEntity>, CommandResult> 
        where TLookupEntity : class, ILookupEntity, new()
    {
        private readonly ISeedDbContext _dbContext;

        public LookupEntityCommandHandlers(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Add
        public Task<CommandResult> Handle(CreateLookupCommand<TLookupEntity> command)
        {
            var lookup = new TLookupEntity
            {
                Name = command.Name
            };

            _dbContext.Set<TLookupEntity>().Add(lookup);

            return Task.FromResult(CommandResult.Ok);
        }

        // Edit
        public async Task<CommandResult> Handle(EditLookupCommand<TLookupEntity> command)
        {
            var lookup = await _dbContext.Set<TLookupEntity>().FindAsync(command.Id);

            if (lookup == null)
            {
                throw new IndexOutOfRangeException();
            }

            lookup.Name = command.Name;

            return CommandResult.Ok;
        }

        // Activate
        public async Task<CommandResult> Handle(ActivateLookupCommand<TLookupEntity> command)
        {
            var lookup = await _dbContext.Set<TLookupEntity>().FindAsync(command.Id);

            if (lookup == null)
            {
                throw new IndexOutOfRangeException();
            }

            lookup.Activate();

            return CommandResult.Ok;
        }

        // Deactivate
        public async Task<CommandResult> Handle(DeactivateLookupCommand<TLookupEntity> command)
        {
            var lookup = await _dbContext.Set<TLookupEntity>().FindAsync(command.Id);

            if (lookup == null)
            {
                throw new IndexOutOfRangeException();
            }

            lookup.Deactivate();

            return CommandResult.Ok;
        }
    }
}
