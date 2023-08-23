using System.Collections.Immutable;
using GitTool.Infrastructure.Git.Commands.Abstract;

namespace GitTool.Infrastructure.Git.Commands.CommitsCount
{
    public class GitCommitsCountCommandRunner
    {
        private readonly IProcessCommandRunner _processCommandRunner;

        public GitCommitsCountCommandRunner(IProcessCommandRunner processCommandRunner)
        {
            _processCommandRunner = processCommandRunner;
        }

        public int Run(string path)
        {
            path = GitCommitHelpers.CurrentWorkingDirectoryOrNominatedPath(path);

            return Convert.ToInt32(_processCommandRunner.Runner(new CommitsCountGitCommandLineArguments(path)).First());
        }

        private record CommitsCountGitCommandLineArguments : AbstractGitCommandLineArguments
        {
            public CommitsCountGitCommandLineArguments(string path)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "rev-list",
                    "--count",
                    "HEAD"
                }.ToImmutableList();
            }
        }
    }
}