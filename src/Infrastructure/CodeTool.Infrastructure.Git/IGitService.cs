using System.Runtime.CompilerServices;
using CodeTool.Infrastructure.Git.Models;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Infrastructure.Git
{
    public interface IGitService
    {
        Task<int> CountCommits(RepositoryDetails repositoryDetails, CancellationToken ctx);

        IAsyncEnumerable<GitLog> GetLogs(RepositoryDetails repositoryDetails, GitPageParameters pageParameters,
            CancellationToken ctx);

        IAsyncEnumerable<GitLog> GetLogsWithFiles(RepositoryDetails repositoryDetails, GitPageParameters pageParameters,
            CancellationToken ctx);

        IAsyncEnumerable<GitCommitLineage> GetLineage(RepositoryDetails repositoryDetails, CancellationToken ctx);
    }
}