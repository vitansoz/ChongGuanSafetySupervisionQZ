using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanDotNetUtils.Extensions
{
    public static class ExtensionString
    {
        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// this method is copied from IdentityServer4.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha256(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

        public static string Md5(this string input, Encoding encoding)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            using (var md5 = MD5.Create())
            {
                var bytes = encoding.GetBytes(input);
                var hash = md5.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string Md5(this string input)
        {
            return Md5(input, Encoding.UTF8);
        }

        public static HttpContent ToHttpContent(this string input)
        {
            var content = new StringContent(input, System.Text.Encoding.UTF8, "application/json");
            return content;
        }

        public static byte[] Gzip(this byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(data, 0, data.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
        }


        static byte[] UnGzip(this byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
    }
}
