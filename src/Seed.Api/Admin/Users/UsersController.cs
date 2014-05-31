using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Seed.Admin.Users;
using Seed.Common.CommandHandling;
using Seed.Common.Data;
using Seed.Data;
using Seed.Security;

namespace Seed.Api.Admin.Users
{
    [RoutePrefix("admin/users")]
    [Authorize]
    public class UsersController : ApiCommandController
    {
        private readonly ICommandBus _bus;
        private readonly ISeedDbContext _dbContext;

        public UsersController(
            ICommandBus bus, 
            ISeedDbContext dbContext)
        {
            _bus = bus;
            _dbContext = dbContext;
        }

        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] UserQueryFilter filter, [FromUri] PagingOptions pagingOptions)
        {
            var users = await _dbContext.Users
                .ApplyFilter(filter)
                .Paged(pagingOptions, new SortDescriptor("Id"))
                .Project().To<UserSummaryResponse>()
                .ToListAsync();

            return Ok(users);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get([FromUri] int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            var response = Mapper.Map<UserDetailResponse>(user);

            return Ok(response);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Post([FromUri] int id, [FromBody] EditUserRequest request)
        {
            var command = Mapper.Map<EditUserCommand>(request);

            command.UserId = id;

            var result = await _bus.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}/activate")]
        [HttpPost]
        public async Task<IHttpActionResult> Activate([FromUri] int id)
        {
            var command = new ActivateUserCommand(id);

            var result = await _bus.Send(command);

            return CommandResult(result);
        }
        
        [Route("{id:int}/deactivate")]
        [HttpPost]
        public async Task<IHttpActionResult> Deactivate([FromUri] int id)
        {
            var command = new DeactivateUserCommand(id);

            var result = await _bus.Send(command);

            return CommandResult(result);
        }
    }
}