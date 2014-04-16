using Newtonsoft.Json.Serialization;
using Seed.Api.Infrastructure;
using System;
using System.Web.Http;

namespace Seed.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Filters.Add(new ApplyAntiForgeryToken(true));
            config.Filters.Add(new IntroduceLatencyFilter());
        }
    }
}