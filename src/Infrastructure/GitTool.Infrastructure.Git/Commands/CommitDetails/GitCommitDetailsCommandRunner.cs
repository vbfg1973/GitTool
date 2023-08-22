using System.Collections.Immutable;
using GitTool.Infrastructure.Git.Commands.Abstract;
using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.Parsers.GitLog;

namespace GitTool.Infrastructure.Git.Commands.CommitDetails
{
    /// <summary>
    ///     Extract GitCommit objects from current branch of a repository
    /// </summary>
    public class GitCommitDetailsCommandRunner
    {
        private readonly IProcessCommandRunner _commandRunner;

        private readonly IGitLogParser _gitLogParser;

        public GitCommitDetailsCommandRunner(IGitLogParser gitLogParser, IProcessCommandRunner commandRunner)
        {
            _commandRunner = commandRunner;
            _gitLogParser = gitLogParser;
        }

        /// <summary>
        ///     Extract GitCommitDetails from the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEnumerable<GitCommitDetails> Run(string path = ".")
        {
            var arguments = new CommitDetailsGitCommandLineArguments(path);
            path = GitCommitHelpers.CurrentWorkingDirectoryOrNominatedPath(path);

            var lines = _commandRunner.Runner(arguments);

            return _gitLogParser.GitCommitDetailsParser(lines);
        }

        private record CommitDetailsGitCommandLineArguments : AbstractGitCommandLineArguments
        {
            public CommitDetailsGitCommandLineArguments(string path)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "log",
                    "--name-status"
                }.ToImmutableList();
            }
        }
    }
}