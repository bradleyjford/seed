using System;

namespace Seed.Web.Http.AntiXsrf
{
    /// <summary>
    /// Abstracts out the serialization process for an anti-forgery token
    /// </summary>
    internal interface IAntiForgeryTokenSerializer
    {
        AntiForgeryToken Deserialize(string serializedToken);
        string Serialize(AntiForgeryToken token);
    }
}
