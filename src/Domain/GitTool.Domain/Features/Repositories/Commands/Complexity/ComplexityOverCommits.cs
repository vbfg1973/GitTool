using MediatR;

namespace GitTool.Domain.Features.Repositories.Commands.Complexity
{
    public record ComplexityOverCommits(string RepositoryPath, string CsvFile) : IRequest;
}