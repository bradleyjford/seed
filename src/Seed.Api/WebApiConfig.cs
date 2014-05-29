using System;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Seed.Api.Infrastructure.Filters;

namespace Seed.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var formatters = config.Formatters;
            
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter("Bearer"));

#if DEBUG
            config.Filters.Add(new IntroduceLatencyFilter(TimeSpan.FromMilliseconds(300)));
#endif
        }
    }
}