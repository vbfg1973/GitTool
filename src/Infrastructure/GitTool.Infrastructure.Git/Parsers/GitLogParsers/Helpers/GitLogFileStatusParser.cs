using System.Text.RegularExpressions;
using GitTool.Infrastructure.Git.Models;

namespace GitTool.Infrastructure.Git.Parsers.GitLogParsers.Helpers
{
    internal partial class GitLogFileStatusParser
    {
        private static readonly Regex RegexIsFileStatus = GenerateFileStatusMatchLineRegex();

        public void Parse(string line, GitLog? commit)
        {
            // File and changeKind
            if (TryParseFileStatusLine(line, out var changeKind, out var currentPath, out var oldPath))
                commit?.Files.Add(new GitFileStatus(changeKind, currentPath, oldPath));
        }

        /// <summary>
        ///     Tests if the current line is a commit file status line
        /// </summary>
        /// <param name="line"></param>
        /// <param name="changeKind"></param>
        /// <param name="currentPath"></param>
        /// <param name="oldPath"></param>
        /// <returns></returns>
        private static bool TryParseFileStatusLine(string line, out ChangeKind changeKind, out string currentPath,
            out string oldPath)
        {
            changeKind = ChangeKind.Unmodified;
            currentPath = string.Empty;
            oldPath = string.Empty;

            if (!RegexIsFileStatus.IsMatch(line)) return false;

            changeKind = MapStatusToChangeKind(line);

            // The last two (of a possible three) tab seperated elements are the paths.
            // If there's only one then 
            var pathElements = line.Split('\t').Skip(1).ToArray();

            switch (pathElements.Length)
            {
                case 1:
                    currentPath = pathElements[0];
                    oldPath = pathElements[0];
                    break;
                case 2:
                    oldPath = pathElements[0];
                    currentPath = pathElements[1];
                    break;
                default:
                    throw new Exception();
            }

            return true;
        }

        /// <summary>
        ///     Returns a ChangeKind enum based on the character parsed from a fileStatus line in git log output
        /// </summary>
        /// <param name="fileStatusLine"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static ChangeKind MapStatusToChangeKind(string fileStatusLine)
        {
            var c = fileStatusLine[0];
            return c switch
            {
                'A' => ChangeKind.Added,
                'D' => ChangeKind.Deleted,
                'M' => ChangeKind.Modified,
                'R' => ChangeKind.Renamed,
                'I' => ChangeKind.Ignored,
                'T' => ChangeKind.TypeChanged,
                _ => throw new ArgumentOutOfRangeException(nameof(fileStatusLine),
                    $"Unknown fileStatus: {fileStatusLine}")
            };
        }

        /// <summary>
        ///     Generate a regex to match fileStatus lines in git log output
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex("^[A-Z]\\w*\\s")]
        private static partial Regex GenerateFileStatusMatchLineRegex();
    }
}