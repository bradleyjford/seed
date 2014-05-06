using System;
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
}
