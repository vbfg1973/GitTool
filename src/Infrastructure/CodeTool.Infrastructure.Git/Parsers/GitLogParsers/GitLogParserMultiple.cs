using CodeTool.Infrastructure.Git.Models;
using CodeTool.Infrastructure.Git.Parsers.GitLogParsers.Helpers;

namespace CodeTool.Infrastructure.Git.Parsers.GitLogParsers
{
    public class GitLogParserMultiple : IGitLogParserMultiple
    {
        private const string CommitHeader = "commit";

        private readonly GitLogFileStatusParser _gitLogFileStatusParser;
        private readonly GitLogHeaderParser _gitLogHeaderParser;
        private readonly GitLogMessageParser _gitLogMessageParser;

        public GitLogParserMultiple()
        {
            _gitLogHeaderParser = new GitLogHeaderParser();
            _gitLogMessageParser = new GitLogMessageParser();
            _gitLogFileStatusParser = new GitLogFileStatusParser();
        }

        public IEnumerable<GitLog> Parse(string body)
        {
            GitLog? commit = null;
            foreach (var line in body.Split("\n"))
            {
                // Commit header - must go first for object initialisation
                if (TryParseCommitHeader(line, out var sha))
                {
                    if (commit != null)
                        yield return commit;

                    commit = new GitLog
                    {
                        Sha = sha
                    };
                }

                _gitLogHeaderParser.Parse(line, commit);

                _gitLogMessageParser.Parse(line, commit);

                _gitLogFileStatusParser.Parse(line, commit);
            }

            if (commit != null)
                yield return commit;
        }


        /// <summary>
        ///     Tests if the current line is shaId commit header
        /// </summary>
        /// <param name="line"></param>
        /// <param name="sha"></param>
        /// <returns></returns>
        private static bool TryParseCommitHeader(string line, out string sha)
        {
            sha = string.Empty;
            if (!line.StartsWith(CommitHeader)) return false;

            sha = line.Split(' ')[1];
            return true;
        }
    }
}