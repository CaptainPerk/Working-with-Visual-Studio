using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WorkingWithVisualStudio.Controllers;
using WorkingWithVisualStudio.Models;
using Xunit;

namespace WorkingWithVisualStudio.Tests
{
    public class HomeControllerTests
    {
        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; set; }

            public void AddProduct(Product product)
            {
                //do nothing - not required for test
            }
        }

        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsComplete(Product[] products)
        {
            // Arrange
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepository
            {
                Products = products
            };

            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.Repository.Products, model, 
                Comparer.Get<Product>((product1, product2) => product1.Name == product2.Name && product1.Price == product2.Price));
        }

        class PropertyOnceFakeRepository : IRepository
        {
            public int PropertyCounter { get; set; } = 0;

            public IEnumerable<Product> Products
            {
                get
                {
                    PropertyCounter++;
                    return new[] {new Product {Name = "P1", Price = 100}};
                }
            }

            public void AddProduct(Product product)
            {
                // do nothing - not required for the test
            }
        }

        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            //Arrange
            var repo = new PropertyOnceFakeRepository();
            var controller = new HomeController {Repository = repo};

            //Act
            var result = controller.Index();

            //Assert
            Assert.Equal(1, repo.PropertyCounter);
        }
    }
}
