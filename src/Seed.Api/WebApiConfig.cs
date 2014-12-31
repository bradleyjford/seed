using System;
using System.Web.Http;
using FluentValidation.WebApi;
using Newtonsoft.Json.Serialization;
using Seed.Api.Infrastructure;
using Seed.Common.Net.Http;
using Seed.Common.Net.Http.Filters;

namespace Seed.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(new SeedDirectRouteProvider());

            var formatters = config.Formatters;
            
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter("Bearer"));
            config.Filters.Add(new ValidationFilter());
            config.Filters.Add(new EntityNotFoundFilter());

            FluentValidationModelValidatorProvider.Configure(config);

#if DEBUG
            //config.Filters.Add(new IntroduceLatencyFilter(TimeSpan.FromMilliseconds(150)));
#endif
        }
    }
}
