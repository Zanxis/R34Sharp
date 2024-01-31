using R34Sharp.Enums;
using R34Sharp.Search;

namespace R34Sharp.Tests.Search
{
    public sealed class R34TagsSearchBuilderTests
    {
        [Fact]
        public void Constructor_DefaultValues_Success()
        {
            // Arrange
            R34TagsSearchBuilder searchBuilder = new();

            // Act & Assert
            Assert.Equal(R34TagSearchType.Name, searchBuilder.SearchType);
            Assert.Empty(searchBuilder.Search);
            Assert.Equal(100, searchBuilder.Limit);
        }

        [Fact]
        public void WithSearchType_ValidValue_ReturnsBuilderWithSearchType()
        {
            // Arrange
            R34TagsSearchBuilder searchBuilder = new();

            // Act
            R34TagsSearchBuilder result = searchBuilder.WithSearchType(R34TagSearchType.Pattern);

            // Assert
            Assert.Equal(R34TagSearchType.Pattern, searchBuilder.SearchType);
            Assert.Same(searchBuilder, result);
        }

        [Fact]
        public void WithSearch_ValidValue_ReturnsBuilderWithSearch()
        {
            // Arrange
            R34TagsSearchBuilder searchBuilder = new();

            // Act
            R34TagsSearchBuilder result = searchBuilder.WithSearch("tag123");

            // Assert
            Assert.Equal("tag123", searchBuilder.Search);
            Assert.Same(searchBuilder, result);
        }

        [Fact]
        public void WithLimit_ValidValue_ReturnsBuilderWithLimit()
        {
            // Arrange
            R34TagsSearchBuilder searchBuilder = new();

            // Act
            R34TagsSearchBuilder result = searchBuilder.WithLimit(50);

            // Assert
            Assert.Equal(50, searchBuilder.Limit);
            Assert.Same(searchBuilder, result);
        }

        [Theory]
        [InlineData(R34TagSearchType.Name)]
        [InlineData(R34TagSearchType.Pattern)]
        [InlineData(R34TagSearchType.Id)]
        public void WithSearchType_SetValidValues_Success(R34TagSearchType searchType)
        {
            // Arrange
            R34TagsSearchBuilder searchBuilder = new();

            // Act
            R34TagsSearchBuilder result = searchBuilder.WithSearchType(searchType);

            // Assert
            Assert.Equal(searchType, searchBuilder.SearchType);
            Assert.Same(searchBuilder, result);
        }
    }
}
