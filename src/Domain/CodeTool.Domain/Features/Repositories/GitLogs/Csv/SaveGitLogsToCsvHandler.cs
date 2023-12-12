using CodeTool.Domain.Features.Repositories.GitLogs.GetPage;
using CodeTool.Domain.Helpers;
using CodeTool.Infrastructure.Git;
using CodeTool.Infrastructure.Git.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CodeTool.Domain.Features.Repositories.GitLogs.Csv
{
    public class SaveGitLogsToCsvHandler : IRequestHandler<SaveGitLogsToCsv>
    {
        private readonly IGitService _gitService;
        private readonly ILogger<SaveGitLogsToCsvHandler> _logger;
        private readonly IMediator _mediator;

        public SaveGitLogsToCsvHandler(IMediator mediator, IGitService gitService,
            ILogger<SaveGitLogsToCsvHandler> logger)
        {
            _mediator = mediator;
            _gitService = gitService;
            _logger = logger;
        }

        public async Task Handle(SaveGitLogsToCsv request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Saving git logs from {Repository} to {CsvFile}", request.RepositoryDetails.RepositoryPath, request.CsvFile);
            
            var commitCount = await _gitService.CountCommits(request.RepositoryDetails, cancellationToken);
            
            _logger.LogTrace("Commit count in {Repository} is {CommitCount}", request.RepositoryDetails.RepositoryPath, commitCount);

            await Console.Error.WriteLineAsync($"{commitCount}");
            
            var gitLogPageRequest = new GetGitLogPage(request.RepositoryDetails,
                new GitPageParameters() { Page = 1, PageSize = commitCount });
            
            var gitLogEnumerable = Map(_mediator.CreateStream(gitLogPageRequest, cancellationToken));

            await CsvHelpers.WriteCsvAsync(gitLogEnumerable, request.CsvFile);
        }

        private async IAsyncEnumerable<GitLogDto> Map(IAsyncEnumerable<GitLog> gitLogs)
        {
            await foreach (var gitLog in gitLogs)
            {
                foreach (var dto in Map(gitLog))
                {
                    yield return dto;
                }
            }
        }

        private IEnumerable<GitLogDto> Map(GitLog gitLog)
        {
            return gitLog.Files.Select(file => new GitLogDto()
            {
                Sha = gitLog.Sha,
                Date = gitLog.Date.ToString("O"),
                Merge = gitLog.Merge,
                AuthorName = gitLog.Author.Name,
                AuthorEmail = gitLog.Author.Email,
                Path = file.Path,
                OldPath = file.OldPath,
                ChangeKind = file.ChangeKind.ToString()
            });
        }
    }
}