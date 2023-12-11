using GitTool.Cli.Verbs.Commits;
using GitTool.Cli.Verbs.CoOccurrence;
using GitTool.Cli.Verbs.Count;
using Microsoft.Extensions.DependencyInjection;

namespace GitTool.Cli.Verbs
{
    public static class Configure
    {
        public static IServiceCollection ConfigureVerbs(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<CommitVerb>()
                .AddTransient<CountVerb>()
                .AddTransient<CoOccurrenceVerb>()
                ;

            return serviceCollection;
        }
    }
}