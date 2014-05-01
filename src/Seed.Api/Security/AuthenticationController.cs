using System;
using System.Web.Http;
using System.Web.Security;
using AutoMapper;
using Seed.Api.Infrastructure.Filters;
using Seed.Infrastructure.Messaging;
using Seed.Security;

namespace Seed.Api.Security
{
    [RoutePrefix("authentication")]
    public class AuthenticationController : ApiCommandController
    {
        private readonly ICommandBus _bus;

        public AuthenticationController(ICommandBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [Route("signin")]
        [ValidateAntiForgeryToken]
        public IHttpActionResult SignIn([FromBody]SignInRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = Mapper.Map<SignInCommand>(request);

            var result = _bus.Submit(command);

            if (!result.Success)
            {
                return BadRequest("Unknown username or password.");
            }

            FormsAuthentication.SetAuthCookie(request.Username, false);

            var roles = new[] {
                "admin",
                "user",
                "public"
            };

            var response = new SignInSuccessResponse(request.Username, "Testing User", roles);

            return Ok(response);
        }

        [HttpPost]
        [Route("principal")]
        [ValidateAntiForgeryToken()]
        public IHttpActionResult Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                var roles = new[] {
                    "admin",
                    "user",
                    "public"
                };

                var response = new SignInSuccessResponse("test", "Testing User", roles);

                return Ok(response);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("signout")]
        public IHttpActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            RequestContext.Principal = User = null;

            return Ok();
        }

        [HttpGet]
        [Route("test")]
        [Authorize]
        public IHttpActionResult Test()
        {
            return Ok("Test");
        }
    }
}