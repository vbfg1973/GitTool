namespace CodeTool.Infrastructure.Git.Models.Diff
{
    public record GitFileDiffChunkPart(char Identifier, int Start, int Length, GitFileDiffChunkPartLines ChunkLines);
}