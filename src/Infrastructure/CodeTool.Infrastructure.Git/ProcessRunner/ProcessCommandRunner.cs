﻿using System.Text.Json;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Abstract;
using static SimpleExec.Command;

namespace CodeTool.Infrastructure.Git.ProcessRunner
{
    public class ProcessCommandRunner : IProcessCommandRunner
    {
        public async Task<ProcessCommandRunnerResult> RunAsync(GitProcessCommandLineArguments commandLineArguments,
            CancellationToken ctx)
        {
            Console.WriteLine(JsonSerializer.Serialize(commandLineArguments));
            ProcessCommandRunnerResult processCommandRunnerResult = null!;
            var standardError = string.Empty;
            var standardOutput = string.Empty;
            try
            {
                var process = commandLineArguments.ProcessName;
                var args = commandLineArguments.Arguments().ToList();

                Console.WriteLine($"{process} {string.Join(" ", args)}");

                (standardOutput, standardError) =
                    await ReadAsync(process, args, cancellationToken: ctx);

                processCommandRunnerResult = new ProcessCommandRunnerResult(NormaliseLineEndings(standardOutput),
                    NormaliseLineEndings(standardError), true);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                processCommandRunnerResult = new ProcessCommandRunnerResult(string.Empty, e.Message, false);
                throw;
            }

            return processCommandRunnerResult;
        }

        /// <summary>
        ///     Force line endings to be replaces by a newline, irrespective of the original type
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string NormaliseLineEndings(string str)
        {
            return str.ReplaceLineEndings('\n'.ToString());
        }
    }
}