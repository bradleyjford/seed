using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;

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
        private readonly ILookupRepository<TLookupEntity> _repository;

        public EditLookupCommandHandler(ILookupRepository<TLookupEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(EditLookupCommand<TLookupEntity> command)
        {
            var lookup = await _repository.Get(command.Id);

            if (lookup == null)
            {
                throw new IndexOutOfRangeException();
            }

            lookup.Name = command.Name;

            return CommandResult.Ok;
        }
    }
}
