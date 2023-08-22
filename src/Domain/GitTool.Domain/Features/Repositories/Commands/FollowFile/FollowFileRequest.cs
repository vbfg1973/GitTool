using System.Collections.Concurrent;
using GitTool.Domain.Complexity;
using GitTool.Domain.Complexity.Abstract;
using GitTool.Domain.Features.Repositories.Commands.Complexity;
using GitTool.Domain.Helpers;
using GitTool.Infrastructure.Git;
using MediatR;

namespace GitTool.Domain.Features.Repositories.Commands.FollowFile
{
    public record FollowFileRequest(string RepositoryPath, string FilePath, string CsvFile) : IRequest;

    public class FollowFileRequestHandler : IRequestHandler<FollowFileRequest>
    {
        private readonly IGitService _gitService;

        public FollowFileRequestHandler(IGitService gitService)
        {
            _gitService = gitService;
        }

        public async Task Handle(FollowFileRequest request, CancellationToken cancellationToken)
        {
            var commits = _gitService.FollowFile(request.RepositoryPath, request.FilePath);

            var cbag = new ConcurrentBag<ComplexityDetail>();
            Parallel.ForEach(commits, commit =>
            {
                var body = _gitService.CommitFileContent(request.RepositoryPath, commit.Sha, request.FilePath);
                var complexityAnalyzer = new IndentationComplexityAnalyzer(body);

                cbag.Add(new ComplexityDetail(commit.Sha, request.FilePath, commit.Date.ToString("O"), complexityAnalyzer.ComplexityScore));
            });
            
            await CsvHelpers.WriteCsvAsync(cbag.OrderByDescending(x => x.Date), request.CsvFile);
        }
    }
}