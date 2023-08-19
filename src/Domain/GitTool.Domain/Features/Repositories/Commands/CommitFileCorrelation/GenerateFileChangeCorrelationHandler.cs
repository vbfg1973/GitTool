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
            var dictionary = _gitService
                .GetAllCommits(request.RepositoryPath)
                .SelectMany(x => x.Files)
                .Select(x => x.Path)
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var kvp in dictionary.OrderByDescending(d => d.Value))
            {
                Console.WriteLine($"{kvp.Key} {kvp.Value}");
            }
        }
    }
}