using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LocCheck.Services;

namespace LocCheck
{
    internal class GithubSettings
    {
        public GithubSettings(string appId, string privateKey, string statusContext)
        {
            AppId = appId;
            PrivateKey = privateKey;
            StatusContext = statusContext;
            RsaParameters =
                string.IsNullOrEmpty(privateKey)
                ? default
                : CryptoHelper.GetRsaParameters(privateKey);
        }

        public string AppId { get; }
        public string PrivateKey { get; }
        public RSAParameters RsaParameters { get; }
        public string StatusContext { get; set; }
    }
}
