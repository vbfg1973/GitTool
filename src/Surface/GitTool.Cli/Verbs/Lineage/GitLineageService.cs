using GitTool.Infrastructure.Git;
using GitTool.Infrastructure.Git.Models;
using QuickGraph;

namespace GitTool.Cli.Verbs.Lineage
{
    public interface IGitLineageService
    {
        Task AddLineages(IAsyncEnumerable<GitCommitLineage> fullLineage);
        IEnumerable<string> Parents(string sha);
        IEnumerable<string> Children(string sha);
    }

    public class GitLineageService : IGitLineageService
    {
        private readonly BidirectionalGraph<string, Edge<string>> _children;
        private readonly BidirectionalGraph<string, Edge<string>> _parentage;

        public GitLineageService(IGitService gitService)
        {
            _parentage = new BidirectionalGraph<string, Edge<string>>();
            _children = new BidirectionalGraph<string, Edge<string>>();
        }

        public async Task AddLineages(IAsyncEnumerable<GitCommitLineage> fullLineage)
        {
            await foreach (var commitLineage in fullLineage) AddCommitLineage(commitLineage);
        }

        public IEnumerable<string> Parents(string sha)
        {
            if (_parentage.Vertices.Contains(sha) && _children.Vertices.Contains(sha))
                return _parentage.OutEdges(sha).Select(x => x.Target);

            return Enumerable.Empty<string>();
        }

        public IEnumerable<string> Children(string sha)
        {
            if (_parentage.Vertices.Contains(sha) && _children.Vertices.Contains(sha))
                return _children.OutEdges(sha).Select(x => x.Target);

            return Enumerable.Empty<string>();
        }

        private void AddCommitLineage(GitCommitLineage commitLineage)
        {
            foreach (var parent in commitLineage.Parents)
            {
                var parentEdge = new Edge<string>(commitLineage.Sha, parent);
                _parentage.AddVerticesAndEdge(parentEdge);

                var childEdge = new Edge<string>(parent, commitLineage.Sha);
                _children.AddVerticesAndEdge(childEdge);
            }
        }
    }
}