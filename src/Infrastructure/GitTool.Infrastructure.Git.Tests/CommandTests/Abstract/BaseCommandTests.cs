namespace GitTool.Infrastructure.Git.Tests.CommandTests.Abstract
{
    public abstract class BaseCommandTests
    {
        protected List<string> DirPath;

        protected BaseCommandTests()
        {
            DirPath = new List<string>();
        }

        protected string GetPathToTestResourceFile(string fileName)
        {
            var newList = new List<string>(DirPath) { fileName };
            return Path.Combine(newList.ToArray());
        }
    }
}