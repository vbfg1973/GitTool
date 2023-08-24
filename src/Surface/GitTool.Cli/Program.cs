using CommandLine;
using GitTool.Cli.Verbs;
using GitTool.Cli.Verbs.Count;
using GitTool.Domain;
using GitTool.Infrastructure.Git;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

internal static class Program
{
    private static IConfiguration s_configuration;
    private static IServiceCollection s_serviceCollection;
    private static IServiceProvider s_serviceProvider;
    private static readonly CancellationTokenSource canTokenSource = new CancellationTokenSource();

    internal static void Main(string[] args)
    {
        BuildConfiguration();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(s_configuration)
            .CreateLogger();
        ConfigureServices();

        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            Console.WriteLine("Cancel event triggered");
            canTokenSource.Cancel();
            eventArgs.Cancel = true;
        };
        
        ParseCommandLine(args);
    }

    private static void ParseCommandLine(string[] args)
    {
        Parser.Default
            .ParseArguments<
                CountOptions
            >(args)
            .WithParsed<CountOptions>(options =>
            {
                var verb = s_serviceProvider.GetService<CountVerb>();

                verb?.Run(options, canTokenSource.Token).Wait();
            })
            ;
    }

    private static void BuildConfiguration()
    {
        ConfigurationBuilder configuration = new();

        s_configuration = configuration.AddJsonFile("appsettings.json", true, true)
            // .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static void ConfigureServices()
    {
        s_serviceCollection = new ServiceCollection();

        // var appSettings = new AppSettings();
        // s_configuration.Bind("Settings", appSettings);

        s_serviceCollection.AddLogging(configure => configure.AddSerilog());

        s_serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(DomainAssemblyReference.Assembly));

        s_serviceCollection.AddGitServices();

        s_serviceCollection.ConfigureVerbs();

        s_serviceProvider = s_serviceCollection.BuildServiceProvider();
    }
}