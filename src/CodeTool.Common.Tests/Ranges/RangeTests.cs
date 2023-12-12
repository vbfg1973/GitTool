using CodeTool.Common.Ranges;
using FluentAssertions;

namespace CodeTool.Common.Tests.Ranges
{
    public class RangeTests
    {
        [Theory]
        [InlineData(0, 10, 1, 2)]
        [InlineData(0, 10, 1, 9)]
        [InlineData(0, 10, 0, 9)]
        [InlineData(0, 10, 1, 10)]
        [InlineData(0, 10, 0, 10)]
        public void ThisWrapsOther(int thisMin, int thisMax, int otherMin, int otherMax)
        {
            var thisRange = new Range<int>(thisMin, thisMax);
            var otherRange = new Range<int>(otherMin, otherMax);

            thisRange.Wraps(otherRange)
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData(0, 10, -1, 2)]
        [InlineData(0, 10, 1, 11)]
        [InlineData(0, 10, -1, 11)]
        public void ThisDoesNotWrapOther(int thisMin, int thisMax, int otherMin, int otherMax)
        {
            var thisRange = new Range<int>(thisMin, thisMax);
            var otherRange = new Range<int>(otherMin, otherMax);

            thisRange.Wraps(otherRange)
                .Should()
                .BeFalse();
        }

        [Theory]
        [InlineData(0, 10, 0)]
        [InlineData(0, 10, 1)]
        [InlineData(0, 10, 10)]
        public void IsInside(int thisMin, int thisMax, int point)
        {
            var thisRange = new Range<int>(thisMin, thisMax);

            thisRange.IsInside(point)
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData(0, 10, -1)]
        [InlineData(0, 10, 11)]
        public void IsNotInside(int thisMin, int thisMax, int point)
        {
            var thisRange = new Range<int>(thisMin, thisMax);

            thisRange.IsInside(point)
                .Should()
                .BeFalse();
        }
    }
}