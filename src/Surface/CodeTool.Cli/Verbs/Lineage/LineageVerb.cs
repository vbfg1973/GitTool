using System.Text;
using CodeTool.Infrastructure.Git;
using CodeTool.Infrastructure.Git.Models;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Cli.Verbs.Lineage
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

            var gitLineage = new GitLineageService(_gitService);
            await gitLineage.AddLineages(_gitService.GetLineage(details, ctx));

            await foreach (var gitCommitLineage in _gitService.GetLineage(details, ctx))
            {
                Console.WriteLine(CommitLineageString(gitCommitLineage));
                
                Console.WriteLine(string.Join("\t\n", gitLineage.Parents(gitCommitLineage.Sha)));
                Console.WriteLine();
            }
        }

        private static string CommitLineageString(GitCommitLineage gitCommitLineage)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id: {gitCommitLineage.Sha}");

            foreach (var r in gitCommitLineage.Parents) sb.AppendLine($"\tParent: {r}");

            sb.AppendLine();

            return sb.ToString();
        }
    }
}