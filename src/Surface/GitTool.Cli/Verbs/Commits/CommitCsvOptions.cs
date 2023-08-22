using CommandLine;
using GitTool.Cli.Verbs.Abstract;

namespace GitTool.Cli.Verbs.Commits
{
    [Verb("commits")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class CommitCsvOptions : IRepositoryOptions, ICsvOptions
    {
        public string RepositoryPath { get; init; } = null!;

        public string CsvFile { get; init; } = null!;
    }
}