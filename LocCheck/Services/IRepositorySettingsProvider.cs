using System.Threading.Tasks;

namespace LocCheck.Services
{
    public interface IRepositorySettingsProvider
    {
        Task<RepositorySettings> GetRepositorySettingsAsync(PullRequestContext context);
    }
}