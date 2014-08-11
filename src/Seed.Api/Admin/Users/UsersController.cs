using System;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Seed.Admin.Users;
using Seed.Common.CommandHandling;
using Seed.Common.Data;
using Seed.Infrastructure.Data;
using Seed.Security;

namespace Seed.Api.Admin.Users
{
    [RoutePrefix("admin/users")]
    [Authorize]
    public class UsersController : ApiCommandController
    {
        private readonly ICommandBus _mediator;
        private readonly ISeedDbContext _dbContext;

        public UsersController(
            ICommandBus mediator,
            ISeedDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [Route("")]
        public async Task<IHttpActionResult> Get(
            [FromUri] UserQueryFilter filter,
            [FromUri] PagingOptions pagingOptions)
        {
            var users = await _dbContext.Users
                .ApplyFilter(filter)
                .Project().To<UserSummaryResponse>()
                .ToPagedResultAsync(pagingOptions, new SortDescriptor("Id"));

            return Ok(users);
        }

        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get([FromUri] Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            var response = Mapper.Map<UserDetailResponse>(user);

            return Ok(response);
        }

        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Post([FromUri] Guid id, [FromBody] EditUserRequest request)
        {
            var command = Mapper.Map<EditUserCommand>(request);

            command.UserId = id;

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }

        [Route("{id:Guid}/activate")]
        [HttpPost]
        public async Task<IHttpActionResult> Activate([FromUri] Guid id)
        {
            var command = new ActivateUserCommand(id);

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }

        [Route("{id:Guid}/deactivate")]
        [HttpPost]
        public async Task<IHttpActionResult> Deactivate([FromUri] Guid id)
        {
            var command = new DeactivateUserCommand(id);

            var result = await _mediator.Send(command);

            return CommandResult(result);
        }
    }
}
