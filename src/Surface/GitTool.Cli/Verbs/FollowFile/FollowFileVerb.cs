using GitTool.Domain.Features.Repositories.Commands.FollowFile;
using MediatR;

namespace GitTool.Cli.Verbs.FollowFile
{
    public class FollowFileVerb
    {
        private readonly IMediator _mediator;

        public FollowFileVerb(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Run(FollowFileOptions options)
        {
            await _mediator.Send(new FollowFileRequest(options.RepositoryPath, options.FileToTrack, options.CsvFile));
        }
    }
}