using CommandLine;

namespace GitTool.Cli.Verbs.Correlation
{
    [Verb("correlation")]
    public class CorrelationOptions
    {
        // ReSharper disable once ClassNeverInstantiated.Global        public sealed class CommitCsvOptions
        [Option('p', nameof(RepositoryPath))] public string RepositoryPath { get; set; } = null!;

        [Option('c', nameof(CsvFile))] public string CsvFile { get; set; } = null!;
    }
}