namespace GitTool.Infrastructure.Git.Models
{
    /// <summary>
    ///     The status of the file in the current commit
    /// </summary>
    public record GitFileStatus(ChangeKind ChangeKind, string Path, string OldPath);
}