using CommandLine;
using GitTool.Cli.Verbs.Abstract;

namespace GitTool.Cli.Verbs.Correlation
{
    [Verb("correlation")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class CorrelationOptions : IRepositoryOptions, ICsvOptions
    {
        public string RepositoryPath { get; init; } = null!;

        public string CsvFile { get; init; } = null!;
    }
}