using GitTool.Infrastructure.Git.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace GitTool.Infrastructure.Git
{
    public static class ConfigureModule
    {
        public static IServiceCollection AddGitServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IProcessCommandRunner, ProcessCommandRunner>();
            serviceCollection.AddTransient<IGitService, GitService>();

            return serviceCollection;
        }
    }
}