using CommandLine;

namespace GitTool.Cli.Verbs.FollowFile
{
    [Verb("follow")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FollowFileOptions
    {
        [Option('p', nameof(RepositoryPath))] public string RepositoryPath { get; set; } = null!;

        [Option('c', nameof(CsvFile))] public string CsvFile { get; set; } = null!;
        [Option('f', nameof(File))] public string File { get; set; } = null!;
    }
}