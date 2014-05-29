//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Threading.Tasks;
//using System.Web.Http;
//using Seed.Common.CommandHandling;
//using Seed.Common.Data;
//using Seed.Data;
//using Seed.Lookups;

//namespace Seed.Api.Admin.Lookups
//{
//    [RoutePrefix("admin/lookups/{type}")]
//    public class LookupsController : ApiCommandController
//    {
//        private static readonly IDictionary<string, Type> LookupEntityTypeMap = 
//            new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase)
//            {
//                { "countries", typeof(Country) }
//            };

//        private readonly ICommandBus _bus;
//        private readonly ISeedDbContext _dbContext;

//        public LookupsController(
//            ICommandBus bus, 
//            ISeedDbContext dbContext)
//        {
//            _bus = bus;
//            _dbContext = dbContext;
//        }

//        [Route("")]
//        public async Task<IHttpActionResult> Get([FromUri] string type, [FromBody] PagingOptions pagingOptions)
//        {
//            var entityType = ResolveEntityType(type);

//            if (entityType == null)
//            {
//                return NotFound();
//            }

//            var items = await _dbContext.Set(entityType).ToListAsync();

//            return Ok(items);
//        }

//        private Type ResolveEntityType(string type)
//        {
//            if (LookupEntityTypeMap.ContainsKey(type))
//            {
//                return LookupEntityTypeMap[type];
//            }

//            return null;
//        }

//        [Route("{id:int}")]
//        public async Task<IHttpActionResult> Get([FromUri] string type, [FromUri] int id)
//        {
//            var entityType = ResolveEntityType(type);

//            if (entityType == null)
//            {
//                return NotFound();
//            }

//            var item = await _dbContext.Set(entityType).FindAsync(id);

//            if (item == null)
//            {
//                return NotFound();
//            }

//            return Ok(item);
//        }

//        //[Route("{id:int")]
//        //public async Task<IHttpActionResult> Post([FromUri] string type, [FromUri] int id, [FromBody] EditLookupRequest request)
//        //{
//        //    if (!ModelState.IsValid)
//        //    {
//        //        return BadRequest(ModelState);
//        //    }

//        //    var entityType = ResolveEntityType(type);

//        //    if (entityType == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    var command = Mapper.Map<EditLookupCommand<TLookupEntity>>(request);

//        //    var result = await _bus.Submit(command);

//        //    return CommandResult(result);
//        //}
//    }
//}