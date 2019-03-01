﻿using System.Threading.Tasks;

namespace LocCheck.Services
{
    public class PullRequestHandler : IPullRequestHandler
    {
        private readonly IPullRequestInfoProvider _prInfoProvider;
        private readonly IRepositorySettingsProvider _repositorySettingsProvider;
        private readonly IPullRequestPolicy _pullRequestPolicy;
        private readonly ICommitStatusWriter _statusWriter;

        public PullRequestHandler(IPullRequestInfoProvider prInfoProvider,
            IRepositorySettingsProvider repositorySettingsProvider,
            IPullRequestPolicy pullRequestPolicy,
            ICommitStatusWriter statusWriter)
        {
            _prInfoProvider = prInfoProvider;
            _pullRequestPolicy = pullRequestPolicy;
            _statusWriter = statusWriter;
            _repositorySettingsProvider = repositorySettingsProvider;
        }

        public async Task HandleWebhookEventAsync(PullRequestContext context)
        {
            context.PullRequestInfoProvider = _prInfoProvider;

            context.Log.Verbose($"Getting repository settings for pull request #{context.Payload.Number}");
            context.RepositorySettings = await _repositorySettingsProvider.GetRepositorySettingsAsync(context);

            context.Log.Verbose($"Evaluating status for pull request #{context.Payload.Number}...");
            var (state, description) = await _pullRequestPolicy.GetStatusAsync(context);
            context.Log.Info($"Status for pull request #{context.Payload.Number} is '{state}' ({description})");

            context.Log.Verbose($"Writing commit status for pull request #{context.Payload.Number}...");
            await _statusWriter.WriteCommitStatusAsync(context, state, description);
        }
    }
}
