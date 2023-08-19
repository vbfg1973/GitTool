namespace GitTool.Domain.Features.Repositories.Commands.CommitCsv
{
    public class FileCommitDetail
    {
        public string Sha { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string AuthorEmail { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string CurrentPath { get; set; } = null!;
        public string OldPath { get; set; } = null!;
        public string ChangeKind { get; set; } = null!;
    }
}