namespace GitTool.Infrastructure.Git.ProcessRunner.Commands.Parameters
{
    public class RepositoryDetails
    {
        public RepositoryDetails(string repositoryPath, string branch)
        {
            RepositoryPath = repositoryPath;
            Branch = branch;
        }

        public RepositoryDetails(string repositoryPath)
        {
            RepositoryPath = repositoryPath;
            Branch = "HEAD";
        }

        public string RepositoryPath { get; init; }
        public string Branch { get; init; }
    }
}