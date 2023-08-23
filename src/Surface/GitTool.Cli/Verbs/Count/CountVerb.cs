using GitTool.Cli.Verbs.Reverse;
using GitTool.Infrastructure.Git;

namespace GitTool.Cli.Verbs.Count
{
    public class CountVerb
    {
        private readonly IGitService _gitService;

        public CountVerb(IGitService gitService)
        {
            _gitService = gitService;
        }

        public async Task Run(CountOptions options)
        {
            Console.WriteLine(_gitService.CountCommits(options.RepositoryPath));
        }
    }
}