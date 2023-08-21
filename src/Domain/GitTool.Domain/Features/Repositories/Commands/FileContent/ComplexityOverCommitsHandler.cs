using GitTool.Domain.Complexity;
using GitTool.Infrastructure.Git;
using GitTool.Infrastructure.Git.Models;
using MediatR;

namespace GitTool.Domain.Features.Repositories.Commands.FileContent
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

            foreach (var commit in commits)
            {
                foreach (var file in commit.Files.Where(x => x.ChangeKind != ChangeKind.Deleted))
                {
                    var body = _gitService.CommitFileContent(request.RepositoryPath, commit.Sha, file.Path);

                    var complexityAnalyser = new IndentationComplexityAnalyzer(body);

                    Console.WriteLine($"{commit.Sha} {file.Path} {commit.Date:O} {complexityAnalyser.ComplexityScore}");
                }
            }
        }
    }
}