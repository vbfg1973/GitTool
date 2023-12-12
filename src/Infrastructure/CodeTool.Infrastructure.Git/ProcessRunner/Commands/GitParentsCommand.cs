using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Abstract;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Infrastructure.Git.ProcessRunner.Commands
{
    public class GitParentsCommand : GitRevListCommandLineArguments
    {
        public GitParentsCommand(RepositoryDetails repositoryDetails) : base(repositoryDetails, new GitPageParameters())
        {
        }

        public override IEnumerable<string> Arguments()
        {
            foreach (var argument in Preamble()) yield return argument;

            yield return "--all";
            yield return "--parents";

            yield return Branch();
        }
    }
}