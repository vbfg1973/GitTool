namespace CodeTool.Infrastructure.Git.ProcessRunner
{
    public record ProcessCommandRunnerResult(string StandardOut, string StandardErr, bool IsSuccessful);
}