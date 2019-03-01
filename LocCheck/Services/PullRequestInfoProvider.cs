using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace LocCheck.Services
{
    internal class PullRequestInfoProvider : IPullRequestInfoProvider
    {
        public Task<string> GetBaseAsync(PullRequestContext context)
        {
            return Task.FromResult(context.Payload.PullRequest.Base.Ref);
        }

        public async Task<IEnumerable<string>> GetFilesAsync(PullRequestContext context)
        {
            context.Log.Verbose($"Getting files for pull request #{context.Payload.Number}");

            var client = new GitHubClient(context.GithubConnection);
            var rawFiles = await client.PullRequest.Files(context.Payload.Repository.Id, context.Payload.PullRequest.Number);
            var files = rawFiles.Select(f => f.FileName);

            return files;
        }

        public async Task<IEnumerable<string>> GetCommentsAsync(PullRequestContext context)
        {
            context.Log.Verbose($"Getting comments for pull request #{context.Payload.Number}");

            var client = new GitHubClient(context.GithubConnection);
            var rawComments = await client.Issue.Comment.GetAllForIssue(context.Payload.Repository.Id, context.Payload.PullRequest.Number);
            var comments = rawComments.Select(c => c.Body);

            return comments;
        }
    }
}
