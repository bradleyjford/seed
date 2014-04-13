﻿using System;

namespace Seed.Web.Http.AntiXsrf
{
    /// <summary>
    /// Represents the security token for the Anti-XSRF system.
    /// The token is a random 128-bit value that correlates the session with the request body.
    /// </summary>
    internal sealed class AntiForgeryToken
    {
        internal const int SecurityTokenBitLength = 128;
        internal const int ClaimUidBitLength = 256;

        private BinaryBlob _securityToken;
        private string _username;
        private string _additionalData;

        public string AdditionalData
        {
            get { return _additionalData ?? String.Empty; }
            set { _additionalData = value; }
        }

        public BinaryBlob ClaimUid { get; set; }

        public bool IsSessionToken { get; set; }

        public BinaryBlob SecurityToken
        {
            get
            {
                if (_securityToken == null)
                {
                    _securityToken = new BinaryBlob(SecurityTokenBitLength);
                }

                return _securityToken;
            }

            set { _securityToken = value; }
        }

        public string Username
        {
            get { return _username ?? String.Empty; }
            set { _username = value; }
        }
    }
}
