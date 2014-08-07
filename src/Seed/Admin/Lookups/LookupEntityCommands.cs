using System;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;

namespace Seed.Admin.Lookups
{
    public interface ILookupCommand : ICommand<CommandResult>
    {
        int Id { get; set; }
    }

    public class CreateLookupCommand<TLookupEntity> : ILookupCommand
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditLookupCommand<TLookupEntity> : ILookupCommand
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ActivateLookupCommand<TLookupEntity> : ILookupCommand
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
    }

    public class DeactivateLookupCommand<TLookupEntity> : ILookupCommand
        where TLookupEntity : ILookupEntity
    {
        public int Id { get; set; }
    }
}
