using CommandLine;

namespace GitTool.Cli.Verbs.Commits
{
    [Verb("commitcsv")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class CommitCsvOptions
    {
        [Option('p', nameof(RepositoryPath))]
        public string RepositoryPath { get; set; } = null!;

        [Option('c', nameof(CsvFile))]
        public string CsvFile { get; set; } = null!;
    }
}