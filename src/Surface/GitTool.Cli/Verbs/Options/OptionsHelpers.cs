using GitTool.Cli.Verbs.Options.Abstract;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Cli.Verbs.Options
{
    public static class OptionsHelpers
    {
        public static RepositoryDetails GetRepositoryDetails(this IRepositoryOptions options)
        {
            return new RepositoryDetails(options.RepositoryPath);
        }
    }
}