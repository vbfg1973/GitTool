using GitTool.Infrastructure.Git.Commands;
using GitTool.Infrastructure.Git.Commands.CommitDetails;
using GitTool.Infrastructure.Git.Commands.CommitFileContent;
using GitTool.Infrastructure.Git.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GitTool.Infrastructure.Git
{
    public class GitService : IGitService
    {
        private readonly IServiceProvider _serviceProvider;

        public GitService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<GitCommitDetails> GetAllCommits(string repositoryPath)
        {
            var gitCommitDetailsCommandRunner =
                new GitCommitDetailsCommandRunner(_serviceProvider.GetService<IProcessCommandRunner>()!);

            return gitCommitDetailsCommandRunner.Run(repositoryPath);
        }

        public string CommitFileContent(string repositoryPath, string sha, string filePath)
        {
            var commitFileContentCommandRunner =
                new CommitFileContentCommandRunner(_serviceProvider.GetService<IProcessCommandRunner>()!);

            return commitFileContentCommandRunner.Run(sha, filePath, repositoryPath);
        }
    }
}