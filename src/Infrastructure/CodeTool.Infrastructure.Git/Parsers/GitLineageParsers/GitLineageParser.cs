using CodeTool.Infrastructure.Git.Models;

namespace CodeTool.Infrastructure.Git.Parsers.GitLineageParsers
{
    public class GitLineageParser : IGitLineageParser
    {
        public IEnumerable<GitCommitLineage> Parse(string body)
        {
            var lines = body.Split("\n",
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            foreach (var l in lines)
            {
                var shaIds = l.Split(' ',
                        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var sha = shaIds[0];

                yield return new GitCommitLineage(sha, GetCommits(shaIds));
            }
        }

        private static CommitParents GetCommits(IReadOnlyCollection<string> shaIdsList)
        {
            if (shaIdsList.Count <= 1) return new CommitParents();

            var c = new CommitParents();
            c.AddRange(shaIdsList.Skip(1));

            return c;
        }
    }
}