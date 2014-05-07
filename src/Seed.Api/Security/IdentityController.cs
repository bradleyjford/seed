using System;
using System.Security.Claims;
using System.Web.Http;

namespace Seed.Api.Security
{
    [RoutePrefix("identity")]
    public class IdentityController : ApiCommandController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User as ClaimsPrincipal;

                var response = new SignInSuccessResponse(user.Identity.Name, "Testing User", new [] { "admin" });

                return Ok(response);
            }

            return NotFound();
        }
    }
}