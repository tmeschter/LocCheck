using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace LocCheck.Services
{
    internal class PullRequestInfoProvider : IPullRequestInfoProvider
    {
        public async Task<PullRequestInfo> GetPullRequestInfoAsync(PullRequestContext context)
        {
            context.Log.Verbose($"Getting files for pull request #{context.Payload.Number}");
            var files = await GetFilesAsync(context);
            context.Log.Verbose($"Getting comments for pull request #{context.Payload.Number}");
            var comments = await GetCommentsAsync(context);
            return new PullRequestInfo
            {
                Base = context.Payload.PullRequest.Base.Ref,
                Files = files.Select(f => f.FileName),
                Comments = comments.Select(c => c.Body)
            };
        }

        private Task<IReadOnlyList<PullRequestFile>> GetFilesAsync(PullRequestContext context)
        {
            var client = new GitHubClient(context.GithubConnection);
            return client.PullRequest.Files(context.Payload.Repository.Id, context.Payload.PullRequest.Number);
        }

        private Task<IReadOnlyList<PullRequestReviewComment>> GetCommentsAsync(PullRequestContext context)
        {
            var client = new GitHubClient(context.GithubConnection);
            return client.PullRequest.ReviewComment.GetAll(context.Payload.Repository.Id, context.Payload.PullRequest.Number);
        }
    }
}
