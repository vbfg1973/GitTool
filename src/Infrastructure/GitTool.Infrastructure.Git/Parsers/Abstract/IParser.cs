namespace GitTool.Infrastructure.Git.Parsers.Abstract
{
    public interface IParser<T>
    {
        IEnumerable<T> Parse(string body);
    }
}