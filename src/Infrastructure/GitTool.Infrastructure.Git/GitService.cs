﻿using System.Runtime.CompilerServices;
using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.Parsers.GitLogParsers;
using GitTool.Infrastructure.Git.ProcessRunner;
using GitTool.Infrastructure.Git.ProcessRunner.Commands;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Infrastructure.Git
{
    public class GitService : IGitService
    {
        private readonly IGitLogParser _gitLogParser;
        private readonly IProcessCommandRunner _processCommandRunner;

        public GitService(IProcessCommandRunner processCommandRunner,
            IGitLogParser gitLogParser)
        {
            _processCommandRunner = processCommandRunner;
            _gitLogParser = gitLogParser;
        }

        public async Task<int> CountCommits(RepositoryDetails repositoryDetails, CancellationToken ctx)
        {
            var processRunnerResult =
                await _processCommandRunner.RunAsync(new GitCommitCount(repositoryDetails),
                    ctx);

            return processRunnerResult.IsSuccessful ? Convert.ToInt32(processRunnerResult.StandardOut) : 0;
        }

        public async IAsyncEnumerable<GitLog> GetLogs(RepositoryDetails repositoryDetails,
            GitPageParameters pageParameters,
            [EnumeratorCancellation] CancellationToken ctx)
        {
            var command = new GitLogBasicPaging(repositoryDetails, pageParameters, includeFiles: false);
            var processRunnerResult = await _processCommandRunner.RunAsync(command, ctx);

            if (!processRunnerResult.IsSuccessful) yield break;

            foreach (var gitLog in _gitLogParser.Parse(processRunnerResult.StandardOut)) yield return gitLog;
        }

        public async IAsyncEnumerable<GitLog> GetLogsWithFiles(RepositoryDetails repositoryDetails,
            GitPageParameters pageParameters, [EnumeratorCancellation] CancellationToken ctx)
        {
            Console.WriteLine("Calling");
            var command = new GitLogBasicPaging(repositoryDetails, pageParameters, includeFiles: true);
            var processRunnerResult = await _processCommandRunner.RunAsync(command, ctx);

            if (!processRunnerResult.IsSuccessful)
            {
                Console.WriteLine("Failed");
                yield break;
            }

            foreach (var gitLog in _gitLogParser.Parse(processRunnerResult.StandardOut)) yield return gitLog;
        }
    }
}