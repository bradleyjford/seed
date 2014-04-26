using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Seed.Admin.Users;
using Seed.Infrastructure.Messaging;
using Seed.Security;

namespace Seed.Api.Admin.Users
{
    [RoutePrefix("admin/users")]
    [Authorize]
    public class UsersController : ApiCommandController
    {
        private readonly ICommandBus _bus;
        private readonly IUserRepository _repository;

        public UsersController(ICommandBus bus, IUserRepository repository)
        {
            _bus = bus;
            _repository = repository;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var users = _repository.GetAll();

            var response = Mapper.Map<IEnumerable<GetUserResponse>>(users);

            return Ok(response);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var user = _repository.Get(id);

            var response = Mapper.Map<GetUserResponse>(user);

            return Ok(response);
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]SaveUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = Mapper.Map<EditUserCommand>(request);

            var result = _bus.Submit(command);

            return CommandResult(result);
        }
    }
}