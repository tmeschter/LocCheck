using System.Threading.Tasks;

namespace LocCheck.Services
{
    public interface IPullRequestInfoProvider
    {
        Task<PullRequestInfo> GetPullRequestInfoAsync(PullRequestContext context);
    }
}