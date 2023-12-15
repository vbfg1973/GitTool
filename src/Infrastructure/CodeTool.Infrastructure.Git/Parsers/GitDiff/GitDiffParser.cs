using System.Text.RegularExpressions;
using CodeTool.Infrastructure.Git.Models.Diff;

namespace CodeTool.Infrastructure.Git.Parsers.GitDiff
{
    public enum GitDiffType
    {
        Added,
        Modified,
        Renamed,
        Deleted
    }

    public partial class GitDiffParser : IGitDiffParser
    {
        private static readonly Regex MatchRunLength = GenerateDiffRunLengthRegex();
        private static readonly Regex MatchDiffLine = GenerateDiffLineRegex();
        private static readonly Regex MatchRenameLine = GenerateRenameLineRegex();
        private static readonly Regex MatchDeleteLine = GenerateDeleteLineRegex();
        private static readonly Regex MatchNewLine = GenerateNewLineRegex();


        public GitFileDiff Parse(string body)
        {
            var lines = body.Split("\n");
            return new GitFileDiff("", "", "", "", DetermineType(body));
        }

        public GitDiffType DetermineType(string body)
        {
            var lines = body.Split("\n");

            var diff = MatchDiffLine.IsMatch(lines[1]);
            var del = MatchDeleteLine.IsMatch(lines[1]);
            var rename = MatchRenameLine.IsMatch(lines[1]);
            var added = MatchNewLine.IsMatch(lines[1]);
            
            Console.WriteLine($"Del {del}; Diff {diff}; Rename: {rename}");

            return del switch
            {
                true when !rename && !diff && !added => GitDiffType.Deleted,
                false when rename && !diff && !added => GitDiffType.Renamed,
                false when !rename && !diff && added => GitDiffType.Added,
                _ => GitDiffType.Modified
            };
        }

        [GeneratedRegex(@"^\@\@ (-?[0-9]\d*),(-?[0-9]\d*) (-?[0-9]\d*)(-?[0-9]\d*) \@\@")]
        private static partial Regex GenerateDiffRunLengthRegex();

        [GeneratedRegex(@"^diff --git (\w)(.*)\s(\w)().*$")]
        private static partial Regex GenerateDiffLineRegex();

        [GeneratedRegex(@"^similarity")]
        private static partial Regex GenerateRenameLineRegex();

        [GeneratedRegex(@"^delete")]
        private static partial Regex GenerateDeleteLineRegex();

        [GeneratedRegex(@"^new")]
        private static partial Regex GenerateNewLineRegex();
    }
}