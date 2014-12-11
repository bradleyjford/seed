using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;
using FluentValidation.WebApi;
using Newtonsoft.Json.Serialization;
using Seed.Web.Infrastructure;
using Seed.Web.Infrastructure.Filters;

namespace Seed.Web
{
    public static class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(new SeedDirectRouteProvider());

            RegisterFormatters(config.Formatters);
            RegisterFilters(config.Filters);

            FluentValidationModelValidatorProvider.Configure(config);
        }

        public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public static void RegisterFilters(HttpFilterCollection filters)
        {
            filters.Add(new WebApiValidationFilter());

#if DEBUG
            //config.Filters.Add(new IntroduceLatencyFilter(TimeSpan.FromMilliseconds(150)));
#endif
        }
    }
}
