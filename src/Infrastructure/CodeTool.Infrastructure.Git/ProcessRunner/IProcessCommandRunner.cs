using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Abstract;

namespace CodeTool.Infrastructure.Git.ProcessRunner
{
    public interface IProcessCommandRunner
    {
        Task<ProcessCommandRunnerResult> RunAsync(GitProcessCommandLineArguments commandLineArguments, CancellationToken ctx);
    }
}