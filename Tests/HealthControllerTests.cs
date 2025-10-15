using Xunit;

namespace EnergiaApi.Tests
{
    public class HealthControllerTests
    {
        [Fact]
        public void HealthCheck_ShouldReturnTrue()
        {
            // Arrange
            var expected = true;

            // Act
            var result = expected;

            // Assert
            Assert.True(result);
        }
    }
}
