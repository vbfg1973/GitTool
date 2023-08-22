using System.Globalization;
using System.Text;
using GitTool.Infrastructure.Git.Commands;
using GitTool.Infrastructure.Git.Models;

namespace GitTool.Infrastructure.Git.Parsers
{
    public class GitLogParser
    {
        private bool _currentlyProcessingMessageBody;
        private StringBuilder _messageBuilder = new();

        private readonly string[] DateFormatStrings =
        {
            "ddd MMM d HH:mm:ss yyyy K"
        };


        public GitLogParser()
        {
            ResetMessageBodyProcessing();
        }

        public IEnumerable<GitCommitDetails> GitCommitDetailsParser(IEnumerable<string> lines)
        {
            GitCommitDetails? commit = null;
            foreach (var line in lines)
            {
                // Commit header - must go first for object initialisation
                if (line.TryParseCommitHeader(out var sha))
                {
                    if (commit != null)
                        yield return commit;

                    commit = new GitCommitDetails
                    {
                        Sha = sha
                    };
                }

                ParseHeaders(line, commit);

                ParseMessages(line, commit);

                ParseFileStatuses(line, commit);
            }

            if (commit != null)
                yield return commit;
        }

        private static void ParseFileStatuses(string line, GitCommitDetails? commit)
        {
            // File and changeKind
            if (line.TryParseFileStatusLine(out var changeKind, out var currentPath, out var oldPath))
                commit?.Files.Add(new GitFileStatus(changeKind, currentPath, oldPath));
        }
        
        #region Headers

        /// <summary>
        ///     Examine git log headers and process accordingly
        /// </summary>
        /// <param name="line"></param>
        /// <param name="commit"></param>
        private void ParseHeaders(string line, GitCommitDetails? commit)
        {
            // Header lines - author, date, merge, etc
            if (!line.TryParseHeader(out var headerName, out var headerValue)) return;

            switch (headerName)
            {
                case "Date":
                    commit!.Date = ParseDateTimeOffset(headerValue);
                    break;
                case "Author":
                    commit!.Author = ParseAuthorDetails(headerValue);
                    break;
                case "Merge":
                    commit!.Merge = headerValue;
                    break;
            }
        }

        /// <summary>
        ///     Parse the git date format
        /// </summary>
        /// <param name="dateTimeString"></param>
        /// <returns></returns>
        public DateTimeOffset ParseDateTimeOffset(string dateTimeString)
        {
            return DateTimeOffset.ParseExact(dateTimeString, DateFormatStrings, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Parse git author details
        /// </summary>
        /// <param name="authorHeader"></param>
        /// <returns></returns>
        private static GitAuthor ParseAuthorDetails(string authorHeader)
        {
            // Console.WriteLine(authorHeader);

            var firstIndex = authorHeader.IndexOf('<');

            return new GitAuthor(
                authorHeader.Substring(0, firstIndex > 0 ? firstIndex - 1 : firstIndex),
                authorHeader.Substring(
                    firstIndex + 1,
                    authorHeader.IndexOf('>') - (firstIndex + 1)));
        }

        #endregion

        #region Message Lines

        private void ParseMessages(string line, GitCommitDetails? commit)
        {
            // Commit messages
            if (line.IsMessageLine())
                AddMessageLine(line);

            else if (!line.IsMessageLine()) BuildGitCommitMessageIfFinishedProcessingMessageBody(commit);
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

        private void BuildGitCommitMessageIfFinishedProcessingMessageBody(GitCommitDetails? commit)
        {
            // If not currently processing then no body to collect
            if (!_currentlyProcessingMessageBody) return;

            // If commit is null then we haven't really started
            if (commit == null) return;

            commit.Message = _messageBuilder.ToString().NormaliseLineEndings();
            ResetMessageBodyProcessing();
        }

        #endregion
    }
}