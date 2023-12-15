using CodeTool.Infrastructure.Git.Parsers.GitDiff;
using FluentAssertions;

namespace CodeTool.Infrastructure.Git.Tests.ParsingTests.Diff
{
    public class GitDiffParserTests
    {
        private readonly string[] _z = new[] { "Resources", "DiffOutput" };

        [Theory]
        [InlineData("gid.diff.file.modify.txt", GitDiffType.Modified)]
        public void Given_File_GitDiffType_Is_Correct(string fileName, GitDiffType expected)
        {
            var body = LoadFile(fileName);
            var parser = new GitDiffParser();

            var t = parser.DetermineType(body);

            t.Should().Be(GitDiffType.Modified);
        }

        private string LoadFile(string fileName)
        {
            var elements = new List<string>();
            elements.AddRange(_z);
            elements.Add(fileName);

            return File.ReadAllText(Path.Combine(elements.ToArray()));
        }
    }
}