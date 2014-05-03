using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Seed.Admin.Users;
using Seed.Api.Infrastructure.Filters;
using Seed.Data;
using Seed.Infrastructure.Messaging;
using Seed.Security;

namespace Seed.Api.Admin.Users
{
    [RoutePrefix("admin/users")]
    [Authorize]
    public class UsersController : ApiCommandController
    {
        private readonly ICommandBus _bus;
        private readonly SeedDbContext _dbContext;

        public UsersController(
            ICommandBus bus, 
            ISeedUnitOfWork unitOfWork)
        {
            _bus = bus;
            _dbContext = unitOfWork.DbContext;
        }

        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri]UserQueryFilter filter, [FromUri]PagingOptions pagingOptions)
        {
            filter = filter ?? new UserQueryFilter();
            pagingOptions = pagingOptions ?? new PagingOptions();

            var usersQuery = _dbContext.Users.AsQueryable();

            usersQuery = filter.Apply(usersQuery);

            usersQuery = usersQuery.OrderBy(u => u.Username);

            var users = await usersQuery.ToPagedListAsync(pagingOptions);

            var response = Mapper.Map<IEnumerable<GetUserResponse>>(users);

            return Ok(response);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            var response = Mapper.Map<GetUserResponse>(user);

            return Ok(response);
        }

        [Route("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Post(int id, [FromBody]EditUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = Mapper.Map<EditUserCommand>(request);

            command.UserId = id;

            var result = await _bus.Submit(command);

            return CommandResult(result);
        }

        [Route("{id:int}/activate")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IHttpActionResult> Activate(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new ActivateUserCommand(id);

            var result = await _bus.Submit(command);

            return CommandResult(result);
        }
        
        [Route("{id:int}/deactivate")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IHttpActionResult> Deactivate(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new DeactivateUserCommand(id);

            var result = await _bus.Submit(command);

            return CommandResult(result);
        }
    }
}