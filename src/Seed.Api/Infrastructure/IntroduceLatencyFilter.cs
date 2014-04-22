﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Seed.Api.Infrastructure
{
    public class IntroduceLatencyFilter : IActionFilter
    {
        public bool AllowMultiple { get { return true; } }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            await Task.Delay(1000);

            return await continuation();
        }
    }
}