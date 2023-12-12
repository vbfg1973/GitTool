using System.Text;
using System.Text.RegularExpressions;
using CodeTool.Infrastructure.Git.Models;

namespace CodeTool.Infrastructure.Git.Parsers.GitLogParsers.Helpers
{
    internal partial class GitLogMessageParser
    {
        private static readonly Regex RegexIsMessageLine = GenerateMessageMatchLineRegex();
        private bool _currentlyProcessingMessageBody;
        private StringBuilder _messageBuilder = new();

        /// <summary>
        ///     If message line extract and add to gitLog object
        /// </summary>
        /// <param name="line"></param>
        /// <param name="gitLog"></param>
        public void Parse(string line, GitLog? gitLog)
        {
            // Commit messages
            if (IsMessageLine(line))
                AddMessageLine(line);

            else if (!IsMessageLine(line)) BuildGitCommitMessageIfFinishedProcessingMessageBody(gitLog);
        }

        /// <summary>
        ///     Tests if the current line is part of a commit message body
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static bool IsMessageLine(string line)
        {
            return RegexIsMessageLine.IsMatch(line);
        }

        private void ResetMessageBodyProcessing()
        {
            _messageBuilder = new StringBuilder();
            _currentlyProcessingMessageBody = false;
        }

        private void AddMessageLine(string messageLine)
        {
            _currentlyProcessingMessageBody = true;
            _messageBuilder.AppendLine(messageLine);
        }

        private void BuildGitCommitMessageIfFinishedProcessingMessageBody(GitLog? gitLog)
        {
            // If not currently processing then no body to collect
            if (!_currentlyProcessingMessageBody) return;

            // If null then we haven't really started
            if (gitLog == null) return;

            gitLog.Message = _messageBuilder.ToString();
            ResetMessageBodyProcessing();
        }

        [GeneratedRegex(@"^(?:\t|\s{4})")] // Leads with a tab or four spaces
        private static partial Regex GenerateMessageMatchLineRegex();
    }
}