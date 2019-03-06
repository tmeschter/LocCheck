using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using LocCheck.Extensions;
using Microsoft.IdentityModel.Tokens;
using Octokit;

namespace LocCheck.Services
{
    internal class GithubAppTokenService : IGithubAppTokenService
    {
        private readonly IGithubSettingsProvider _settingsProvider;

        public GithubAppTokenService(IGithubSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public Task<string> GetTokenForApplicationAsync()
        {
            var settings = _settingsProvider.Settings;

            var key = new RsaSecurityKey(settings.RsaParameters);
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(claims: new[]
                {
                    new Claim("iat", now.ToUnixTimeStamp().ToString(), ClaimValueTypes.Integer),
                    new Claim("exp", now.AddMinutes(10).ToUnixTimeStamp().ToString(), ClaimValueTypes.Integer),
                    new Claim("iss", settings.AppId)
                },
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(jwt);
        }

        public async Task<string> GetTokenForInstallationAsync(long installationId)
        {
            var appToken = await GetTokenForApplicationAsync();

            var appClient = new GitHubClient(new ProductHeaderValue("LocCheck"))
            {
                Credentials = new Credentials(appToken, AuthenticationType.Bearer)
            };

            var installationToken = await appClient.GitHubApps.CreateInstallationToken(installationId);

            return installationToken.Token;
        }
    }
}
