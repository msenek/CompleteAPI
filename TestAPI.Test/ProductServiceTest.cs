using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System.Xml.Linq;
using TestAPI.DB.DTOs;
using TestAPI.DB.Entities;
using TestAPI.Repository.Interface;
using TestAPI.Services;
using TestAPI.Services.Interface;
namespace TestAPI.Test
{
    public class ProductServiceTest
    {
        // getbyid
        [Fact]
        public async Task GeyById_DebeRetornarProducto_CuandoExiste()
        {
            var mockRepository = new Mock<IProductRepository>();
            var mockCache = new Mock<ICacheService>();
            var service = new ProductService(mockRepository.Object, mockCache.Object);
            var testProduct = new Product()
            {
                Id = 1,
                Name = "Test",
                Price = 200,
                Description = "It Works yupie"
            };
            mockRepository.Setup(r => r.GetById(1)).ReturnsAsync(testProduct);
            mockCache.Setup(c => c.GetAsync<ResponseDto>("product:1")).ReturnsAsync((ResponseDto?)null);

            var result = await service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Name);
        }
        [Fact]
        public async Task Get_DebeRetornarTodosLosProductos()
        {
            var mockCache = new Mock<ICacheService>();
            var mockRepository = new Mock<IProductRepository>();
            var service = new ProductService(mockRepository.Object, mockCache.Object);

            var product1 = new Product()
            {
                Id = 1,
                Name = "product1",
                Description = "one",
                Price = 100
            };
            var product2 = new Product()
            {
                Id = 2,
                Name = "product2",
                Description = "two",
                Price = 200
            };
            var product3 = new Product()
            {
                Id = 3,
                Name = "product3",
                Description = "tree",
                Price = 300
            };
            var productList = new List<Product> { product1, product2, product3 };
            var filtering = new FilteringDto();


            mockRepository.Setup(r => r.GetAllAsync(1, 10, filtering)).ReturnsAsync(productList);
            mockCache.Setup(c => c.GetAsync<List<ResponseDto>>("Product:Page=1:PageSize=10:Name=:MinPrice=:MaxPrice=")).ReturnsAsync((List<ResponseDto>?)null);

            var result = await service.GetAllAsync(1, 10, filtering);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("product1", result[0].Name);
            Assert.Equal(100, result[0].Price);
            Assert.Equal("one", result[0].Description);
        }
        [Fact]
        public async Task CreateAsync_DeveDevolverRequestDtoCorrecto()
        {
            var MockRepository = new Mock<IProductRepository>();
            var MockCache = new Mock<ICacheService>();
            var service = new ProductService(MockRepository.Object, MockCache.Object);

            var testRequest = new RequestDto()
            {
                Name = "test",
                Description = "test description",
                Price = 999
            };

            var testProduct = new Product()
            {
                Name = "test",
                Description = "test description",
                Price = 999
            };

            MockRepository.Setup(r => r.CreateAsync(It.IsAny<Product>())).ReturnsAsync(testProduct);

            var result = await service.CreateAsync(testRequest);

            Assert.NotNull(result);
            Assert.Equal(testRequest.Name, result.Name);
            Assert.Equal(testRequest.Description, result.Description);
            Assert.Equal(testRequest.Price, result.Price);
        }

        [Fact]

        public async Task UpdateAsync_devuelveProductYIdCorrectamente()
        {
            var MockRepository = new Mock<IProductRepository>();
            var MockCacheService = new Mock<ICacheService>();
            var service = new ProductService(MockRepository.Object, MockCacheService.Object);

            var product = new Product()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 999
            };
            var productRequest = new RequestDto()
            {
                Name = "Test",
                Description = "Test",
                Price = 999
            };

            MockRepository.Setup(r => r.GetById(1)).ReturnsAsync(product);
            MockRepository.Setup(r => r.UpdateAsync(1, product)).ReturnsAsync(product);
            MockCacheService.Setup(c => c.RemoveAsync("Product:1"));

            var result = await service.UpdateAsync(1, productRequest);


            Assert.NotNull(result);
            Assert.Equal(productRequest.Name, result.Name);
            Assert.Equal(productRequest.Description, result.Description);
            Assert.Equal(productRequest.Price, result.Price);
        }


        [Fact]

        public async Task DeleteAsync_DevuelveBool()
        {
            var mockRepository = new Mock<IProductRepository>();
            var mockCacheService = new Mock<ICacheService>();
            var service = new ProductService(mockRepository.Object, mockCacheService.Object);

            mockRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
            mockCacheService.Setup(c => c.RemoveAsync("Product:1"));

            var result = service.DeleteAsync(1);

            Assert.True(true);
        }
    }
}
