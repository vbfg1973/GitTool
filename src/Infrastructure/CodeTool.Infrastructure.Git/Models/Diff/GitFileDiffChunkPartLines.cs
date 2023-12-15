namespace CodeTool.Infrastructure.Git.Models.Diff
{
    public class GitFileDiffChunkPartLines : List<string>, IEquatable<GitFileDiffChunkPartLines>
    {
        public bool Equals(GitFileDiffChunkPartLines? other)
        {
            return !ReferenceEquals(null, other) && this.Order().SequenceEqual(other.Order());
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((GitFileDiffChunkPartLines)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.Aggregate(19, (current, line) => current * 31 + line.GetHashCode());
            }
        }
    }
}