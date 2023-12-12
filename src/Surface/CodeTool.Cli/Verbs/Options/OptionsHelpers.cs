using CodeTool.Cli.Verbs.Options.Abstract;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Cli.Verbs.Options
{
    public static class OptionsHelpers
    {
        public static RepositoryDetails GetRepositoryDetails(this IRepositoryOptions options)
        {
            return new RepositoryDetails(options.RepositoryPath);
        }
    }
}