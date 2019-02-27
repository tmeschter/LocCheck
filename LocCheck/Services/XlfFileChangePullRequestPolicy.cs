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
        public (CommitState state, string description) GetStatus(PullRequestContext context)
        {
            if (!TouchesXlfFiles(context.PullRequestInfo.Files))
            {
                context.Log.Info("Pull request does not change .xlf files");
                return (CommitState.Success, "No localization changes.");
            }

            if (!context.RepositorySettings.ProtectedBranches.Contains(context.PullRequestInfo.Base))
            {
                context.Log.Info("Pull request is into unprotected branch.");
                context.Log.Info($"  Base branch: {context.PullRequestInfo.Base}");
                context.Log.Info($"  Protected branches: {string.Join(";", context.RepositorySettings.ProtectedBranches)}");
                return (CommitState.Success, $"Localization changes are allowed in this branch: {context.PullRequestInfo.Base}");
            }

            if (LocUnblockedViaComment(context.PullRequestInfo.Comments))
            {
                context.Log.Info("Pull request is explicitly unblocked.");
                return (CommitState.Success, "Localization changes have been explicitly unblocked.");
            }

            return (CommitState.Failure, "Localization changes are not allowed in this branch.");
        }

        private bool LocUnblockedViaComment(IEnumerable<string> comments)
        {
            return comments.Any(c => c == "Ublock LocCheck");
        }

        private bool TouchesXlfFiles(IEnumerable<string> files)
        {
            return files.Any(f => Path.GetExtension(f) == ".xlf");
        }
    }
}
