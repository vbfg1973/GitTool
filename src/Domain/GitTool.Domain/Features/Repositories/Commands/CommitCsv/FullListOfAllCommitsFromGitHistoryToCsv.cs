using MediatR;

namespace GitTool.Domain.Features.Repositories.Commands.CommitCsv
{
    public record FullListOfAllCommitsFromGitHistoryToCsv(string RepositoryPath, string CsvFile) : IRequest;
}