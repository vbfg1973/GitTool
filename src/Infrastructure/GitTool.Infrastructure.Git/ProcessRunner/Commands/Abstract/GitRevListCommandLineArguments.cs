using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Infrastructure.Git.ProcessRunner.Commands.Abstract
{
    public abstract class GitRevListCommandLineArguments : GitProcessCommandLineArguments
    {
        protected GitRevListCommandLineArguments(RepositoryDetails repositoryDetails, GitPaging gitPaging) : base(
            repositoryDetails,
            gitPaging)
        {
            Command = "rev-list";
        }
    }
}