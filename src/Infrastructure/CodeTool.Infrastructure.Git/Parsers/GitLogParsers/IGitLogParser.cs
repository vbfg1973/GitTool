using CodeTool.Infrastructure.Git.Models;
using CodeTool.Infrastructure.Git.Parsers.Abstract;

namespace CodeTool.Infrastructure.Git.Parsers.GitLogParsers
{
    public interface IGitLogParser : IParser<GitLog>
    {
        new IEnumerable<GitLog> Parse(string body);
    }
}