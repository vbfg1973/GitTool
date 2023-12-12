namespace CodeTool.Domain.Features.Repositories.GitLogs.Csv
{
    public class GitLogDto 
    {
        public string Sha { get; init; }
        public string Date { get; init; }
        public string Merge { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Path { get; set; }
        public string OldPath { get; set; }
        public string ChangeKind { get; set; }
    }
}