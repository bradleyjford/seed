using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using Seed.Common.CommandHandling;
using Seed.Common.Net.Http.ActionResults;

namespace Seed.Api
{
    public abstract class ApiCommandController : ApiController
    {
        protected IHttpActionResult CommandResult(ICommandResult result)
        {
            if (result.Success)
            {
                return Ok();
            }

            return InternalServerError(result.Error);
        }

        protected IHttpActionResult ValidationFailure(IEnumerable<ValidationResult> results)
        {
            return new ValidationFailureResponse(ActionContext, results);
        }
    }
}
