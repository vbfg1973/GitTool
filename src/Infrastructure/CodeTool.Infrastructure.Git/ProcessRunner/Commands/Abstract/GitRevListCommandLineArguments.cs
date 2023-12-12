using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Infrastructure.Git.ProcessRunner.Commands.Abstract
{
    public abstract class GitRevListCommandLineArguments : GitProcessCommandLineArguments
    {
        protected GitRevListCommandLineArguments(RepositoryDetails repositoryDetails, GitPageParameters pageParameters) : base(
            repositoryDetails,
            pageParameters)
        {
            Command = "rev-list";
        }
    }
}