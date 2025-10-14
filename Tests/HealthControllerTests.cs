using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnergiaApi.Controllers;
using EnergiaApi.Data;
using Xunit;

namespace EnergiaApi.Tests
{
    public class HealthControllerTests
    {
        [Fact]
        public void HealthCheck_ShouldReturnOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EnergiaDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EnergiaDbContext(options);
            var controller = new HealthController(context);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsType<Task<IActionResult>>(result);
        }
    }
}
