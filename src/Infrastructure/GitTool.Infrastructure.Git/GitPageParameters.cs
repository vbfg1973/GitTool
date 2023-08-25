namespace GitTool.Infrastructure.Git
{
    public class GitPageParameters
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 1000;

        public int Take => PageSize;
        public int Skip => Page * PageSize - PageSize;
    }
}