namespace CodeTool.Infrastructure.Git.Models
{
    public class GitLog
    {
        public string Merge { get; set; } = null!;
        public DateTimeOffset Date { get; set; }
        public GitAuthor Author { get; set; } = null!;
        public string Sha { get; set; } = null!;
        public string Message { get; set; } = null!;
        public List<GitFileStatus> Files { get; set; } = new();
    }
}