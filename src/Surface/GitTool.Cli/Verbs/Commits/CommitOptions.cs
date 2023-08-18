using CommandLine;

namespace GitTool.Cli.Verbs.Commits
{
    [Verb("commit")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class CommitOptions
    {
        [Option('p', nameof(RepositoryPath))]
        public string RepositoryPath { get; set; } = null!;
    }
}