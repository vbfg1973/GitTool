using GitTool.Cli.Verbs.Commits;
using GitTool.Cli.Verbs.Complexity;
using GitTool.Cli.Verbs.Correlation;
using GitTool.Cli.Verbs.Count;
using GitTool.Cli.Verbs.FollowFile;
using GitTool.Cli.Verbs.Lineage;
using GitTool.Cli.Verbs.ReverseTopographical;
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
                .AddTransient<LineageVerb>()
                .AddTransient<ReverseTopographicalVerb>()
                .AddTransient<CountVerb>()
                ;

            return serviceCollection;
        }
    }
}