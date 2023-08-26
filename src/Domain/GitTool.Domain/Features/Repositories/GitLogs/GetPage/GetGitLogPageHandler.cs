using GitTool.Infrastructure.Git;
using GitTool.Infrastructure.Git.Models;
using MediatR;

namespace GitTool.Domain.Features.Repositories.GitLogs.GetPage
{
    public class GetGitLogPageHandler : IStreamRequestHandler<GetGitLogPage, GitLog>
    {
        private readonly IGitService _gitService;

        public GetGitLogPageHandler(IGitService gitService)
        {
            _gitService = gitService;
        }

        public IAsyncEnumerable<GitLog> Handle(GetGitLogPage request, CancellationToken cancellationToken)
        {
            return _gitService.GetLogsWithFiles(request.RepositoryDetails,
                request.PageParameters,
                cancellationToken);
        }
    }
}