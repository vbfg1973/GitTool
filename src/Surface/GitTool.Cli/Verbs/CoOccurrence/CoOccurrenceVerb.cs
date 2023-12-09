using GitTool.Domain.Helpers;
using GitTool.Infrastructure.Git;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Cli.Verbs.CoOccurrence
{
    public class CoOccurrenceVerb
    {
        private readonly IGitService _gitService;

        public CoOccurrenceVerb(IGitService gitService)
        {
            _gitService = gitService;
        }

        public async Task Run(CoOccurrenceOptions options, CancellationToken cancellationToken)
        {
            var token = new CancellationToken();

            var repositoryDetails = new RepositoryDetails(options.RepositoryPath);

            var commitCount = await _gitService.CountCommits(repositoryDetails, token);
            var commits = _gitService.GetLogsWithFiles(repositoryDetails,
                new GitPageParameters() { Page = 1, PageSize = commitCount }, token);

            var statistics = new CoOccurrenceStatisticsGraph();
            var tasks = commits.Select(statistics.AddGitLog).ToEnumerable();

            await Task.WhenAll(tasks);

            // Console.WriteLine(statistics.ByFile.Keys.Count);
            //
            var res = statistics
                .CoOccurrences()
                .OrderByDescending(x => x.Count);

            await CsvHelpers.WriteCsvAsync(res, options.CsvFile);
        }
    }
}