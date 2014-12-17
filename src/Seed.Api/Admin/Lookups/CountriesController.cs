using System;
using System.Web.Http;
using Seed.Common.CommandHandling;
using Seed.Infrastructure.Data;
using Seed.Lookups;

namespace Seed.Api.Admin.Lookups
{
    [Authorize]
    [RoutePrefix("admin/lookups/countries")]
    public class CountriesController : LookupsController<Country>
    {
        public CountriesController(
            ICommandBus commandBus,
            ISeedDbContext dbContext)
            : base(commandBus, dbContext)
        {
        }
    }
}
