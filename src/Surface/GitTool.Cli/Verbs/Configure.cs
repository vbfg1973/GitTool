using GitTool.Cli.Verbs.Commits;
using GitTool.Cli.Verbs.Complexity;
using GitTool.Cli.Verbs.Correlation;
using GitTool.Cli.Verbs.FollowFile;
using Microsoft.Extensions.DependencyInjection;

namespace GitTool.Cli.Verbs
{
    public static class Configure
    {
        public static IServiceCollection ConfigureVerbs(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<CommitCsvVerb>()
                .AddTransient<CorrelationVerb>()
                .AddTransient<ComplexityVerb>()
                .AddTransient<FollowFileVerb>()
                ;

            return serviceCollection;
        }
    }
}