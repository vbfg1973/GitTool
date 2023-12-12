using System.Globalization;
using System.Text.RegularExpressions;
using CodeTool.Infrastructure.Git.Models;

namespace CodeTool.Infrastructure.Git.Parsers.GitLogParsers.Helpers
{
    internal partial class GitLogHeaderParser
    {
        private static readonly Regex RegexIsHeaderLine = GenerateHeaderMatchLineRegex();

        private readonly string[] _dateFormatStrings;

        public GitLogHeaderParser()
        {
            _dateFormatStrings = new[]
            {
                "ddd MMM d HH:mm:ss yyyy K"
            };
        }

        /// <summary>
        ///     If header line parse and add to gitLog object
        /// </summary>
        /// <param name="line"></param>
        /// <param name="gitLog"></param>
        public void Parse(string line, GitLog? gitLog)
        {
            // Header lines - author, date, merge, etc
            if (!TryParseHeader(line, out var headerName, out var headerValue)) return;

            switch (headerName)
            {
                case "Date":
                    gitLog!.Date = ParseDateTimeOffset(headerValue);
                    break;
                case "Author":
                    gitLog!.Author = ParseAuthorDetails(headerValue);
                    break;
                case "Merge":
                    gitLog!.Merge = headerValue;
                    break;
            }
        }

        /// <summary>
        ///     Parse the git date format
        /// </summary>
        /// <param name="dateTimeString"></param>
        /// <returns></returns>
        private DateTimeOffset ParseDateTimeOffset(string dateTimeString)
        {
            return DateTimeOffset.ParseExact(dateTimeString, _dateFormatStrings, CultureInfo.InvariantCulture);
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

        /// <summary>
        ///     Tests if the current line is a commit header other than commit shaId. Author, date, merge, etc
        /// </summary>
        /// <param name="line"></param>
        /// <param name="headerName"></param>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        private static bool TryParseHeader(string line, out string headerName, out string headerValue)
        {
            headerName = string.Empty;
            headerValue = string.Empty;

            if (!RegexIsHeaderLine.IsMatch(line)) return false;

            var elements = line.Split(':');
            headerName = elements[0];
            headerValue = string.Join(':', elements.Skip(1)).Trim();

            return true;
        }

        [GeneratedRegex(@"^\w+\:")] // is a header line
        private static partial Regex GenerateHeaderMatchLineRegex();
    }
}