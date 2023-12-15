using CodeTool.Infrastructure.Git.Models;
using CodeTool.Infrastructure.Git.Parsers.Abstract;

namespace CodeTool.Infrastructure.Git.Parsers.GitLineageParsers
{
    public interface IGitLineageParserMultiple : IParserMultiple<GitCommitLineage> { }
}