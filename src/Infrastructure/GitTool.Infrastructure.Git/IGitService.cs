using GitTool.Infrastructure.Git.Models;

namespace GitTool.Infrastructure.Git
{
    public interface IGitService
    {
        IEnumerable<GitCommitDetails> GetAllCommits(string repositoryPath);

        IEnumerable<GitCommitDetails> FollowFile(string repositoryPath, string filePath);

        string CommitFileContent(string repositoryPath, string sha, string filePath);

        GitCommitParents Parents(string repositoryPath, string sha);

        IEnumerable<string> ReverseTopographicalShaIds(string repositoryPath);
        int CountCommits(string repositoryPath);
    }
}