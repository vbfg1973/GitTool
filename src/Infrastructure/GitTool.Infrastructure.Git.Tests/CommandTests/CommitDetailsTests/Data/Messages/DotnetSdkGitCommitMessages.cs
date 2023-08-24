using System.Collections;

namespace GitTool.Infrastructure.Git.Tests.CommandTests.CommitDetailsTests.Data.Messages
{
    public class DotnetSdkGitCommitMessages : IEnumerable<object[]>
    {
        private const string FileName = "dotnet-sdk.txt";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FileName, "0091fb4deb0134b885c67a93f4ca3ceaaecf4d21", 81 };
            yield return new object[] { FileName, "19abc283c3e13dabaf94407e986c1d513d957c8d", 31 };
            yield return new object[] { FileName, "cf8c22ef8be20451ea0e2c8ef5b0f94b10eea2db", 71 };
            yield return new object[] { FileName, "e006ecf6a4171974ae31bd91ebedcd27c5ca0864", 25 };
            yield return new object[] { FileName, "f2f4a3ecf1cec94cb4c32c16e1c85d25c66646a2", 110 };
            yield return new object[] { FileName, "ff634058dca93dd80a0b23f06fc3c64d419e16e8", 415 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}