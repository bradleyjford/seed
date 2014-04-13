using System;
using System.Web.Http;
using System.Web.Security;
using Seed.Api.Infrastructure;

namespace Seed.Api.Security
{
    [RoutePrefix("authentication")]
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        [Route("signin")]
        [ValidateAntiForgeryToken]
        public IHttpActionResult SignIn([FromBody]SignInRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.UserName != "test")
            {
                return BadRequest("Invalid username or password");
            }

            FormsAuthentication.SetAuthCookie(request.UserName, false);

            var roles = new[] {
                "admin",
                "user",
                "public"
            };

            var response = new SignInSuccessResponse("Testing User", roles);

            return Ok(response);
        }

        [HttpPost]
        [Route("principal")]
        public IHttpActionResult Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                var roles = new[] {
                    "admin",
                    "user",
                    "public"
                };

                var response = new SignInSuccessResponse("Testing User", roles);

                return Ok(response);
            }

            return Unauthorized();
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