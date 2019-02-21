using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;
using Octokit;

namespace LocCheck
{
    public class PullRequestContext
    {
        public PullRequestContext(PullRequestEventPayload payload, IConnection githubConnection, TraceWriter log)
        {
            Payload = payload;
            GithubConnection = githubConnection;
            Log = log;
        }

        public PullRequestEventPayload Payload { get; }
        public IConnection GithubConnection { get; }
        public TraceWriter Log { get; }
        public PullRequestInfo PullRequestInfo { get; set; }
        public RepositorySettings RepositorySettings { get; set; }
    }
}
