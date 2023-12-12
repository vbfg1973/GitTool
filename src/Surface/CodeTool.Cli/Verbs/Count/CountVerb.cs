using CodeTool.Cli.Verbs.Options;
using CodeTool.Infrastructure.Git;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Cli.Verbs.Count
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
            var repositoryDetails = options.GetRepositoryDetails();
            Console.WriteLine(await _gitService.CountCommits(repositoryDetails, ctx));
        }
    }
}