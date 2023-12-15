using System.Text.RegularExpressions;
using CodeTool.Infrastructure.Git.Models.Diff;

namespace CodeTool.Infrastructure.Git.Parsers.GitDiff
{
    public class GitDiffParserMultiple : IGitDiffParserMultiple
    {
        private readonly Regex _chunkStart = new Regex("^diff --git a.+ b.+$");
        
        public IEnumerable<GitFileDiff> Parse(string body)
        {
            body = body.ReplaceLineEndings("\n");

            foreach (var chunk in Chunks(body))
            {
                // yield return new GitFileDiff()
            }

            return Enumerable.Empty<GitFileDiff>();
        }

        public IEnumerable<string> Chunks(string body)
        {
            var chunkLines = new List<string>();
            var lines = body.Split("\n");

            foreach (var line in lines)
            {
                if (_chunkStart.IsMatch(line))
                {
                    chunkLines = new List<string>();
                    yield return string.Join("\n", chunkLines);
                }
                
                chunkLines.Add(line);
            }
            
            yield return string.Join("\n", chunkLines);
        }
    }
}