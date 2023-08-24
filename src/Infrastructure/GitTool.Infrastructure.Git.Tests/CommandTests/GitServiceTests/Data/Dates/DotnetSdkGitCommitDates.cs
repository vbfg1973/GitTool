using System.Collections;

namespace GitTool.Infrastructure.Git.Tests.CommandTests.CommitDetailsTests.Data.Dates
{
    public class DotnetSdkGitCommitDates : IEnumerable<object[]>
    {
        private const string FileName = "dotnet-sdk.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
                { FileName, "ff634058dca93dd80a0b23f06fc3c64d419e16e8", "Tue Apr 11 16:33:21 2023 +0000" };
            yield return new object[]
                { FileName, "19abc283c3e13dabaf94407e986c1d513d957c8d", "Tue Apr 4 14:34:45 2023 -0700" };
            yield return new object[]
                { FileName, "e006ecf6a4171974ae31bd91ebedcd27c5ca0864", "Wed Apr 26 15:05:06 2023 -0700" };
            yield return new object[]
                { FileName, "f2f4a3ecf1cec94cb4c32c16e1c85d25c66646a2", "Thu Apr 27 22:33:58 2023 -0700" };
            yield return new object[]
                { FileName, "cf8c22ef8be20451ea0e2c8ef5b0f94b10eea2db", "Fri Apr 21 09:57:43 2023 -0700" };
            yield return new object[]
                { FileName, "0091fb4deb0134b885c67a93f4ca3ceaaecf4d21", "Tue Apr 25 17:29:14 2023 -0400" };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}