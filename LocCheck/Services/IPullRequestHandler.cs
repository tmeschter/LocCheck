using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocCheck.Services
{
    internal interface IPullRequestHandler
    {
        Task HandleWebhookEventAsync(PullRequestContext context);
    }
}
