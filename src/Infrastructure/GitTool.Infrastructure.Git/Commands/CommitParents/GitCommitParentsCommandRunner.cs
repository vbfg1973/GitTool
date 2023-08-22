using System.Collections.Immutable;
using GitTool.Infrastructure.Git.Commands.Abstract;
using GitTool.Infrastructure.Git.Models;

namespace GitTool.Infrastructure.Git.Commands.CommitParents
{
    public class GitCommitParentsCommandRunner
    {
        private readonly IProcessCommandRunner _processCommandRunner;

        public GitCommitParentsCommandRunner(IProcessCommandRunner processCommandRunner)
        {
            _processCommandRunner = processCommandRunner;
        }

        public GitCommitParents Run(string shaId, string path)
        {
            path = GitCommitHelpers.CurrentWorkingDirectoryOrNominatedPath(path);

            return new GitCommitParents
            {
                Sha = shaId,
                Parents = _processCommandRunner.Runner(new CommitParentsGitCommandLineArguments(path, shaId)).ToList()
            };
        }

        private record CommitParentsGitCommandLineArguments : AbstractGitCommandLineArguments
        {
            public CommitParentsGitCommandLineArguments(string path, string shaId)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "rev-parse",
                    $"{shaId}^@"
                }.ToImmutableList();
            }
        }
    }
}