using GitTool.Infrastructure.Git.ProcessRunner.Commands.Abstract;
using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Infrastructure.Git.ProcessRunner.Commands
{
    public class GitLogBasicPaging : GitLogCommandLineArguments
    {
        private readonly bool _includeFiles;

        public GitLogBasicPaging
        (
            RepositoryDetails repositoryDetails,
            GitPaging gitPaging,
            bool includeFiles = false
        ) :
            base(repositoryDetails, gitPaging)
        {
            _includeFiles = includeFiles;
        }

        public override IEnumerable<string> Arguments()
        {
            foreach (var argument in Preamble()) yield return argument;

            if (_includeFiles) yield return "--name-status";

            foreach (var argument in Paging()) yield return argument;
        }
    }
}