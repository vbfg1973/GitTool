using CodeTool.Domain.Complexity.Abstract;

namespace CodeTool.Domain.Complexity
{
    /// <summary>
    ///     Indentation based cognitive complexity analysis
    /// </summary>
    public class IndentationComplexityAnalyzer : IComplexityAnalyzer
    {
        public IndentationComplexityAnalyzer(IEnumerable<string> lines)
        {
            var linesArray = CleanLinesArray(lines.ToArray());
            ComplexityScore = linesArray.Sum(LeadingWhitespaceCount);
        }

        public IndentationComplexityAnalyzer(string text)
        {
            var linesArray = CleanLinesArray(text.Split("\n").ToArray());
            ComplexityScore = linesArray.Sum(LeadingWhitespaceCount);
        }

        public int ComplexityScore { get; }

        /// <summary>
        ///     Counts the leading whitespace of a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns>
        ///     An integer representing the count of the leading whitespace. The type of whitespace is not taken into
        ///     account so tabs, spaces, etc are considered equivalent.
        /// </returns>
        /// <remarks>
        ///     Counts the leading whitespace of a string, favouring for loops over TakeWhile Linq methods to
        ///     reduce allocations and optimise for speed. Don't change this to LINQ for the sake of readability!
        /// </remarks>
        private static int LeadingWhitespaceCount(string str)
        {
            var count = 0;
            for (var i = 0; i <= str.Length; i++)
            {
                if (!char.IsWhiteSpace(str[i])) break;

                count++;
            }

            return count;
        }

        /// <summary>
        ///     Convert to array of only populated lines
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private static string[] CleanLinesArray(string[] lines)
        {
            return lines
                .Where(str => !string.IsNullOrEmpty(str))
                .Where(str => !string.IsNullOrWhiteSpace(str))
                .ToArray();
        }
    }
}