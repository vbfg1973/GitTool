using System.Collections.Concurrent;
using GitTool.Domain.Complexity;
using GitTool.Domain.Helpers;
using GitTool.Infrastructure.Git;
using GitTool.Infrastructure.Git.Models;
using MediatR;

namespace GitTool.Domain.Features.Repositories.Commands.Complexity
{
    public class ComplexityOverCommitsHandler : IRequestHandler<ComplexityOverCommits>
    {
        private readonly IGitService _gitService;

        public ComplexityOverCommitsHandler(IGitService gitService)
        {
            _gitService = gitService;
        }

        public async Task Handle(ComplexityOverCommits request, CancellationToken cancellationToken)
        {
            var commits = _gitService.GetAllCommits(request.RepositoryPath);

            await CsvHelpers.WriteCsvAsync(ComplexityDetails(request.RepositoryPath, commits), request.CsvFile);
        }

        private IEnumerable<ComplexityDetail> ComplexityDetails(string repositoryPath,
            IEnumerable<GitCommitDetails> commits)
        {
            var count = 0;
            foreach (var commit in commits)
            {
                count++;
                ConcurrentBag<ComplexityDetail> details = new();

                Parallel.ForEach(commit.Files.Where(x => x.ChangeKind != ChangeKind.Deleted), file =>
                {
                    var body = _gitService.CommitFileContent(repositoryPath, commit.Sha, file.Path);

                    var complexityAnalyser = new IndentationComplexityAnalyzer(body);

                    details.Add(new ComplexityDetail(commit.Sha, file.Path, commit.Date.ToString("O"),
                        complexityAnalyser.ComplexityScore));
                });

                foreach (var d in details) yield return d;

                Console.Error.Write(count + "\r");
            }
        }
    }

    public record ComplexityDetail(string Sha, string Path, string Date, int ComplexityScore);
}