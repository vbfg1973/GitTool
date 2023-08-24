using CommandLine;

namespace GitTool.Cli.Verbs.Abstract
{
    public interface IRepositoryOptions
    {
        [Option('p', nameof(RepositoryPath), Required = true, HelpText = "Path to repository")]
        public string RepositoryPath { get; init; }
    }
}