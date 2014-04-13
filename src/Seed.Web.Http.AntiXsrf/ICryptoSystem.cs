using System;

namespace Seed.Web.Http.AntiXsrf
{
    /// <summary>
    /// Provides an abstraction around the cryptographic subsystem for the anti-XSRF helpers.
    /// </summary> 
    internal interface ICryptoSystem
    {
        string Protect(byte[] data);
        byte[] Unprotect(string protectedData);
    }
}
