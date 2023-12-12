namespace CodeTool.Infrastructure.Git.Models
{
    public record GitCommitLineage(string Sha, CommitParents Parents)
    {
        public int ParentCount => Parents.Count;
    }
}