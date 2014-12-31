using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Seed.Common.CommandHandling;
using Seed.Security;

namespace Seed.Api.Security
{
    [RoutePrefix("register")]
    public class RegistrationController : ApiCommandController
    {
        static RegistrationController()
        {
            Mapper.CreateMap<RegisterUserRequest, RegisterUserCommand>();
            Mapper.CreateMap<ConfirmUserRequest, ConfirmUserCommand>();
        }

        private readonly ICommandBus _mediator;

        public RegistrationController(ICommandBus mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(RegisterUserRequest request)
        {
            var command = Mapper.Map<RegisterUserCommand>(request);

            var validationResult = await _mediator.Validate(command);

            if (!validationResult.Success())
            {
                return ValidationFailure(validationResult);
            }

            var result = await _mediator.Execute(command);

            return Ok();
        }

        [Route("confirm")]
        [HttpPost]
        public async Task<IHttpActionResult> Confirm(ConfirmUserRequest request)
        {
            var command = Mapper.Map<ConfirmUserCommand>(request);

            var result = await _mediator.Execute(command);

            return Ok();
        }
    }
}
