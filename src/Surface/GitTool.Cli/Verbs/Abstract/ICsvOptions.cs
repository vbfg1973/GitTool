using CommandLine;

namespace GitTool.Cli.Verbs.Abstract
{
    public interface ICsvOptions
    {
        [Option('p', nameof(CsvFile), Required = true, HelpText = "Path to output csv file")] public string CsvFile { get; init; }
    }
}