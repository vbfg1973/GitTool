using CommandLine;
using GitTool.Cli.Verbs.Options.Abstract;

namespace GitTool.Cli.Verbs.Commits
{
    [Verb("commit", HelpText = "Dump all commit data with filenames and changeKind information to CSV")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CommitOptions : IRepositoryOptions, ICsvOptions
    {
        public string CsvFile { get; init; } = null!;

        public string RepositoryPath { get; init; } = null!;
    }
}