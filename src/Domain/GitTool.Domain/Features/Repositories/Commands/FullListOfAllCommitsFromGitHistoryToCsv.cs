using MediatR;

namespace GitTool.Domain.Features.Repositories.Commands
{
    public record FullListOfAllCommitsFromGitHistoryToCsv(string RepositoryPath, string CsvFile) : IRequest;
}