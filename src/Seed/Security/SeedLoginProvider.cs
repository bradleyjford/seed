using System;

namespace Seed.Security
{
    public class SeedLoginProvider : LoginProvider
    {
        private const string ProviderName = "Seed";

        public SeedLoginProvider(string userKey) 
            : base(ProviderName, userKey)
        {
        }


    }
}
