using System.Collections.Generic;

namespace LocCheck
{
    public class PullRequestInfo
    {
        /// <summary>
        /// The branch the PR is trying to merge change into.
        /// </summary>
        public string Base { get; set; }
        /// <summary>
        /// The set of files modified by the PR.
        /// </summary>
        public IEnumerable<string> Files { get; set; }
        /// <summary>
        /// The set of comments on the PR.
        /// </summary>
        public IEnumerable<string> Comments { get; set; }
    }
}