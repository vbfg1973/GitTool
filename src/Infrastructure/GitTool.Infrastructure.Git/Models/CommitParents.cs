namespace GitTool.Infrastructure.Git.Models
{
    public class CommitParents : List<string>, IEquatable<CommitParents>
    {
        public bool Equals(CommitParents? other)
        {
            return !ReferenceEquals(null, other) && this.OrderBy(x => x).SequenceEqual(other.OrderBy(x => x));
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CommitParents)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.Aggregate(19, (current, foo) => current * 31 + foo.GetHashCode());
            }
        }
    }
}