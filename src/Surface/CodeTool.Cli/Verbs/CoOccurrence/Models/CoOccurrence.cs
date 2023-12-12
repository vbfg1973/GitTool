namespace CodeTool.Cli.Verbs.CoOccurrence.Models
{
    public record CoOccurrence(string FileName, string AssociatedFilename, int CommitCount, int Count)
    {
        public double Ratio => Math.Round(Convert.ToDouble(Count) / Convert.ToDouble(CommitCount), 4);

        public bool BothHaveExtension(string extension)
        {
            return Path.GetExtension(FileName) == extension && Path.GetExtension(AssociatedFilename) == extension;
        }
        
        public bool OneHasExtension(string extension)
        {
            return Path.GetExtension(FileName) == extension || Path.GetExtension(AssociatedFilename) == extension;
        }
    };
}