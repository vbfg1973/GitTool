namespace GitTool.Domain.Features.Repositories.Commands.CommitFileCorrelation
{
    public class CorrelationData
    {
        public string File { get; set; }
        public int Commits { get; set; }
        public string CoChangingFile { get; set; }
        public int CoChangesCount { get; set; }
    }
}