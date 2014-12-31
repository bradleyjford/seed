using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Seed.Common.Domain;

namespace Seed.Common.Net.Http.Filters
{
    public class EntityNotFoundFilter : IActionFilter
    {
        public bool AllowMultiple
        {
            get { return false; }
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken, 
            Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                return await continuation();
            }
            catch (EntityNotFoundException)
            {
                var result = new NotFoundResult(actionContext.Request);

                return result.ExecuteAsync(cancellationToken).Result;
            }
        }
    }
}