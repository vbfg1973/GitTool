using System.Text;
using GitTool.Infrastructure.Git;
using GitTool.Infrastructure.Git.Models;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Cli.Verbs.Lineage
{
    public class LineageVerb
    {
        private readonly IGitService _gitService;

        public LineageVerb(IGitService gitService)
        {
            _gitService = gitService;
        }

        public async Task Run(LineageOptions options, CancellationToken ctx)
        {
            var details = new RepositoryDetails(options.RepositoryPath);

            await foreach (var gitCommitLineage in _gitService.GetLineage(details, ctx))
            {
                Console.WriteLine(CommitLineageString(gitCommitLineage));
            }
        }

        private static string CommitLineageString(GitCommitLineage gitCommitLineage)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id: {gitCommitLineage.Sha}");

            foreach (var r in gitCommitLineage.Parents)
            {
                sb.AppendLine($"\tParent: {r}");
            }

            sb.AppendLine();

            return sb.ToString();
        }
    }
}