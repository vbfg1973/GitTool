using CommandLine;
using GitTool.Cli.Verbs.Options.Abstract;

namespace GitTool.Cli.Verbs.CoOccurrence
{
    [Verb("cooccurrence", HelpText = "Count all commits on the branch")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CoOccurrenceOptions : IRepositoryOptions, ICsvOptions
    {
        public string RepositoryPath { get; init; } = null!;
        public string CsvFile { get; init; } = null!;
    }
}