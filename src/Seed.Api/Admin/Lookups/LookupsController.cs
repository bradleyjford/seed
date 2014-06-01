using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Seed.Admin.Lookups;
using Seed.Common.CommandHandling;
using Seed.Common.Data;
using Seed.Data;
using Seed.Lookups;

namespace Seed.Api.Admin.Lookups
{
    [RoutePrefix("admin/lookups/{type}")]
    [Authorize]
    public class LookupsController : ApiCommandController
    {
        private static readonly IDictionary<string, Type> LookupEntityTypeMap =
            new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "countries", typeof(Country) }
            };

        private readonly ICommandBus _bus;
        private readonly ISeedDbContext _dbContext;

        public LookupsController(
            ICommandBus bus,
            ISeedDbContext dbContext)
        {
            _bus = bus;
            _dbContext = dbContext;
        }

        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] string type, [FromUri] PagingOptions pagingOptions)
        {
            var entityType = ResolveEntityType(type);

            if (entityType == null)
            {
                return NotFound();
            }

            var items = await _dbContext.Set(entityType)
                // TODO: .Project().To<LookupSummaryResponse>()
                .ToPagedResultAsync(entityType, pagingOptions, new SortDescriptor("Name"));

            return Ok(items);
        }

        private Type ResolveEntityType(string type)
        {
            if (LookupEntityTypeMap.ContainsKey(type))
            {
                return LookupEntityTypeMap[type];
            }

            return null;
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get([FromUri] string type, [FromUri] int id)
        {
            var entityType = ResolveEntityType(type);

            if (entityType == null)
            {
                return NotFound();
            }

            var item = await _dbContext.Set(entityType).FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [Route("")]
        [HttpPut]
        public async Task<IHttpActionResult> Add(
            [FromUri] string type,
            [FromBody] CreateLookupRequest request)
        {
            var entityType = ResolveEntityType(type);

            if (entityType == null)
            {
                return NotFound();
            }

            var command = MapRequestToCommand(request, typeof(CreateLookupCommand<>), entityType);

            var result = await _bus.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(
            [FromUri] string type,
            [FromUri] int id,
            [FromBody] EditLookupRequest request)
        {
            var entityType = ResolveEntityType(type);

            if (entityType == null)
            {
                return NotFound();
            }

            var command = MapRequestToCommand(request, typeof(EditLookupCommand<>), entityType);

            var result = await _bus.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}/activate")]
        [HttpPost]
        public async Task<IHttpActionResult> Activate([FromUri] string type, [FromUri] int id)
        {
            var entityType = ResolveEntityType(type);

            if (entityType == null)
            {
                return NotFound();
            }

            var command = MapRequestToCommand(new { Id = id }, typeof(ActivateLookupCommand<>), entityType);

            var result = await _bus.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}/deactivate")]
        [HttpPost]
        public async Task<IHttpActionResult> Deactivate([FromUri] string type, [FromUri] int id)
        {
            var entityType = ResolveEntityType(type);

            if (entityType == null)
            {
                return NotFound();
            }

            var command = MapRequestToCommand(new { Id = id }, typeof(DeactivateLookupCommand<>), entityType);

            var result = await _bus.Send(command);

            return CommandResult(result);
        }

        private static ICommand<CommandResult> MapRequestToCommand(object request, Type commandType, Type entityType)
        {
            var closedCommandTYpe = commandType.MakeGenericType(entityType);

            return (ICommand<CommandResult>)Mapper.Map(request, request.GetType(), closedCommandTYpe);
        }
    }
}