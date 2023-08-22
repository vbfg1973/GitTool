namespace GitTool.Infrastructure.Git.Models
{
    public class GitCommitParents
    {
        public GitCommitParents()
        {
            Parents = new List<string>();
        }

        public string Sha { get; init; } = null!;
        public List<string> Parents { get; init; }
    }
}