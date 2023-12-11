using System.Runtime.CompilerServices;
using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Infrastructure.Git
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