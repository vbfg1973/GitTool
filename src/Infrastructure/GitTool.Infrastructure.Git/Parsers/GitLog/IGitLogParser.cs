using GitTool.Infrastructure.Git.Models;

namespace GitTool.Infrastructure.Git.Parsers.GitLog
{
    public interface IGitLogParser
    {
        IEnumerable<GitCommitDetails> GitCommitDetailsParser(IEnumerable<string> lines);
    }
}