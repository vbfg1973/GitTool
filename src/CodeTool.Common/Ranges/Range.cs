namespace CodeTool.Common.Ranges
{
    public class Range<T> : IRange<T> where T : IComparable<T>
    {
        public Range(T min, T max)
        {
            Min = min;
            Max = max;
        }

        public T Min { get; }
        public T Max { get; }

        public bool IsInside(T point)
        {
            return point.CompareTo(Min) >= 0 &&
                   point.CompareTo(Max) <= 0;
        }

        public bool OverLaps(IRange<T> other)
        {
            return IsInside(other.Min) ||
                   IsInside(other.Max);
        }

        public bool Wraps(IRange<T> other)
        {
            return IsInside(other.Min) &&
                   IsInside(other.Max);
        }

        public bool IsWrappedBy(IRange<T> other)
        {
            return other.Wraps(this);
        }
    }
}