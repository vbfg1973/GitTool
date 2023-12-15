namespace CodeTool.Infrastructure.Git.Parsers.Abstract
{
    public interface IParserMultiple<T>
    {
        IEnumerable<T> Parse(string body);
    }
}