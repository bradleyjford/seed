using System;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;

namespace Seed.Admin.Lookups
{
    public class CreateLookupCommand<TLookupEntity> : ICommand<CommandResult>
        where TLookupEntity : ILookupEntity
    {
        public CreateLookupCommand(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class EditLookupCommand<TLookupEntity> : ICommand<CommandResult>
        where TLookupEntity : ILookupEntity
    {
        public EditLookupCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }

    public class ActivateLookupCommand<TLookupEntity> : ICommand<CommandResult>
        where TLookupEntity : ILookupEntity
    {
        public ActivateLookupCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    public class DeactivateLookupCommand<TLookupEntity> : ICommand<CommandResult>
        where TLookupEntity : ILookupEntity
    {
        public DeactivateLookupCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
