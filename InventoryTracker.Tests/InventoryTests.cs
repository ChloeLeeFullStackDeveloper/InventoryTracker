using Xunit;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    public class InventoryTests
    {
        [Fact]
        public void Product_Should_Have_Correct_Name()
        {
            // Arrange
            var product = new Product { Name = "Laptop", Quantity = 5, Price = 1299.99 };

            // Act
            string actualName = product.Name;

            // Assert
            Assert.Equal("Laptop", actualName);
        }
    }
}
