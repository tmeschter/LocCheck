using Octokit;

namespace LocCheck.Services
{
    public interface IPullRequestPolicy
    {
        (CommitState state, string description) GetStatus(PullRequestContext context);
    }
}