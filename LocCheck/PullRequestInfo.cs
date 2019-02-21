using System.Collections.Generic;

namespace LocCheck
{
    public class PullRequestInfo
    {
        public string Title { get; set; }
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<string> CommitMessages { get; set; }
        public string SourceRepositoryFullName { get; set; }
        public string Head { get; set; }
    }
}