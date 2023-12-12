namespace CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters
{
    public class GitPaging
    {
        public GitPaging(int take = 0, int skip = 0)
        {
            Take = take;
            Skip = skip;
        }

        public int Take { get; }
        public int Skip { get; }

        public bool UsePaging => Take > 0;
    }
}