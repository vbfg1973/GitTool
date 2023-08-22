using System.Collections.Immutable;
using GitTool.Infrastructure.Git.Commands.Abstract;
using GitTool.Infrastructure.Git.Models;

namespace GitTool.Infrastructure.Git.Commands.CommitsReverseTopographical
{
    public class GitCommitReverseTopographicalCommandRunner
    {
        private readonly IProcessCommandRunner _processCommandRunner;

        public GitCommitReverseTopographicalCommandRunner(IProcessCommandRunner processCommandRunner)
        {
            _processCommandRunner = processCommandRunner;
        }

        public IEnumerable<string> Run(string path)
        {
            path = GitCommitHelpers.CurrentWorkingDirectoryOrNominatedPath(path);


            return _processCommandRunner.Runner(new CommitsReverseTopographicalGitCommandLineArguments(path));
        }

        private record CommitsReverseTopographicalGitCommandLineArguments : AbstractGitCommandLineArguments
        {
            public CommitsReverseTopographicalGitCommandLineArguments(string path)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "rev-list",
                    "--reverse",
                    "--topo-order",
                    "--first-parent",
                    "HEAD"
                }.ToImmutableList();
            }
        }
    }
}