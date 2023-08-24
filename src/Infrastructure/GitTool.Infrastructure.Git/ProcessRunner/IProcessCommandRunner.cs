using GitTool.Infrastructure.Git.ProcessRunner.Commands.Abstract;

namespace GitTool.Infrastructure.Git.ProcessRunner
{
    public interface IProcessCommandRunner
    {
        Task<ProcessCommandRunnerResult> RunAsync(GitProcessCommandLineArguments commandLineArguments, CancellationToken ctx);
    }
}