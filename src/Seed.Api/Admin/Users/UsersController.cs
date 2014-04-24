using System;
using System.Web.Http;

namespace Seed.Api.Admin.Users
{
    [RoutePrefix("admin/users")]
    [Authorize]
    public class UsersController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UserRepository.GetAll());
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(UserRepository.Get(id));
        }
    }
}