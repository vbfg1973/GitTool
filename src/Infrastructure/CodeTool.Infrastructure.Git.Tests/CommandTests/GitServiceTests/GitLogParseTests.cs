﻿using System.Globalization;
using CodeTool.Infrastructure.Git.Tests.CommandTests.Abstract;
using CodeTool.Infrastructure.Git.Tests.CommandTests.GitServiceTests.Data.Authors;
using CodeTool.Infrastructure.Git.Tests.CommandTests.GitServiceTests.Data.Dates;
using CodeTool.Infrastructure.Git.Tests.CommandTests.GitServiceTests.Data.FileCount;
using CodeTool.Infrastructure.Git.Tests.CommandTests.GitServiceTests.Data.Messages;
using CodeTool.Infrastructure.Git.Tests.Helpers;
using FluentAssertions;
using CodeTool.Infrastructure.Git.Models;
using CodeTool.Infrastructure.Git.Parsers;
using CodeTool.Infrastructure.Git.Parsers.GitLineageParsers;
using CodeTool.Infrastructure.Git.Parsers.GitLogParsers;
using CodeTool.Infrastructure.Git.ProcessRunner;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Infrastructure.Git.Tests.CommandTests.GitServiceTests
{
    public class GitLogParseTests : BaseCommandTests
    {
        public GitLogParseTests()
        {
            DirPath = new List<string>
            {
                "Resources",
                "GitLogOutput"
            };
        }

        [Theory]
        [InlineData("linux.txt", 283)]
        [InlineData("nopCommerce.txt", 601)]
        [InlineData("dotnet-sdk.txt", 935)]
        [InlineData("roslyn-analyzers.txt", 789)]
        public async Task Given_GitLog_Number_Of_Commits_In_Log_Is_Correct(string fileName, int expectedCommitCount)
        {
            var commandRunner = CommandRunner(fileName);

            var list = await commandRunner
                .GetLogs(
                    new RepositoryDetails("Fake"),
                    new GitPageParameters(),
                    new CancellationToken()).ToListAsync();

            list
                .Count
                .Should()
                .Be(expectedCommitCount);
        }

        private GitService CommandRunner(string fileName)
        {
            var pathToLog = GetPathToTestResourceFile(fileName);
            IProcessCommandRunner processCommandRunner = new FileReaderProcessRunner(pathToLog);
            IGitLogParser gitLogParser = new GitLogParser();
            IGitLineageParser gitLineageParser = new GitLineageParser();
            var commandRunner = new GitService(processCommandRunner, gitLogParser, gitLineageParser);
            return commandRunner;
        }


        [Theory]
        [ClassData(typeof(DotnetSdkGitCommitAuthors))]
        [ClassData(typeof(LinuxGitCommitAuthors))]
        [ClassData(typeof(NopCommerceGitCommitAuthors))]
        [ClassData(typeof(RoslynAnalysersGitCommitAuthors))]
        public async Task Given_GitLog_Identified_By_ShaId_Author_Details_Are_Correct(string fileName, string shaId,
            string authorName,
            string authorEmail)
        {
            var gitCommitDetails = await FindGitCommitDetailsByShaId(fileName, shaId);

            gitCommitDetails.Author.Name.Should().Be(authorName);
            gitCommitDetails.Author.Email.Should().Be(authorEmail);
        }

        [Theory]
        [ClassData(typeof(DotnetSdkGitCommitDates))]
        [ClassData(typeof(LinuxGitCommitDates))]
        [ClassData(typeof(NopCommerceGitCommitDates))]
        [ClassData(typeof(RoslynAnalysersGitCommitDates))]
        public async Task Given_GitLog_Identified_By_ShaId_Date_Is_Correct(string fileName, string shaId,
            string dateString)
        {
            var DateFormatStrings = new[]
            {
                "ddd MMM d HH:mm:ss yyyy K"
            };

            var gitCommitDetails = await FindGitCommitDetailsByShaId(fileName, shaId);

            var parsedDate = DateTimeOffset.ParseExact(dateString, DateFormatStrings, CultureInfo.InvariantCulture);

            gitCommitDetails.Date.Should().Be(parsedDate);
        }

        [Theory]
        [ClassData(typeof(DotnetSdkGitCommitFileCount))]
        [ClassData(typeof(LinuxGitCommitFileCount))]
        [ClassData(typeof(NopCommerceGitCommitFileCount))]
        [ClassData(typeof(RoslynAnalysersGitCommitFileCount))]
        public async Task Given_GitLog_Identified_By_ShaId_FileCount_Is_Correct(string fileName, string shaId,
            int fileCount)
        {
            var gitCommitDetails = await FindGitCommitDetailsByShaId(fileName, shaId);

            gitCommitDetails
                .Files
                .Count
                .Should()
                .Be(fileCount);
        }

        [Theory]
        [ClassData(typeof(DotnetSdkGitCommitMessages))]
        [ClassData(typeof(LinuxGitCommitMessages))]
        [ClassData(typeof(NopCommerceGitCommitMessages))]
        [ClassData(typeof(RoslynAnalysersGitCommitMessages))]
        public async Task Given_GitLog_Identified_By_ShaId_Message_Body_Size_Is_Correct(string fileName, string shaId,
            int messageBodySize)
        {
            var gitCommitDetails = await FindGitCommitDetailsByShaId(fileName, shaId);

            string.Join("", gitCommitDetails
                    .Message
                    .Split("\n")
                    .Select(x => x.TrimEnd()))
                .Length
                .Should()
                .Be(messageBodySize);
        }

        private async Task<GitLog> FindGitCommitDetailsByShaId(string fileName, string shaId)
        {
            var commandRunner = CommandRunner(fileName);

            return await commandRunner
                .GetLogs(
                    new RepositoryDetails("Fake"),
                    new GitPageParameters(),
                    new CancellationToken())
                .FirstAsync(x => string.Equals(x.Sha, shaId));
        }
    }
}