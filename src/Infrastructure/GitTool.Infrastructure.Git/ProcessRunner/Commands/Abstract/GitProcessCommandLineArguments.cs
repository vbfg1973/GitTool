using GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace GitTool.Infrastructure.Git.ProcessRunner.Commands.Abstract
{
    public abstract class GitProcessCommandLineArguments
    {
        private readonly GitPageParameters _pageParameters;
        private readonly RepositoryDetails _repositoryDetails;

        protected GitProcessCommandLineArguments(RepositoryDetails repositoryDetails, GitPageParameters pageParameters)
        {
            _repositoryDetails = repositoryDetails;
            _pageParameters = pageParameters;
            ProcessName = "git";
        }

        protected string Command { get; init; } = null!;

        public string ProcessName { get; }

        private string Take(int n)
        {
            return $"-n {_pageParameters.Take}";
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
            yield return Take(_pageParameters.Take);
            yield return Skip(_pageParameters.Skip);
        }

        public abstract IEnumerable<string> Arguments();
    }
}