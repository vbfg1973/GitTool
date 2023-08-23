using System.Collections.Immutable;
using GitTool.Infrastructure.Git.Commands.Abstract;

namespace GitTool.Infrastructure.Git.Commands.CommitsReverse
{
    public class GitCommitsReverseCommandRunner
    {
        private readonly IProcessCommandRunner _processCommandRunner;

        public GitCommitsReverseCommandRunner(IProcessCommandRunner processCommandRunner)
        {
            _processCommandRunner = processCommandRunner;
        }

        public IEnumerable<string> Run(string path)
        {
            path = GitCommitHelpers.CurrentWorkingDirectoryOrNominatedPath(path);

            return _processCommandRunner.Runner(new CommitsReverseGitCommandLineArguments(path));
        }

        private record CommitsReverseGitCommandLineArguments : AbstractGitCommandLineArguments
        {
            public CommitsReverseGitCommandLineArguments(string path, int n = int.MaxValue, int skip = 0)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "rev-list",
                    "--reverse",
                    $"--skip={skip}",
                    $"-n={n}",
                    "HEAD"
                }.ToImmutableList();
            }
        }
    }
}