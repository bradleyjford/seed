using System;
using System.Net;
using Owin;

namespace Seed.Web.Infrastructure.Middleware.WindowsAuthentication
{
    public static class WindowsAuthenticationExtensions
    {
        /// <remarks>
        /// Only applies when hosted outside of IIS.
        /// </remarks>
        public static IAppBuilder UseWindowsAuthentication(this IAppBuilder app)
        {
            object value;

            if (app.Properties.TryGetValue("System.Net.HttpListener", out value))
            {
                var listener = value as HttpListener;

                if (listener != null)
                {
                    listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;
                }
            }

            return app;
        }
    }
}