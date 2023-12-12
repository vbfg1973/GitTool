using CodeTool.Cli.Verbs.Options.Abstract;
using CommandLine;

namespace CodeTool.Cli.Verbs.Lineage
{
    [Verb("lineage", HelpText = "Commit lineage")]
    public class LineageOptions : IRepositoryOptions
    {
        public string RepositoryPath { get; init; } = null!;
    }
}