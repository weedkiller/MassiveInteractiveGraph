using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using MassiveInteractiveGraph.Common;

namespace MassiveInteractiveGraph.WebClient.Helpers
{
    public static class ResourceUtilities
    {
        private static readonly MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
        private static readonly Dictionary<string, Guid> FileHash = new Dictionary<string, Guid>();

        private static volatile object _syncRoot = new object();

        /// <summary>
        /// Appends the hash of the file as a querystring parameter to a supplied string.
        /// </summary>
        /// <param name="fname">The filename.</param>
        /// <param name="request">The current HttpRequest.</param>
        /// <returns>String with hash of the file appended.</returns>
        public static string AppendHash(this string fname, HttpRequestBase request)
        {
            return String.Format(@"{0}?hash={1}", fname, GetFileHash(fname, request));
        }

        /// <summary>
        /// Appends the hash of the file as a querystring parameter to a supplied string.
        /// </summary>
        /// <param name="fname">The filename.</param>
        /// <param name="server">The current HttpRequest's HttpServerUtilityBase.</param>
        /// <returns>String with hash of the file appended.</returns>
        public static string AppendHash(this string fname, HttpServerUtilityBase server)
        {
            return String.Format(@"{0}?hash={1}", fname, GetFileHash(fname, server));
        }

        /// <summary>
        /// Returns a hash of the supplied file.
        /// </summary>
        /// <param name="fname">The name of the file.</param>
        /// <param name="request">The current HttpRequest.</param>
        /// <returns>A Guid representing the hash of the file.</returns>
        public static Guid GetFileHash(string fname, HttpRequestBase request)
        {
            var localPath = request.RequestContext.HttpContext.Server.MapPath(fname.Replace('/', '\\'));

            return GetFileHash(localPath);
        }

        public static Guid GetFileHash(string fname, HttpServerUtilityBase server)
        {
            var localPath = server.MapPath(fname.Replace('/', '\\'));

            return GetFileHash(localPath);
        }

        private static Guid GetFileHash(string localPath)
        {
            var lastWriteTime = File.GetLastWriteTime(localPath);
            var lastWriteTimeStream = lastWriteTime.ToString(CultureInfo.InvariantCulture).ToStream();

            byte[] hashBytes;
            lock (_syncRoot)
            {
                hashBytes = Md5.ComputeHash(lastWriteTimeStream);
            }

            var hash = new Guid(hashBytes);

            Guid check;
            if (!FileHash.TryGetValue(localPath, out check))
            {
                FileHash.Add(localPath, hash);
            }
            else if (check != hash)
            {
                FileHash[localPath] = hash;
            }

            return hash;
        }

    }
}