using R34Sharp.Search;

namespace R34Sharp.Tests.Search
{
    public sealed class R34CommentsSearchBuilderTests
    {
        [Fact]
        public void Constructor_DefaultValues_Success()
        {
            // Arrange
            R34CommentsSearchBuilder searchBuilder = new();

            // Act & Assert
            Assert.Equal(0ul, searchBuilder.PostId);
        }

        [Fact]
        public void WithId_ValidValue_ReturnsBuilderWithId()
        {
            // Arrange
            R34CommentsSearchBuilder searchBuilder = new();

            // Act
            R34CommentsSearchBuilder result = searchBuilder.WithId(42);

            // Assert
            Assert.Equal(42ul, searchBuilder.PostId);
            Assert.Same(searchBuilder, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(999)]
        public void WithId_SetValidValues_Success(ulong id)
        {
            // Arrange
            R34CommentsSearchBuilder searchBuilder = new();

            // Act
            R34CommentsSearchBuilder result = searchBuilder.WithId(id);

            // Assert
            Assert.Equal(id, searchBuilder.PostId);
            Assert.Same(searchBuilder, result);
        }
    }
}
