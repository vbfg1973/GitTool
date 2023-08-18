using GitTool.Infrastructure.Git;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GitTool.Domain.Features.Repositories.Queries
{
    public class GetFullListOfAllFilesFromGitHistoryHandler : IRequestHandler<GetFullListOfAllFilesFromGitHistory>
    {
        private readonly IGitService _gitService;
        private readonly ILogger<GetFullListOfAllFilesFromGitHistoryHandler> _logger;

        public GetFullListOfAllFilesFromGitHistoryHandler(IGitService gitService,
            ILogger<GetFullListOfAllFilesFromGitHistoryHandler> logger)
        {
            _gitService = gitService;
            _logger = logger;
        }

        public async Task Handle(GetFullListOfAllFilesFromGitHistory request, CancellationToken cancellationToken)
        {
            var hashSet = new HashSet<string>();

            foreach (var gitCommit in _gitService.GetAllCommits(request.RepositoryPath))
            {
                foreach (var fileDetail in gitCommit.Files)
                {
                    hashSet.Add(fileDetail.Path);
                }
            }

            Console.WriteLine(hashSet.Count);
        }
    }
}