using GitTool.Infrastructure.Git.Models;

namespace GitTool.Infrastructure.Git
{
    public interface IGitService
    {
        IEnumerable<GitCommitDetails> GetAllCommits(string repositoryPath);
    }
}