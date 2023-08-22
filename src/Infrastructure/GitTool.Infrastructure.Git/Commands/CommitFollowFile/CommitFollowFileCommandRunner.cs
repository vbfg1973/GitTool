using System.Collections.Immutable;
using GitTool.Infrastructure.Git.Commands.Abstract;
using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.Parsers.GitLog;

namespace GitTool.Infrastructure.Git.Commands.CommitFollowFile
{
    /// <summary>
    ///     Extract GitCommit objects from current branch of a repository where the nominated file was modified
    /// </summary>
    public class CommitFollowFileCommandRunner
    {
        private readonly IProcessCommandRunner _commandRunner;

        private readonly IGitLogParser _gitLogParser;

        public CommitFollowFileCommandRunner(IGitLogParser gitLogParser, IProcessCommandRunner commandRunner)
        {
            _commandRunner = commandRunner;
            _gitLogParser = gitLogParser;
        }

        /// <summary>
        ///     Retrieve all commits where the file was modified
        /// </summary>
        /// <param name="fileToFollow"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEnumerable<GitCommitDetails> Run(string fileToFollow, string path = ".")
        {
            var arguments = new CommitFollowFileCommandLineArguments(path, fileToFollow);
            path = GitCommitHelpers.CurrentWorkingDirectoryOrNominatedPath(path);

            var lines = _commandRunner.Runner(arguments);

            return _gitLogParser.GitCommitDetailsParser(lines);
        }

        private record CommitFollowFileCommandLineArguments : AbstractGitCommandLineArguments
        {
            public CommitFollowFileCommandLineArguments(string path, string fileToFollow)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "log",
                    "--follow",
                    "--",
                    fileToFollow
                }.ToImmutableList();
            }
        }
    }
}