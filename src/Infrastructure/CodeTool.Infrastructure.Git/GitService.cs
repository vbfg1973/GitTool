using System.Runtime.CompilerServices;
using CodeTool.Infrastructure.Git.Models;
using CodeTool.Infrastructure.Git.Parsers;
using CodeTool.Infrastructure.Git.Parsers.GitLineageParsers;
using CodeTool.Infrastructure.Git.Parsers.GitLogParsers;
using CodeTool.Infrastructure.Git.ProcessRunner;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Infrastructure.Git
{
    public class GitService : IGitService
    {
        private readonly IGitLogParserMultiple _gitLogParserMultiple;
        private readonly IGitLineageParserMultiple _gitLineageParserMultiple;
        private readonly IProcessCommandRunner _processCommandRunner;

        public GitService(IProcessCommandRunner processCommandRunner,
            IGitLogParserMultiple gitLogParserMultiple,
            IGitLineageParserMultiple gitLineageParserMultiple)
        {
            _processCommandRunner = processCommandRunner;
            _gitLogParserMultiple = gitLogParserMultiple;
            _gitLineageParserMultiple = gitLineageParserMultiple;
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

            foreach (var gitLog in _gitLogParserMultiple.Parse(processRunnerResult.StandardOut)) yield return gitLog;
        }
        
        public async IAsyncEnumerable<GitCommitLineage> GetLineage(RepositoryDetails repositoryDetails,
            [EnumeratorCancellation] CancellationToken ctx)
        {
            var command = new GitParentsCommand(repositoryDetails);
            var processRunnerResult = await _processCommandRunner.RunAsync(command, ctx);

            if (!processRunnerResult.IsSuccessful) yield break;

            foreach (var gitLog in _gitLineageParserMultiple.Parse(processRunnerResult.StandardOut)) yield return gitLog;
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

            foreach (var gitLog in _gitLogParserMultiple.Parse(processRunnerResult.StandardOut)) yield return gitLog;
        }
    }
}