using GitTool.Infrastructure.Git.Commands;
using GitTool.Infrastructure.Git.Parsers.GitLog;
using Microsoft.Extensions.DependencyInjection;

namespace GitTool.Infrastructure.Git
{
    public static class ConfigureModule
    {
        public static IServiceCollection AddGitServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IProcessCommandRunner, ProcessCommandRunner>();
            serviceCollection.AddTransient<IGitService, GitService>();
            serviceCollection.AddTransient<IGitLogParser, GitLogParser>();

            return serviceCollection;
        }
    }
}