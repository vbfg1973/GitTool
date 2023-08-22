using CommandLine;
using GitTool.Cli.Verbs.Abstract;

namespace GitTool.Cli.Verbs.ReverseTopographical
{
    [Verb("reversetopo", HelpText = "List sha IDs in reverse topographical order")]
    public class ReverseTopographicalOptions : IRepositoryOptions
    {
        public string RepositoryPath { get; init; } = null!;
    }
}