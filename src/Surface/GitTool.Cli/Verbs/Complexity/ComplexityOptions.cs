
using CommandLine;

namespace GitTool.Cli.Verbs.Complexity
{
    [Verb("complexity")]
    public class ComplexityOptions
    {
        [Option('p', nameof(RepositoryPath), Required = true)]
        public string RepositoryPath { get; set; } = null!;
    }
}