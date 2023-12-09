﻿using System.Collections.Concurrent;
using GitTool.Infrastructure.Git.Models;
using QuickGraph;

namespace GitTool.Cli.Verbs.CoOccurrence
{
    public class CoOccurrenceStatisticsGraph : ICoOccurenceStatistics
    {
        private readonly AdjacencyGraph<string, Edge<string>> _graph;
        private readonly ConcurrentDictionary<string, int> _fileCommitCounts;
        public CoOccurrenceStatisticsGraph()
        {
            _graph = new AdjacencyGraph<string, Edge<string>>();
            _fileCommitCounts = new ConcurrentDictionary<string, int>();
        }

        public ConcurrentDictionary<string, ConcurrentDictionary<string, int>> ByFile { get; }
        public int CommitCount { get; set; }

        public async Task AddGitLog(GitLog gitLog)
        {
            var vertices = gitLog.Files.Select(x => x.Path);
            _graph.AddVertexRange(vertices);
            
            var edges = GetEdgesFromFiles(gitLog).ToList();
            _graph.AddEdgeRange(edges);

            foreach (var file in gitLog.Files)
            {
                _fileCommitCounts.AddOrUpdate(file.Path, 1, (key, value) => value + 1);
            }
        }

        public IEnumerable<CoOccurrence> CoOccurrences()
        {
            foreach (var v in _graph.Vertices)
            {
                var occurrences = _graph.OutEdges(v)
                    .GroupBy(edge => edge.Target)
                    .Select(grouping => new CoOccurrence(v, grouping.Key, _fileCommitCounts[v], grouping.Count()));

                foreach (var occurrence in occurrences)
                {
                    yield return occurrence;
                }
            }
        }

        public IEnumerable<CoOccurrence> CoOccurrencesByFile(string file)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Edge<string>> GetEdgesFromFiles(GitLog gitLog)
        {
            return from file in gitLog.Files
                from assocFile in gitLog.Files
                where !Equals(file, assocFile)
                select new Edge<string>(file.Path, assocFile.Path);
        }
    }
}