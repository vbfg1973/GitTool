using System.Collections.Concurrent;
using GitTool.Infrastructure.Git.Models;

namespace GitTool.Cli.Verbs.CoOccurrence.Services
{
    public interface ICoOccurenceStatistics
    {
        ConcurrentDictionary<string, ConcurrentDictionary<string, int>> ByFile { get; }
        int CommitCount { get; set; }
        Task AddGitLog(GitLog gitLog);
        IEnumerable<Models.CoOccurrence> CoOccurrences();
        IEnumerable<Models.CoOccurrence> CoOccurrencesByFile(string file);
    }
}