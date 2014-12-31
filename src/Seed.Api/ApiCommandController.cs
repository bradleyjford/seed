using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Seed.Api.Infrastructure.ActionResults;
using Seed.Common.CommandHandling;
using FluentValidationResult = FluentValidation.Results.ValidationResult;
using SystemValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

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

        protected IHttpActionResult ValidationFailure(IEnumerable<SystemValidationResult> results)
        {
            return new ValidationFailureResponse(ActionContext, results);
        }
    }
}
