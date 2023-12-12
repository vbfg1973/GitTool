using CodeTool.Infrastructure.Git.Parsers;
using CodeTool.Infrastructure.Git.Parsers.GitLineageParsers;
using CodeTool.Infrastructure.Git.Parsers.GitLogParsers;
using CodeTool.Infrastructure.Git.ProcessRunner;
using Microsoft.Extensions.DependencyInjection;

namespace CodeTool.Infrastructure.Git
{
    public static class ConfigureModule
    {
        public static IServiceCollection AddGitServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IProcessCommandRunner, ProcessCommandRunner>();
            serviceCollection.AddTransient<IGitService, GitService>();
            serviceCollection.AddTransient<IGitLogParser, GitLogParser>();
            serviceCollection.AddTransient<IGitLineageParser, GitLineageParser>();

            return serviceCollection;
        }
    }
}