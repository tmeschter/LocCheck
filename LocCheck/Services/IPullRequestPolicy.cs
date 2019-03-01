using System.Threading.Tasks;
using Octokit;

namespace LocCheck.Services
{
    public interface IPullRequestPolicy
    {
        Task<(CommitState state, string description)> GetStatusAsync(PullRequestContext context);
    }
}