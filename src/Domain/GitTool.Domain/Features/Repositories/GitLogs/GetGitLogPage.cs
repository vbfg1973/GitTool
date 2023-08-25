using GitTool.Infrastructure.Git;
using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;
using MediatR;

namespace GitTool.Domain.Features.Repositories.GitLogs
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GetGitLogPage : IStreamRequest<GitLog>
    {
        public GetGitLogPage(RepositoryDetails repositoryDetails, GitPageParameters pageParameters)
        {
            RepositoryDetails = repositoryDetails;
            PageParameters = pageParameters;
        }

        public RepositoryDetails RepositoryDetails { get; init; }
        public GitPageParameters PageParameters { get; init; }
    }
}