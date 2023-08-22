using GitTool.Infrastructure.Git.Commands;
using GitTool.Infrastructure.Git.Commands.CommitDetails;
using GitTool.Infrastructure.Git.Commands.CommitFileContent;
using GitTool.Infrastructure.Git.Commands.CommitFollowFile;
using GitTool.Infrastructure.Git.Commands.CommitParents;
using GitTool.Infrastructure.Git.Commands.CommitsReverseTopographical;
using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.Parsers.GitLog;
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
                new GitCommitDetailsCommandRunner(
                    _serviceProvider.GetService<IGitLogParser>()!,
                    _serviceProvider.GetService<IProcessCommandRunner>()!);

            return gitCommitDetailsCommandRunner.Run(repositoryPath);
        }

        public string CommitFileContent(string repositoryPath, string sha, string filePath)
        {
            var commitFileContentCommandRunner =
                new CommitFileContentCommandRunner(_serviceProvider.GetService<IProcessCommandRunner>()!);

            return commitFileContentCommandRunner.Run(sha, filePath, repositoryPath);
        }

        public IEnumerable<GitCommitDetails> FollowFile(string repositoryPath, string filePath)
        {
            var followFileCommandRunner =
                new CommitFollowFileCommandRunner(
                    _serviceProvider.GetService<IGitLogParser>()!,
                    _serviceProvider.GetService<IProcessCommandRunner>()!);

            return followFileCommandRunner.Run(filePath, repositoryPath);
        }

        public GitCommitParents Parents(string repositoryPath, string sha)
        {
            var parentsCommandRunner =
                new GitCommitParentsCommandRunner(_serviceProvider.GetService<IProcessCommandRunner>()!);

            return parentsCommandRunner.Run(sha, repositoryPath);
        }
        
        public IEnumerable<string> ReverseTopographicalShaIds(string repositoryPath)
        {
            var parentsCommandRunner =
                new GitCommitReverseTopographicalCommandRunner(_serviceProvider.GetService<IProcessCommandRunner>()!);

            return parentsCommandRunner.Run(repositoryPath);
        }
    }
}