using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ISeedDbContext _dbContext;

        public UsersController(
            ICommandBus bus, 
            ISeedDbContext dbContext)
        {
            _bus = bus;
            _dbContext = dbContext;
        }

        [Route("")]
        public IHttpActionResult Get([FromUri]UserQueryFilter filter, [FromUri]PagingOptions pagingOptions)
        {
            var usersQuery = _dbContext.Users.AsQueryable();

            if (filter == null)
            {
                filter = new UserQueryFilter();
            }

            if (pagingOptions == null)
            {
                pagingOptions = new PagingOptions
                {
                    PageSize = 10
                };
            }

            if (!String.IsNullOrEmpty(filter.UserName))
            {
                usersQuery = usersQuery.Where(u => u.Username.StartsWith(filter.UserName));
            }

            usersQuery = usersQuery.OrderBy(u => u.Username);

            var users = usersQuery.Paged(pagingOptions);

            var response = Mapper.Map<IEnumerable<GetUserResponse>>(users);

            return Ok(response);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            var response = Mapper.Map<GetUserResponse>(user);

            return Ok(response);
        }

        [Route("{id:int}")]
        [ValidateAntiForgeryToken]
        public IHttpActionResult Post(int id, [FromBody]SaveUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = Mapper.Map<EditUserCommand>(request);

            command.UserId = id;

            var result = _bus.Submit(command);

            return CommandResult(result);
        }

        [Route("{id:int}/activate")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IHttpActionResult Activate(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new ActivateUserCommand(id);

            var result = _bus.Submit(command);

            return CommandResult(result);
        }
        
        [Route("{id:int}/deactivate")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IHttpActionResult Deactivate(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new DeactivateUserCommand(id);

            var result = _bus.Submit(command);

            return CommandResult(result);
        }
    }
}