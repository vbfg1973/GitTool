using CommandLine;
using GitTool.Cli.Verbs.Abstract;

namespace GitTool.Cli.Verbs.Count
{
    [Verb("count", HelpText = "List sha IDs in reverse topographical order")]
    public class CountOptions : IRepositoryOptions
    {
        public string RepositoryPath { get; init; } = null!;
    }
}