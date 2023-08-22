using System.Collections.Immutable;
using GitTool.Infrastructure.Git.Commands.Abstract;

namespace GitTool.Infrastructure.Git.Commands.CommitFileContent
{
    public class CommitFileContentCommandRunner
    {
        private readonly IProcessCommandRunner _processCommandRunner;

        public CommitFileContentCommandRunner(IProcessCommandRunner processCommandRunner)
        {
            _processCommandRunner = processCommandRunner;
        }

        public string Run(string sha, string filePath, string path = ".")
        {
            path = GitCommitHelpers.CurrentWorkingDirectoryOrNominatedPath(path);
            return string.Join("\n",
                _processCommandRunner.Runner(new CommitFileContentGitCommandLineArguments(path, sha, filePath))
                    .ToList());
        }

        private record CommitFileContentGitCommandLineArguments : AbstractGitCommandLineArguments
        {
            public CommitFileContentGitCommandLineArguments(string path, string sha, string filePath)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "show",
                    $"{sha}:{filePath}"
                }.ToImmutableList();
            }
        }
    }
}