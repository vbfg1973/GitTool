namespace CodeTool.Infrastructure.Git.Parsers.Abstract
{
    public interface IParser<T>
    {
        T Parse(string body);
    }
}