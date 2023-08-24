using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.Parsers.Abstract;

namespace GitTool.Infrastructure.Git.Parsers.GitLogParsers
{
    public interface IGitLogParser : IParser<GitLog>
    {
        new IEnumerable<GitLog> Parse(string body);
    }
}