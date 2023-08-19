using GitTool.Domain.Helpers;
using GitTool.Infrastructure.Git;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GitTool.Domain.Features.Repositories.Commands.CommitFileCorrelation
{
    public class GenerateFileChangeCorrelationHandler : IRequestHandler<GenerateFileChangeCorrelation>
    {
        private readonly IGitService _gitService;
        private readonly ILogger<GenerateFileChangeCorrelationHandler> _logger;

        public GenerateFileChangeCorrelationHandler(IGitService gitService,
            ILogger<GenerateFileChangeCorrelationHandler> logger)
        {
            _gitService = gitService;
            _logger = logger;
        }

        public async Task Handle(GenerateFileChangeCorrelation request, CancellationToken cancellationToken)
        {
            var commits = _gitService.GetAllCommits(request.RepositoryPath);

            var correlations = new Correlations();

            foreach (var commit in commits) correlations.AddSet(commit.Files.Select(x => x.Path));

            await CsvHelpers.WriteCsvAsync(correlations.CorrelationData(), request.CsvFile);
        }
    }
}