using FluentAssertions;

namespace GitTool.Infrastructure.Git.Tests.PagingTests
{
    public class PageParameters
    {
        [Theory]
        [InlineData(1, 100, 0, 100)]
        [InlineData(2, 100, 100, 100)]
        [InlineData(3, 100, 200, 100)]
        [InlineData(4, 100, 300, 100)]
        [InlineData(5, 100, 400, 100)]
        public void Given_Page_Parameters_Skip_And_Take_Are_Calculated_Correctly(int page, int pageSize, int expectedSkip, int expectedTake)
        {
            var a = new GitPageParameters { Page = page, PageSize = pageSize };
            a.Skip.Should().Be(expectedSkip);
            a.Take.Should().Be(expectedTake);
        }
    }
}