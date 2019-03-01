using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocCheck.Services
{
    public interface IPullRequestInfoProvider
    {
        /// <summary>
        /// The branch the PR is trying to merge change into.
        /// </summary>
        Task<string> GetBaseAsync(PullRequestContext context);

        /// <summary>
        /// The set of files modified by the PR.
        /// </summary>
        Task<IEnumerable<string>> GetFilesAsync(PullRequestContext context);

        /// <summary>
        /// The set of comments on the PR.
        /// </summary>
        Task<IEnumerable<string>> GetCommentsAsync(PullRequestContext context);
    }
}