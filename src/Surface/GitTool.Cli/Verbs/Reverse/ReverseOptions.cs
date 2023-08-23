using CommandLine;
using GitTool.Cli.Verbs.Abstract;

namespace GitTool.Cli.Verbs.Reverse
{
    [Verb("reverse", HelpText = "List sha IDs in reverse topographical order")]
    public class ReverseOptions : IRepositoryOptions
    {
        public string RepositoryPath { get; init; } = null!;
    }
}