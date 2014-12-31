using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Seed.Api.Infrastructure.ActionResults;

namespace Seed.Api.Infrastructure.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public bool AllowMultiple
        {
            get { return true; }
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken, 
            Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.ModelState.IsValid)
            {
                return continuation();
            }

            var apiController = actionContext.ControllerContext.Controller as ApiController;

            if (apiController == null)
            {
                return continuation();
            }

            var result = new ValidationFailureResponse(actionContext, actionContext.ModelState);

            return result.ExecuteAsync(cancellationToken);
        }
    }
}
