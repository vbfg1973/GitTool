using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Infrastructure.Git.ProcessRunner.Commands.Abstract
{
    public abstract class GitLogCommandLineArguments : GitProcessCommandLineArguments
    {
        protected GitLogCommandLineArguments(RepositoryDetails repositoryDetails, GitPaging gitPaging) : base(
            repositoryDetails,
            gitPaging)
        {
            Command = "log";
        }
    }
}