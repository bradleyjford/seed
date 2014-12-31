using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Seed.Common.Net.Http.ActionResults;

namespace Seed.Common.Net.Http.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public bool AllowMultiple
        {
            get { return false; }
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

            var result = new ValidationFailureResponse(actionContext, actionContext.ModelState);

            return result.ExecuteAsync(cancellationToken);
        }
    }
}
