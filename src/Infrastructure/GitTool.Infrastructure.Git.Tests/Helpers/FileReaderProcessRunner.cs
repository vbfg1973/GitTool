using GitTool.Infrastructure.Git.ProcessRunner;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Abstract;

namespace GitTool.Infrastructure.Git.Tests.Helpers
{
    /// <summary>
    ///     A fake "process runner" which mimics the running of processes by reading their output from a file
    /// </summary>
    public class FileReaderProcessRunner : IProcessCommandRunner
    {
        private readonly string _path;

        public FileReaderProcessRunner(string path)
        {
            _path = path;
        }

        public async Task<ProcessCommandRunnerResult> RunAsync(GitProcessCommandLineArguments commandLineArguments,
            CancellationToken ctx)
        {
            var fileText = await File.ReadAllTextAsync(_path, ctx);
            return new ProcessCommandRunnerResult(NormaliseLineEndings(fileText), string.Empty, true);
        }

        private static string NormaliseLineEndings(string str)
        {
            return str.ReplaceLineEndings("\n");
        }
    }
}