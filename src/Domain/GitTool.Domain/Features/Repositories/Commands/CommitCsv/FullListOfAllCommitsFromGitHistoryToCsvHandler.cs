using GitTool.Domain.Helpers;
using GitTool.Infrastructure.Git;
using GitTool.Infrastructure.Git.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GitTool.Domain.Features.Repositories.Commands.CommitCsv
{
    public class FullListOfAllCommitsFromGitHistoryToCsvHandler
        : IRequestHandler<FullListOfAllCommitsFromGitHistoryToCsv>
    {
        private readonly IGitService _gitService;
        private readonly ILogger<FullListOfAllCommitsFromGitHistoryToCsvHandler> _logger;

        public FullListOfAllCommitsFromGitHistoryToCsvHandler(IGitService gitService,
            ILogger<FullListOfAllCommitsFromGitHistoryToCsvHandler> logger)
        {
            _gitService = gitService;
            _logger = logger;
        }

        public async Task Handle(FullListOfAllCommitsFromGitHistoryToCsv request,
            CancellationToken cancellationToken)
        {
            var allCommits = _gitService
                .GetAllCommits(request.RepositoryPath)
                .SelectMany(Map);

            await CsvHelpers.WriteCsvAsync(allCommits, request.CsvFile);
        }

        private IEnumerable<FileCommitDetail> Map(GitCommitDetails gitCommitDetails)
        {
            return gitCommitDetails.Files.Select(file => new FileCommitDetail
            {
                Sha = gitCommitDetails.Sha,
                AuthorName = gitCommitDetails.Author.Name,
                AuthorEmail = gitCommitDetails.Author.Email,
                Date = gitCommitDetails.Date.ToString("O"),
                CurrentPath = file.Path,
                OldPath = file.OldPath,
                ChangeKind = file.ChangeKind.ToString()
            });
        }
    }
}