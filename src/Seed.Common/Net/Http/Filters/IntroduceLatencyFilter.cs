using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Seed.Common.Net.Http.Filters
{
    public class IntroduceLatencyFilter : IActionFilter
    {
        private readonly TimeSpan _latency;

        public IntroduceLatencyFilter(TimeSpan latency)
        {
            _latency = latency;
        }

        public bool AllowMultiple { get { return true; } }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken, 
            Func<Task<HttpResponseMessage>> continuation)
        {
            await Task.Delay(_latency, cancellationToken);

            return await continuation();
        }
    }
}