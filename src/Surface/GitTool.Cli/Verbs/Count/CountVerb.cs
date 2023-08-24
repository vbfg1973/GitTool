using GitTool.Infrastructure.Git;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Cli.Verbs.Count
{
    public class CountVerb
    {
        private readonly IGitService _gitService;

        public CountVerb(IGitService gitService)
        {
            _gitService = gitService;
        }

        public async Task Run(CountOptions options, CancellationToken ctx)
        {
            var repoDetails = new RepositoryDetails(options.RepositoryPath);
            Console.WriteLine(await _gitService.CountCommits(repoDetails, ctx));
        }
    }
}