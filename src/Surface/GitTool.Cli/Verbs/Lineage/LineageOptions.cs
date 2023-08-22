using System.Text.Json;
using CommandLine;
using GitTool.Cli.Verbs.Abstract;
using GitTool.Infrastructure.Git;
using Microsoft.Extensions.Logging;

namespace GitTool.Cli.Verbs.Lineage
{
    [Verb("lineage")]
    public class LineageOptions : IRepositoryOptions
    {
        public string RepositoryPath { get; init; } = null!;

        [Option('s', nameof(Sha))]
        public string Sha { get; set; } = null!;
    }

    public class LineageVerb
    {
        private readonly IGitService _gitService;
        private readonly ILogger<LineageVerb> _logger;

        public LineageVerb(IGitService gitService, ILogger<LineageVerb> logger)
        {
            _gitService = gitService;
            _logger = logger;
        }

        public async Task Run(LineageOptions options)
        {
            var parents = _gitService.Parents(options.RepositoryPath, options.Sha);

            Console.WriteLine(JsonSerializer.Serialize(parents, JsonSerializerOptions.Default));
        }
    }
}