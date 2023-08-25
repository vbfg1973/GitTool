using System.Collections;

namespace GitTool.Infrastructure.Git.Tests.CommandTests.GitServiceTests.Data.Messages
{
    public class RoslynAnalysersGitCommitMessages : IEnumerable<object[]>
    {
        private const string FileName = "roslyn-analyzers.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "178ece37489aa4ab4426000aba6c62ad0824e2c0", 150 };
            yield return new object[] { FileName, "38fa7f1501568eff9c5e52d9b572ca5d7666cd65", 29 };
            yield return new object[] { FileName, "87457b28b9dfce9deb1d804b5bf831d638ad9d3f", 49 };
            yield return new object[] { FileName, "991cee4ba6efe0380543e00d1bb8151903c98b29", 53 };
            yield return new object[] { FileName, "bac0aeef523770c8ca917c8b12cb7937f9f871c4", 14 };
            yield return new object[] { FileName, "ce66272b7ad48534441fe70c7fc0c710ba8a9927", 105 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}