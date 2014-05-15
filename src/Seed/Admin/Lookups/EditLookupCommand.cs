using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;

namespace Seed.Admin.Lookups
{
    public class EditLookupCommand<TLookupEntity> : ICommand
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditLookupCommandHandler<TLookupEntity> : ICommandHandler<EditLookupCommand<TLookupEntity>>
        where TLookupEntity : class, ILookupEntity
    {
        private readonly ILookupRepository<TLookupEntity> _repository;

        public EditLookupCommandHandler(ILookupRepository<TLookupEntity> repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Handle(EditLookupCommand<TLookupEntity> command)
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
