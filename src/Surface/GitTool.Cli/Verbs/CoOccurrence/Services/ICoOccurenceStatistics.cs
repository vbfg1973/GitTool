using System.Collections.Concurrent;
using GitTool.Infrastructure.Git.Models;

namespace GitTool.Cli.Verbs.CoOccurrence
{
    public interface ICoOccurenceStatistics
    {
        ConcurrentDictionary<string, ConcurrentDictionary<string, int>> ByFile { get; }
        int CommitCount { get; set; }
        Task AddGitLog(GitLog gitLog);
        IEnumerable<CoOccurrence> CoOccurrences();
        IEnumerable<CoOccurrence> CoOccurrencesByFile(string file);
    }
}