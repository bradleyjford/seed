// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Security.Principal;

namespace Seed.Web.Http.AntiXsrf
{
    // Can extract unique identifers for a claims-based identity
    internal interface IClaimUidExtractor
    {
        BinaryBlob ExtractClaimUid(IIdentity identity);
    }
}
