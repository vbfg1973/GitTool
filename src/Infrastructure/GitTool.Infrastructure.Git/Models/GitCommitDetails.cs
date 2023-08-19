namespace GitTool.Infrastructure.Git.Models
{
    public class GitCommitDetails
    {
        public GitCommitDetails()
        {
            Files = new List<GitFileStatus>();
            Message = string.Empty;
        }

        public string Merge { get; set; }
        public DateTimeOffset Date { get; set; }
        public GitAuthor Author { get; set; }
        public string Sha { get; set; } = null!;
        public string Message { get; set; }
        public List<GitFileStatus> Files { get; set; }
    }

    public record GitAuthor(string Name, string Email);
}