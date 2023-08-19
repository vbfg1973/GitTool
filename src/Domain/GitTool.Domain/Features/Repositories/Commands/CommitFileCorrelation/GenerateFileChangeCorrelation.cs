using MediatR;

namespace GitTool.Domain.Features.Repositories.Commands.CommitFileCorrelation
{
    public record GenerateFileChangeCorrelation(string RepositoryPath, string CsvFile) : IRequest;
}