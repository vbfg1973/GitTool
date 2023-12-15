using CodeTool.Infrastructure.Git.Models.Diff;
using CodeTool.Infrastructure.Git.Parsers.Abstract;

namespace CodeTool.Infrastructure.Git.Parsers.GitDiff
{
    public interface IGitDiffParser : IParser<GitFileDiff>
    {
        
    }
}