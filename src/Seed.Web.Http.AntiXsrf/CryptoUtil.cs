﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;

namespace Seed.Web.Http.AntiXsrf
{
    internal static class CryptoUtil
    {
        /// <summary>
        /// This method is specially written to take the same amount of time
        /// regardless of where 'a' and 'b' differ. Please do not optimize it.
        /// </summary>
        public static bool AreByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            bool areEqual = true;
            for (int i = 0; i < a.Length; i++)
            {
                areEqual &= (a[i] == b[i]);
            }
            return areEqual;
        }

        /// <summary>
        /// Computes a SHA256 hash over all of the input parameters.
        /// Each parameter is UTF8 encoded and preceded by a 7-bit encoded
        /// integer describing the encoded byte length of the string.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "MemoryStream is resilient to double-Dispose")]
        public static byte[] ComputeSHA256(IList<string> parameters)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var parameter in parameters)
                {
                    bw.Write(parameter); // also writes the length as a prefix; unambiguous
                }

                bw.Flush();

                using (var sha256 = new SHA256Cng())
                {
                    return sha256.ComputeHash(ms.GetBuffer(), 0, checked((int)ms.Length));
                }
            }
        }
    }
}