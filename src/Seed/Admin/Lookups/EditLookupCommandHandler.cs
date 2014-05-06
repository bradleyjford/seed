using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;

namespace Seed.Admin.Lookups
{
    public class EditLookupCommandHandler<TLookupEntity> : ICommandHandler<EditLookupCommand<TLookupEntity>>
        where TLookupEntity : class, ILookupEntity
    {
        private readonly ILookupRepository<TLookupEntity> _repository;

        public EditLookupCommandHandler(ILookupRepository<TLookupEntity> repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Execute(EditLookupCommand<TLookupEntity> command)
        {
            var lookup = await _repository.Get(command.Id);

            if (lookup == null)
            {
                return CommandResult.Fail;
            }

            lookup.Name = command.Name;

            return CommandResult.Ok;
        }
    }
}
