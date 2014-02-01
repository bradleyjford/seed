using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
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
                return new InvalidModelStateResult(ModelState, this);
            }

            FormsAuthentication.SetAuthCookie(request.UserName, false);

            return Ok("Success");
        }

        [HttpPost]
        [Route("signout")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IHttpActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return Ok("Success");
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