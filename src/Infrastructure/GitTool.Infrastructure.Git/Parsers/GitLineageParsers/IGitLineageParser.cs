using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.Parsers.Abstract;

namespace GitTool.Infrastructure.Git.Parsers.GitLineageParsers
{
    public interface IGitLineageParser : IParser<GitCommitLineage> { }
}