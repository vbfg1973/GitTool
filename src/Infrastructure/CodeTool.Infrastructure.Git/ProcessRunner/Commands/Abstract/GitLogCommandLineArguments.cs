using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Infrastructure.Git.ProcessRunner.Commands.Abstract
{
    public abstract class GitLogCommandLineArguments : GitProcessCommandLineArguments
    {
        protected GitLogCommandLineArguments(RepositoryDetails repositoryDetails, GitPageParameters gitPaging) : base(
            repositoryDetails,
            gitPaging)
        {
            Command = "log";
        }
    }
}