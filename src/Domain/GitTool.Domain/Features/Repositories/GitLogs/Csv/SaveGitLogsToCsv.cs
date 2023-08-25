using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;
using MediatR;

namespace GitTool.Domain.Features.Repositories.GitLogs.Csv
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SaveGitLogsToCsv : IRequest
    {
        public SaveGitLogsToCsv(RepositoryDetails repositoryDetails, string csvFile)
        {
            RepositoryDetails = repositoryDetails;
            CsvFile = csvFile;
        }

        public RepositoryDetails RepositoryDetails { get; init; }
        public string CsvFile { get; init; }
    }
}