using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;

namespace Seed.Web.Http.AntiXsrf
{
    internal sealed class AntiForgeryTokenSerializer : IAntiForgeryTokenSerializer
    {
        private const byte TokenVersion = 0x01;
        private readonly ICryptoSystem _cryptoSystem;

        internal AntiForgeryTokenSerializer(ICryptoSystem cryptoSystem)
        {
            _cryptoSystem = cryptoSystem;
        }

        public AntiForgeryToken Deserialize(string serializedToken)
        {
            try
            {
                using (var stream = new MemoryStream(_cryptoSystem.Unprotect(serializedToken)))
                using (var reader = new BinaryReader(stream))
                {
                    var token = DeserializeImpl(reader);

                    if (token != null)
                    {
                        return token;
                    }
                }
            }
            catch (Exception)
            {
                // swallow all exceptions - homogenize error if something went wrong
            }

            throw HttpAntiForgeryException.CreateDeserializationFailedException();
        }

        /* The serialized format of the anti-XSRF token is as follows:
         * Version: 1 byte integer
         * SecurityToken: 16 byte binary blob
         * IsSessionToken: 1 byte Boolean
         * [if IsSessionToken = true]
         *   +- IsClaimsBased: 1 byte Boolean
         *   |  [if IsClaimsBased = true]
         *   |    `- ClaimUid: 32 byte binary blob
         *   |  [if IsClaimsBased = false]
         *   |    `- Username: UTF-8 string with 7-bit integer length prefix
         *   `- AdditionalData: UTF-8 string with 7-bit integer length prefix
         */
        private static AntiForgeryToken DeserializeImpl(BinaryReader reader)
        {
            // we can only consume tokens of the same serialized version that we generate
            var embeddedVersion = reader.ReadByte();

            if (embeddedVersion != TokenVersion)
            {
                return null;
            }

            var deserializedToken = new AntiForgeryToken();
            var securityTokenBytes = reader.ReadBytes(AntiForgeryToken.SecurityTokenBitLength / 8);

            deserializedToken.SecurityToken = new BinaryBlob(AntiForgeryToken.SecurityTokenBitLength, securityTokenBytes);
            deserializedToken.IsSessionToken = reader.ReadBoolean();

            if (!deserializedToken.IsSessionToken)
            {
                var isClaimsBased = reader.ReadBoolean();

                if (isClaimsBased)
                {
                    var claimUidBytes = reader.ReadBytes(AntiForgeryToken.ClaimUidBitLength / 8);

                    deserializedToken.ClaimUid = new BinaryBlob(AntiForgeryToken.ClaimUidBitLength, claimUidBytes);
                }
                else
                {
                    deserializedToken.Username = reader.ReadString();
                }

                deserializedToken.AdditionalData = reader.ReadString();
            }

            // if there's still unconsumed data in the stream, fail
            if (reader.BaseStream.ReadByte() != -1)
            {
                return null;
            }

            return deserializedToken;
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "MemoryStream is safe for multi-dispose.")]
        public string Serialize(AntiForgeryToken token)
        {
            Contract.Assert(token != null);

            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(TokenVersion);
                writer.Write(token.SecurityToken.GetData());
                writer.Write(token.IsSessionToken);

                if (!token.IsSessionToken)
                {
                    if (token.ClaimUid != null)
                    {
                        writer.Write(true /* isClaimsBased */);
                        writer.Write(token.ClaimUid.GetData());
                    }
                    else
                    {
                        writer.Write(false /* isClaimsBased */);
                        writer.Write(token.Username);
                    }

                    writer.Write(token.AdditionalData);
                }

                writer.Flush();

                return _cryptoSystem.Protect(stream.ToArray());
            }
        }
    }

}
