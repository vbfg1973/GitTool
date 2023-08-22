using CommandLine;
using GitTool.Cli.Verbs.Abstract;

namespace GitTool.Cli.Verbs.FollowFile
{
    [Verb("follow")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FollowFileOptions : IRepositoryOptions, ICsvOptions
    {
        public string RepositoryPath { get; init; } = null!;
        public string CsvFile { get; init; } = null!;
        
        [Option('f', nameof(FileToTrack), Required = true, HelpText = "The file to track through the git history")] public string FileToTrack { get; init; } = null!;
    }
}