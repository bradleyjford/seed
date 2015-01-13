using System;
using System.Security.Claims;
using System.Web.Http;

namespace Seed.Api.Security
{
    [RoutePrefix("identity")]
    public class IdentityController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var user = (ClaimsPrincipal)User;

            var displayName = user.FindFirst(ClaimTypes.Name).Value;

            var response = new IdentityResponse(user.Identity.Name, displayName, user.Claims);

            return Ok(response);
        }
    }
}
