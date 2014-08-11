using System;
using System.Web.Http;
using Seed.Common.CommandHandling;
using Seed.Infrastructure.Data;
using Seed.Lookups;

namespace Seed.Api.Admin.Lookups
{
    [RoutePrefix("admin/lookups/countries")]
    [Authorize]
    public class CountriesController : LookupsController<Country>
    {
        public CountriesController(
            ICommandBus mediator,
            ISeedDbContext dbContext)
            : base(mediator, dbContext)
        {
        }
    }
}
