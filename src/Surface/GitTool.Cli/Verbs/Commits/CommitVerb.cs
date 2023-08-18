using GitTool.Domain.Features.Repositories.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GitTool.Cli.Verbs.Commits
{
    public class CommitVerb
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CommitVerb> _logger;

        public CommitVerb(IMediator mediator, ILogger<CommitVerb> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Run(CommitOptions commitOptions)
        {
            var request = new GetFullListOfAllFilesFromGitHistory(commitOptions.RepositoryPath);

            await _mediator.Send(request);
        }
    }
}