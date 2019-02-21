using System.Threading.Tasks;
using Octokit;

namespace LocCheck.Services
{
    public interface ICommitStatusWriter
    {
        Task WriteCommitStatusAsync(PullRequestContext context, CommitState state, string description);
    }
}