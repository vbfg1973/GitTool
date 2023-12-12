using CodeTool.Infrastructure.Git;
using CodeTool.Infrastructure.Git.Models;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;
using MediatR;

namespace CodeTool.Domain.Features.Repositories.GitLogs.GetPage
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