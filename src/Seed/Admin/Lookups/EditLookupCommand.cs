using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;
using Seed.Data;

namespace Seed.Admin.Lookups
{
    public class EditLookupCommand<TLookupEntity> : ICommand<CommandResult>
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditLookupCommandHandler<TLookupEntity> : ICommandHandler<EditLookupCommand<TLookupEntity>, CommandResult>
        where TLookupEntity : class, ILookupEntity
    {
        private readonly ISeedDbContext _dbContext;

        public EditLookupCommandHandler(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(EditLookupCommand<TLookupEntity> command)
        {
            //var lookup = await _dbContext.Get(command.Id);

            //if (lookup == null)
            //{
            //    throw new IndexOutOfRangeException();
            //}

            //lookup.Name = command.Name;

            return CommandResult.Ok;
        }
    }
}
