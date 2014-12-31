using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Hosting;

namespace Seed.Api.Infrastructure
{
    public class BufferNonStreamedContentHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.Content != null)
            {
                var services = request.GetConfiguration().Services;
                var bufferPolicy = (IHostBufferPolicySelector)services.GetService(typeof(IHostBufferPolicySelector));

                // If the host is going to buffer it anyway
                if (bufferPolicy.UseBufferedOutputStream(response))
                {
                    // Buffer it now so we can catch the exception
                    await response.Content.LoadIntoBufferAsync();
                }
            }

            return response;
        }
    }
}