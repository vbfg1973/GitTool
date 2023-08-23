using GitTool.Infrastructure.Git;

namespace GitTool.Cli.Verbs.Reverse
{
    public class ReverseVerb
    {
        private readonly IGitService _gitService;

        public ReverseVerb(IGitService gitService)
        {
            _gitService = gitService;
        }

        public async Task Run(ReverseOptions options)
        {
            foreach (var id in _gitService.ReverseShaIds(options.RepositoryPath))
            {
                Console.WriteLine(id);
            }
        }
    }
}