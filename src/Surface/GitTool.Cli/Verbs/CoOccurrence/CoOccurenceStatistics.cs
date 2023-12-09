// using System.Collections.Concurrent;
// using GitTool.Infrastructure.Git.Models;
//
// namespace GitTool.Cli.Verbs.CoOccurrence
// {
//     public class CoOccurenceStatistics : ICoOccurenceStatistics
//     {
//         private int _commitCount;
//         private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, int>> _byFile;
//
//         public ConcurrentDictionary<string, ConcurrentDictionary<string, int>> ByFile => _byFile;
//
//         public CoOccurenceStatistics()
//         {
//             _byFile = new ConcurrentDictionary<string, ConcurrentDictionary<string, int>>();
//         }
//
//         public int CommitCount
//         {
//             get => _commitCount;
//             set => _commitCount = value;
//         }
//
//         public async Task AddGitLog(GitLog gitLog)
//         {
//             var fileCombinations = gitLog
//                 .Files
//                 .Select(x => x.Path)
//                 .DifferentCombinations(2)
//                 .Select(x => x.ToArray());
//
//             foreach (var combination in fileCombinations)
//             {
//                 await Update(combination[0], combination[1]);
//                 await Update(combination[1], combination[0]);
//             }
//         }
//
//         public IEnumerable<CoOccurrence> CoOccurrences()
//         {
//             // ReSharper disable once LoopCanBeConvertedToQuery
//             foreach (var file in ByFile.Keys)
//             {
//                 foreach (var occurrence in CoOccurrencesByFile(file))
//                 {
//                     yield return occurrence;
//                 }
//             }
//         }
//
//         public IEnumerable<CoOccurrence> CoOccurrencesByFile(string file)
//         {
//             // ReSharper disable once LoopCanBeConvertedToQuery
//             foreach (var assoc in ByFile[file].Keys)
//             {
//                 yield return new CoOccurrence(file, assoc, ByFile[file][assoc]);
//             }
//         }
//
//         private void TryInitializeStorage(string key)
//         {
//             ByFile.TryAdd(key, new ConcurrentDictionary<string, int>());
//         }
//
//         private async Task Update(string file, string associatedFile)
//         {
//             TryInitializeStorage(file);
//             ByFile[file].AddOrUpdate(associatedFile, 1, static (key, value) => value + 1);
//         }
//     }
// }