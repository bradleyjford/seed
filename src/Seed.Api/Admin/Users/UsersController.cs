﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
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
        public async Task<IHttpActionResult> Get([FromUri]UserQueryFilter filter, [FromUri]PagingOptions pagingOptions)
        {
            filter = filter ?? new UserQueryFilter();
            pagingOptions = pagingOptions ?? new PagingOptions();

            var usersQuery = _dbContext.Users.AsQueryable();

            usersQuery = filter.Apply(usersQuery);

            usersQuery = usersQuery.OrderBy(u => u.UserName);

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
        public async Task<IHttpActionResult> Post(int id, [FromBody]EditUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = Mapper.Map<EditUserCommand>(request);

            command.UserId = id;

            var result = await _bus.Send(command);

            return CommandResult(result);
        }

        [Route("{id:int}/activate")]
        [HttpPost]
        public async Task<IHttpActionResult> Activate(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new ActivateUserCommand(id);

            var result = await _bus.Send(command);

            return CommandResult(result);
        }
        
        [Route("{id:int}/deactivate")]
        [HttpPost]
        public async Task<IHttpActionResult> Deactivate(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new DeactivateUserCommand(id);

            var result = await _bus.Send(command);

            return CommandResult(result);
        }
    }
}