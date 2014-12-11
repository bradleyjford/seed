using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Seed.Web.Infrastructure.Filters
{
    public class WebApiValidationFilter : IActionFilter
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

            var result = new InvalidModelStateResult(actionContext.ModelState, apiController);

            return result.ExecuteAsync(cancellationToken);
        }
    }
}