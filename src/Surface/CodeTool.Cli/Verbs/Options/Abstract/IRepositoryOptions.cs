using CommandLine;

namespace CodeTool.Cli.Verbs.Options.Abstract
{
    public interface IRepositoryOptions
    {
        [Option('p', nameof(RepositoryPath), Required = true, HelpText = "Path to repository")]
        public string RepositoryPath { get; init; }
    }
}