using MediatR;

namespace GitTool.Domain.Features.Repositories.Commands.FileContent
{
    public record ComplexityOverCommits(string RepositoryPath, string CsvFile) : IRequest;
}