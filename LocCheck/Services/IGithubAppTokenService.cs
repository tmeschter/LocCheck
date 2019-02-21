using System.Threading.Tasks;

namespace LocCheck.Services
{
    public interface IGithubAppTokenService
    {
        Task<string> GetTokenForApplicationAsync();
        Task<string> GetTokenForInstallationAsync(long installationId);
    }
}