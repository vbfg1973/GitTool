using CommandLine;
using GitTool.Cli.Verbs.Options.Abstract;

namespace GitTool.Cli.Verbs.Lineage
{
    [Verb("lineage", HelpText = "Commit lineage")]
    public class LineageOptions : IRepositoryOptions
    {
        public string RepositoryPath { get; init; } = null!;
    }
}