using WorkingWithVisualStudio.Models;
using Xunit;

namespace WorkingWithVisualStudio.Tests
{
    public class ProductTests
    {
        [Fact]
        public void CanChangeProductName()
        {
            // Arrange
            var product = new Product {Name = "Test", Price = 100M};

            // Act
            product.Name = "New Name";

            // Assert
            Assert.Equal("New Name", product.Name);
        }

        [Fact]
        public void CanChangeProductPrice()
        {
            // Arrange
            var product = new Product { Name = "Test", Price = 100M };

            // Act
            product.Price = 200M;

            // Assert
            Assert.Equal(200M, product.Price);
        }
    }
}
