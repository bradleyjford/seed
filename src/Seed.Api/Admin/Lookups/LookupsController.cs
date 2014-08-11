using System;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
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
            var command = MapRequestToCommand<CreateLookupCommand<TLookup>>(request);

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(
            [FromUri] int id,
            [FromBody] EditLookupRequest request)
        {
            var command = MapRequestToCommand<EditLookupCommand<TLookup>>(request);

            command.Id = id;

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}/activate")]
        [HttpPost]
        public async Task<IHttpActionResult> Activate([FromUri] int id)
        {
            var command = MapRequestToCommand<ActivateLookupCommand<TLookup>>(new { Id = id });

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}/deactivate")]
        [HttpPost]
        public async Task<IHttpActionResult> Deactivate([FromUri] int id)
        {
            var command = MapRequestToCommand<DeactivateLookupCommand<TLookup>>(new { Id = id });

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }

        private static ILookupCommand MapRequestToCommand<TCommand>(object request) 
            where TCommand : new()
        {
            var commandInstance = new TCommand();

            Mapper.DynamicMap(request, commandInstance, request.GetType(), typeof(TCommand));

            return (ILookupCommand)commandInstance;
        }
    }
}
