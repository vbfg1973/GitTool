using CommandLine;

namespace GitTool.Cli.Verbs.Complexity
{
    [Verb("complexity")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ComplexityOptions
    {
        [Option('p', nameof(RepositoryPath), Required = true)]
        public string RepositoryPath { get; set; } = null!;

        [Option('c', nameof(CsvFile), Required = true)]
        public string CsvFile { get; set; } = null!;
    }
}