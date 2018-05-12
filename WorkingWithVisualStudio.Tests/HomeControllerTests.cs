using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using WorkingWithVisualStudio.Controllers;
using WorkingWithVisualStudio.Models;
using Xunit;

namespace WorkingWithVisualStudio.Tests
{
    public class HomeControllerTests
    {
        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsComplete(Product[] products)
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.SetupGet(repo => repo.Products).Returns(products);
            var controller = new HomeController { Repository = mockRepository.Object };

            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.Repository.Products, model, 
                Comparer.Get<Product>((product1, product2) => product1.Name == product2.Name && product1.Price == product2.Price));
        }

        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            //Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.SetupGet(repo => repo.Products).Returns(new[] {new Product {Name = "P1", Price = 100}});
            var controller = new HomeController { Repository = mockRepository.Object };

            //Act
            var result = controller.Index();

            //Assert
            mockRepository.VerifyGet(repo => repo.Products, Times.Once);
        }
    }
}
