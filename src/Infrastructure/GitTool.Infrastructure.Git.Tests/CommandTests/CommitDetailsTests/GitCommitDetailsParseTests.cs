﻿using System.Globalization;
using FluentAssertions;
using GitTool.Infrastructure.Git.Commands;
using GitTool.Infrastructure.Git.Commands.CommitDetails;
using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.Tests.CommandTests.Abstract;
using GitTool.Infrastructure.Git.Tests.Helpers;
using SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Authors;
using SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Dates;
using SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.FileCount;
using SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitDetailsTests.Data.Messages;

namespace GitTool.Infrastructure.Git.Tests.CommandTests.CommitDetailsParseTests
{
    public class GitCommitDetailsParseTests : BaseCommandTests
    {
        public GitCommitDetailsParseTests()
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
        public void Given_GitLog_Number_Of_Commits_In_Log_Is_Correct(string fileName, int expectedCommitCount)
        {
            var pathToLog = GetPathToTestResourceFile(fileName);
            IProcessCommandRunner processCommandRunner = new FileReaderProcessRunner(pathToLog);
            var commandRunner = new GitCommitDetailsCommandRunner(processCommandRunner);

            commandRunner
                .Run()
                .Count()
                .Should()
                .Be(expectedCommitCount);
        }


        [Theory]
        [ClassData(typeof(DotnetSdkGitCommitAuthors))]
        [ClassData(typeof(LinuxGitCommitAuthors))]
        [ClassData(typeof(NopCommerceGitCommitAuthors))]
        [ClassData(typeof(RoslynAnalysersGitCommitAuthors))]
        public void Given_GitLog_Identified_By_ShaId_Author_Details_Are_Correct(string fileName, string shaId,
            string authorName,
            string authorEmail)
        {
            var gitCommitDetails = FindGitCommitDetailsByShaId(fileName, shaId);

            var author = gitCommitDetails.Headers["Author"];
            
            author.Should().Contain(authorName);
            author.Should().Contain(authorEmail);
            
            gitCommitDetails.Author.Name.Should().Be(authorName);
            gitCommitDetails.Author.Email.Should().Be(authorEmail);
        }

        [Theory]
        [ClassData(typeof(DotnetSdkGitCommitDates))]
        [ClassData(typeof(LinuxGitCommitDates))]
        [ClassData(typeof(NopCommerceGitCommitDates))]
        [ClassData(typeof(RoslynAnalysersGitCommitDates))]
        public void Given_GitLog_Identified_By_ShaId_Date_Is_Correct(string fileName, string shaId, string dateString)
        {
            var DateFormatStrings = new[]
            {
                "ddd MMM d HH:mm:ss yyyy K"
            };

            var gitCommitDetails = FindGitCommitDetailsByShaId(fileName, shaId);

            var commitDate = gitCommitDetails.Headers["Date"];
            var parsedDate = DateTimeOffset.ParseExact(dateString, DateFormatStrings, CultureInfo.InvariantCulture);
            
            commitDate
                .Should()
                .BeEquivalentTo(dateString);

            gitCommitDetails.Date.Should().Be(parsedDate);
        }

        [Theory]
        [ClassData(typeof(DotnetSdkGitCommitFileCount))]
        [ClassData(typeof(LinuxGitCommitFileCount))]
        [ClassData(typeof(NopCommerceGitCommitFileCount))]
        [ClassData(typeof(RoslynAnalysersGitCommitFileCount))]
        public void Given_GitLog_Identified_By_ShaId_FileCount_Is_Correct(string fileName, string shaId, int fileCount)
        {
            var gitCommitDetails = FindGitCommitDetailsByShaId(fileName, shaId);

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
        public void Given_GitLog_Identified_By_ShaId_Message_Body_Size_Is_Correct(string fileName, string shaId,
            int messageBodySize)
        {
            var gitCommitDetails = FindGitCommitDetailsByShaId(fileName, shaId);

            gitCommitDetails
                .Message
                .Length
                .Should()
                .Be(messageBodySize);
        }

        private GitCommitDetails FindGitCommitDetailsByShaId(string fileName, string shaId)
        {
            var pathToLog = GetPathToTestResourceFile(fileName);
            IProcessCommandRunner processCommandRunner = new FileReaderProcessRunner(pathToLog);
            var commandRunner = new GitCommitDetailsCommandRunner(processCommandRunner);

            return commandRunner.Run().First(x => string.Equals(x.Sha, shaId));
        }
    }
}