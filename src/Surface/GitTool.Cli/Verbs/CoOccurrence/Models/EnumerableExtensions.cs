namespace GitTool.Cli.Verbs.CoOccurrence
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0
                ? new[] { new T[0] }
                : elements.SelectMany((e, i) =>
                    elements
                        .Skip(i + 1)
                        .DifferentCombinations(k - 1)
                        .Select(c => (new[] { e })
                            .Concat<T>(c)));
        }
    }
}