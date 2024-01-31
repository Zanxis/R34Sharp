using R34Sharp.Models;
using R34Sharp.Search;

namespace R34Sharp.Tests.Search
{
    public class R34PostsSearchBuilderTests
    {
        [Fact]
        public void Constructor_DefaultValues_Success()
        {
            // Arrange
            R34PostsSearchBuilder searchBuilder = new();

            // Act & Assert
            Assert.Equal(100, searchBuilder.Limit);
            Assert.Equal(0ul, searchBuilder.Id.Value);
            Assert.Equal(0, searchBuilder.Offset.Value);
            Assert.Empty(searchBuilder.Tags);
            Assert.Empty(searchBuilder.BlockedTags);
        }

        [Fact]
        public void WithLimit_ValidValue_ReturnsBuilderWithLimit()
        {
            // Arrange
            R34PostsSearchBuilder searchBuilder = new();

            // Act
            R34PostsSearchBuilder result = searchBuilder.WithLimit(200);

            // Assert
            Assert.Equal(200, searchBuilder.Limit);
            Assert.Same(searchBuilder, result);
        }

        [Fact]
        public void WithId_ValidValue_ReturnsBuilderWithId()
        {
            // Arrange
            R34PostsSearchBuilder searchBuilder = new();

            // Act
            R34PostsSearchBuilder result = searchBuilder.WithId(42);

            // Assert
            Assert.Equal(42ul, searchBuilder.Id.Value);
            Assert.Same(searchBuilder, result);
        }

        [Fact]
        public void WithOffset_ValidValue_ReturnsBuilderWithOffset()
        {
            // Arrange
            R34PostsSearchBuilder searchBuilder = new();

            // Act
            R34PostsSearchBuilder result = searchBuilder.WithOffset(5);

            // Assert
            Assert.Equal(5, searchBuilder.Offset.Value);
            Assert.Same(searchBuilder, result);
        }

        [Fact]
        public void WithTags_ValidValue_ReturnsBuilderWithTags()
        {
            // Arrange
            R34PostsSearchBuilder searchBuilder = new();
            R34FormattedTag[] tags = new R34FormattedTag[] { new("tag1"), new("tag2") };

            // Act
            R34PostsSearchBuilder result = searchBuilder.WithTags(tags);

            // Assert
            Assert.Equal(tags, searchBuilder.Tags);
            Assert.Same(searchBuilder, result);
        }

        [Fact]
        public void WithBlockedTags_ValidValue_ReturnsBuilderWithBlockedTags()
        {
            // Arrange
            R34PostsSearchBuilder searchBuilder = new();
            R34FormattedTag[] blockedTags = new R34FormattedTag[] { new("blocked1"), new("blocked2") };

            // Act
            R34PostsSearchBuilder result = searchBuilder.WithBlockedTags(blockedTags);

            // Assert
            Assert.Equal(blockedTags, searchBuilder.BlockedTags);
            Assert.Same(searchBuilder, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        public void WithLimit_SetValidValues_Success(int limit)
        {
            // Arrange
            R34PostsSearchBuilder searchBuilder = new();

            // Act
            R34PostsSearchBuilder result = searchBuilder.WithLimit(limit);

            // Assert
            Assert.Equal(limit, searchBuilder.Limit);
            Assert.Same(searchBuilder, result);
        }
    }
}