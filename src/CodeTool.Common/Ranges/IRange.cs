namespace CodeTool.Common.Ranges
{
    public interface IRange<T> where T : IComparable<T>
    {
        T Min { get; }
        T Max { get; }
        bool OverLaps(IRange<T> other);
        bool Wraps(IRange<T> other);
        bool IsWrappedBy(IRange<T> other);
        bool IsInside(T point);
    }
}