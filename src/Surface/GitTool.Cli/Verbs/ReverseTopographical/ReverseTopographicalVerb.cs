using GitTool.Infrastructure.Git;

namespace GitTool.Cli.Verbs.ReverseTopographical
{
    public class ReverseTopographicalVerb
    {
        private readonly IGitService _gitService;

        public ReverseTopographicalVerb(IGitService gitService)
        {
            _gitService = gitService;
        }

        public async Task Run(ReverseTopographicalOptions options)
        {
            foreach (var id in _gitService.ReverseTopographicalShaIds(options.RepositoryPath))
            {
                Console.WriteLine(id);
            }
        }
    }
}