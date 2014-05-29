using System;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;

namespace Seed.Admin.Lookups
{
    public class AddLookupCommand<TLookupEntity> : ICommand<CommandResult>
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditLookupCommand<TLookupEntity> : ICommand<CommandResult>
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ActivateLookupCommand<TLookupEntity> : ICommand<CommandResult>
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
    }

    public class DeactivateLookupCommand<TLookupEntity> : ICommand<CommandResult>
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
    }
}
