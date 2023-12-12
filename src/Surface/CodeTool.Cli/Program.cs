using CodeTool.Cli.Verbs.Commits;
using CodeTool.Cli.Verbs.CoOccurrence;
using CodeTool.Cli.Verbs.Count;
using CodeTool.Cli.Verbs.Lineage;
using CommandLine;
using CodeTool.Cli.Verbs;
using CodeTool.Domain;
using CodeTool.Infrastructure.Git;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CodeTool.Cli
{
    internal static class Program
    {
        private static IConfiguration _sConfiguration = null!;
        private static IServiceCollection _sServiceCollection = null!;
        private static IServiceProvider _sServiceProvider = null!;
        private static readonly CancellationTokenSource CanTokenSource = new CancellationTokenSource();

        internal static void Main(string[] args)
        {
            BuildConfiguration();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(_sConfiguration)
                .CreateLogger();
            ConfigureServices();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                Console.WriteLine("Cancel event triggered");
                CanTokenSource.Cancel();
                eventArgs.Cancel = true;
            };

            ParseCommandLine(args);
        }

        private static void ParseCommandLine(string[] args)
        {
            Parser.Default
                .ParseArguments<
                    CommitOptions,
                    CountOptions,
                    CoOccurrenceOptions,
                    LineageOptions
                >(args)
                .WithParsed<CountOptions>(options =>
                {
                    var verb = _sServiceProvider.GetService<CountVerb>();

                    verb?.Run(options, CanTokenSource.Token).Wait();
                })
                .WithParsed<CommitOptions>(options =>
                {
                    var verb = _sServiceProvider.GetService<CommitVerb>();

                    verb?.Run(options, CanTokenSource.Token).Wait();
                })
                .WithParsed<CoOccurrenceOptions>(options =>
                {
                    var verb = _sServiceProvider.GetService<CoOccurrenceVerb>();

                    verb?.Run(options, CanTokenSource.Token).Wait();
                })
                .WithParsed<LineageOptions>(options =>
                {
                    var verb = _sServiceProvider.GetService<LineageVerb>();

                    verb?.Run(options, CanTokenSource.Token).Wait();
                })
                ;
        }

        private static void BuildConfiguration()
        {
            ConfigurationBuilder configuration = new();

            _sConfiguration = configuration.AddJsonFile("appsettings.json", true, true)
                // .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static void ConfigureServices()
        {
            _sServiceCollection = new ServiceCollection();

            // var appSettings = new AppSettings();
            // s_configuration.Bind("Settings", appSettings);

            _sServiceCollection.AddLogging(configure => configure.AddSerilog());

            _sServiceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(DomainAssemblyReference.Assembly));

            _sServiceCollection.AddGitServices();

            _sServiceCollection.ConfigureVerbs();

            _sServiceProvider = _sServiceCollection.BuildServiceProvider();
        }
    }
}