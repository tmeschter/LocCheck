using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace LocCheck.Services
{
    internal class XlfFileChangePullRequestPolicy : IPullRequestPolicy
    {
        public async Task<(CommitState state, string description)> GetStatusAsync(PullRequestContext context)
        {
            var baseBranch = await context.PullRequestInfoProvider.GetBaseAsync(context);
            if (!context.RepositorySettings.ProtectedBranches.Contains(baseBranch))
            {
                context.Log.Info("Pull request is into unprotected branch.");
                context.Log.Info($"  Base branch: {baseBranch}");
                context.Log.Info($"  Protected branches: {string.Join(";", context.RepositorySettings.ProtectedBranches)}");
                return (CommitState.Success, $"Localization changes are allowed in branch {baseBranch}.");
            }

            var files = await context.PullRequestInfoProvider.GetFilesAsync(context);
            if (!TouchesXlfFiles(files))
            {
                context.Log.Info("Pull request does not change .xlf files");
                return (CommitState.Success, "No localization changes.");
            }

            var comments = await context.PullRequestInfoProvider.GetCommentsAsync(context);

            if (LocUnblockedViaComment(comments))
            {
                context.Log.Info("Pull request is explicitly unblocked.");
                return (CommitState.Success, "Localization changes have been explicitly unblocked.");
            }

            return (CommitState.Failure, "Localization changes are not allowed in this branch.");
        }

        private bool LocUnblockedViaComment(IEnumerable<string> comments)
        {
            return comments.Any(c => c == "Unblock LocCheck");
        }

        private bool TouchesXlfFiles(IEnumerable<string> files)
        {
            return files.Any(f => Path.GetExtension(f) == ".xlf");
        }
    }
}
