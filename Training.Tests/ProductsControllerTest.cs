using Training.Database.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Training.API;
using Training.API.Controllers;
using Training.Tests.Utils;
using Xunit;
using Training.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Training.Tests
{
    public class ProductsControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ProductsControllerTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder().UseEnvironment("Development").UseStartup<TestStartup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public void TestGetBookById()
        {
            var mockRepo = new Mock<IProductSessionRepository>();
            mockRepo.Setup(repo => repo.GetProductById(1)).Returns(TestStartup.GetTestSession());
            var controller = new ProductsController(mockRepo.Object);



            string expectedName = "product 1";
            var result = controller.GetProduct(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(okResult.Value);

            Assert.Equal(expectedName, product.ProductName);
        }


        [Fact]
        public async Task GetProductByIdTest()
        {
            int id = 1;
            string result = await _client.GetStringAsync($"/api/products/{id}");

            var product = JsonConvert.DeserializeObject<Product>(result);


            string expectedName = "product 1";
            Assert.Equal(expectedName, product.ProductName);
        }
    }
}
