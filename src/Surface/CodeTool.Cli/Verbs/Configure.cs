using CodeTool.Cli.Verbs.Commits;
using CodeTool.Cli.Verbs.CoOccurrence;
using CodeTool.Cli.Verbs.Count;
using CodeTool.Cli.Verbs.Lineage;
using Microsoft.Extensions.DependencyInjection;

namespace CodeTool.Cli.Verbs
{
    public static class Configure
    {
        public static IServiceCollection ConfigureVerbs(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<CommitVerb>()
                .AddTransient<CountVerb>()
                .AddTransient<CoOccurrenceVerb>()
                .AddTransient<LineageVerb>()
                ;

            return serviceCollection;
        }
    }
}