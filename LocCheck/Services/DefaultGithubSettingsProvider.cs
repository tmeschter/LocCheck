using System.Configuration;

namespace LocCheck.Services
{
    internal class DefaultGithubSettingsProvider : IGithubSettingsProvider
    {
        public GithubSettings Settings { get; } = LoadSettings();

        private static GithubSettings LoadSettings()
        {
            return new GithubSettings(
                ConfigurationManager.AppSettings["GithubAppId"],
                ConfigurationManager.AppSettings["GithubPrivateKey"],
                ConfigurationManager.AppSettings["GithubStatusContext"]);
        }
    }
}
