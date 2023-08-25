using GitTool.Infrastructure.Git.ProcessRunner.Commands.Abstract;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Infrastructure.Git.ProcessRunner.Commands
{
    public class GitCommitCount : GitRevListCommandLineArguments
    {
        public GitCommitCount(RepositoryDetails repositoryDetails) : base(repositoryDetails, new GitPageParameters())
        {
        }

        public override IEnumerable<string> Arguments()
        {
            foreach (var argument in Preamble()) yield return argument;

            yield return "--count";

            yield return Branch();
        }
    }
}