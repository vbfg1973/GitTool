namespace GitTool.Infrastructure.Git
{
    public class GitPageParameters
    {
        public int Page { get; init; }
        public int PageSize { get; init; }

        public int Take => PageSize;
        public int Skip => Page * PageSize - PageSize;
    }
}