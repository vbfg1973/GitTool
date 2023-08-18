using MediatR;

namespace GitTool.Domain.Features.Repositories.Queries
{
    public record GetFullListOfAllFilesFromGitHistory(string RepositoryPath) : IRequest;
}