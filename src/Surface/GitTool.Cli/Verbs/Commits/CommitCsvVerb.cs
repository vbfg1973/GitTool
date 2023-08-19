using GitTool.Domain.Features.Repositories.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GitTool.Cli.Verbs.Commits
{
    public class CommitCsvVerb
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CommitCsvVerb> _logger;

        public CommitCsvVerb(IMediator mediator, ILogger<CommitCsvVerb> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Run(CommitCsvOptions commitCsvOptions)
        {
            var request = new FullListOfAllCommitsFromGitHistoryToCsv(commitCsvOptions.RepositoryPath, commitCsvOptions.CsvFile);

            await _mediator.Send(request);
        }
    }
}