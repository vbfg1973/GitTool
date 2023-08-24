using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Infrastructure.Git.ProcessRunner.Commands.Abstract
{
    public abstract class GitProcessCommandLineArguments
    {
        private readonly GitPaging _gitPaging;
        private readonly RepositoryDetails _repositoryDetails;

        protected GitProcessCommandLineArguments(RepositoryDetails repositoryDetails, GitPaging gitPaging)
        {
            _repositoryDetails = repositoryDetails;
            _gitPaging = gitPaging;
            ProcessName = "git";
        }

        protected string Command { get; init; } = null!;

        public string ProcessName { get; }

        private string Take(int n)
        {
            return $"-n {_gitPaging.Take}";
        }

        private string Skip(int n)
        {
            return $"--skip={n}";
        }

        protected IEnumerable<string> Preamble()
        {
            var a = new[]
            {
                Path.Combine($"--git-dir={_repositoryDetails.RepositoryPath}", ".git"),
                $"--work-tree={_repositoryDetails.RepositoryPath}",
                Command
            };

            return a;
        }

        protected IEnumerable<string> PostAmble()
        {
            foreach (var argument in Paging()) yield return argument;

            yield return _repositoryDetails.Branch;
        }

        protected string Branch()
        {
            return _repositoryDetails.Branch;
        }

        protected IEnumerable<string> Paging()
        {
            if (!_gitPaging.UsePaging) yield break;

            yield return Take(_gitPaging.Take);
            yield return Skip(_gitPaging.Skip);
        }

        public abstract IEnumerable<string> Arguments();
    }
}