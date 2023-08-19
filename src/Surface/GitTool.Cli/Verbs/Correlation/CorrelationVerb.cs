using GitTool.Domain.Features.Repositories.Commands.CommitFileCorrelation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GitTool.Cli.Verbs.Correlation
{
    public class CorrelationVerb
    {
        private readonly ILogger<CorrelationVerb> _logger;
        private readonly IMediator _mediator;

        public CorrelationVerb(IMediator mediator, ILogger<CorrelationVerb> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Run(CorrelationOptions correlationOptions)
        {
            Console.WriteLine("Starting");
            var request =
                new GenerateFileChangeCorrelation(correlationOptions.RepositoryPath, correlationOptions.CsvFile);

            await _mediator.Send(request);
        }
    }
}