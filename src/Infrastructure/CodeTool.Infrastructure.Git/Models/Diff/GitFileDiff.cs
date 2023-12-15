using CodeTool.Infrastructure.Git.Parsers.GitDiff;

namespace CodeTool.Infrastructure.Git.Models.Diff
{
    public record GitFileDiff(string FromSha, string ToSha, string Path, string OldPath, GitDiffType GitDiffType);
}