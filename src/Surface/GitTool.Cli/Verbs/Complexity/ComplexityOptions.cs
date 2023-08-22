using CommandLine;
using GitTool.Cli.Verbs.Abstract;

namespace GitTool.Cli.Verbs.Complexity
{
    [Verb("complexity")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class ComplexityOptions : IRepositoryOptions, ICsvOptions
    {
        public string RepositoryPath { get; init; } = null!;

        public string CsvFile { get; init; } = null!;
    }
}