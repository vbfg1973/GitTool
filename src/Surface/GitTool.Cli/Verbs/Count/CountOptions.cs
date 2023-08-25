using CommandLine;
using GitTool.Cli.Verbs.Options.Abstract;

namespace GitTool.Cli.Verbs.Count
{
    [Verb("count", HelpText = "Count all commits on the branch")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CountOptions : IRepositoryOptions
    {
        public string RepositoryPath { get; init; } = null!;
    }
}