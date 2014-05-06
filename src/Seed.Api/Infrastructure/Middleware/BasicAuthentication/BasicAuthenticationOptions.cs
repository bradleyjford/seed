using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Seed.Api.Infrastructure.Middleware.BasicAuthentication
{
    public delegate Task<IEnumerable<Claim>> CredentialValidationCallback(string username, string password);

    public class BasicAuthenticationOptions : AuthenticationOptions
    {
        private readonly string _realm;
        private readonly CredentialValidationCallback _credentialValidationFunction;

        public BasicAuthenticationOptions(string realm, CredentialValidationCallback credentialValidationFunction) 
            : base("Basic")
        {
            _realm = realm;
            _credentialValidationFunction = credentialValidationFunction;
        }

        public string Realm
        {
            get { return _realm; }
        }

        public CredentialValidationCallback CredentialValidationFunction
        {
            get { return _credentialValidationFunction; }
        }
    }
}