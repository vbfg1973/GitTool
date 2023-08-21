using GitTool.Domain.Features.Repositories.Commands.Complexity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GitTool.Cli.Verbs.Complexity
{
    public class ComplexityVerb
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ComplexityVerb> _logger;

        public ComplexityVerb(IMediator mediator, ILogger<ComplexityVerb> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Run(ComplexityOptions options)
        {
            await _mediator.Send(new ComplexityOverCommits(options.RepositoryPath, options.CsvFile));
        }
    }
}