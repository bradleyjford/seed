using System;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using Seed.Admin.Lookups;
using Seed.Common.CommandHandling;
using Seed.Common.Data;
using Seed.Common.Domain;
using Seed.Infrastructure.Data;

namespace Seed.Api.Admin.Lookups
{
    public abstract class LookupsController<TLookup> : ApiCommandController
        where TLookup : class, ILookupEntity
    {
        private readonly ICommandBus _mediator;
        private readonly ISeedDbContext _dbContext;

        protected LookupsController(
            ICommandBus mediator,
            ISeedDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(
            [FromUri] LookupQueryFilter filter,
            [FromUri] PagingOptions pagingOptions)
        {
            var items = await _dbContext.Set<TLookup>()
                .ApplyFilter(filter)
                .Project().To<LookupSummaryResponse>()
                .ToPagedResultAsync(pagingOptions, new SortDescriptor("Name"));

            return Ok(items);
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] int id)
        {
            var item = await _dbContext.Set<TLookup>().FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] CreateLookupRequest request)
        {
            var command = new CreateLookupCommand<TLookup>(request.Name);

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(
            [FromUri] int id,
            [FromBody] EditLookupRequest request)
        {
            var command = new EditLookupCommand<TLookup>(id, request.Name);

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}/activate")]
        [HttpPost]
        public async Task<IHttpActionResult> Activate([FromUri] int id)
        {
            var command = new ActivateLookupCommand<TLookup>(id);

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}/deactivate")]
        [HttpPost]
        public async Task<IHttpActionResult> Deactivate([FromUri] int id)
        {
            var command = new DeactivateLookupCommand<TLookup>(id);

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }
    }
}
