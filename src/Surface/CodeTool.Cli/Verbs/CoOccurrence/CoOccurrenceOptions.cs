using CodeTool.Cli.Verbs.Options.Abstract;
using CommandLine;

namespace CodeTool.Cli.Verbs.CoOccurrence
{
    [Verb("cooccurrence", HelpText = "Count all commits on the branch")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CoOccurrenceOptions : IRepositoryOptions, ICsvOptions
    {
        public string RepositoryPath { get; init; } = null!;
        public string CsvFile { get; init; } = null!;
        
        [Option('m', nameof(MaxFileCount), HelpText = "Maximum file count of commit to take into account")]
        public int MaxFileCount { get; set; }
    }
}