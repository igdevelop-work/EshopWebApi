using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EshopWebApi.backend.interfaces.product_module;
using EshopWebApi.backend.modules.product_module;
using EshopWebApi.backend.modules.product_module.dto;
using EshopWebApi.backend.shared.entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EshopWebApi.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOk_WithProducts()
        {
            var products = new List<ProductEntity>
            {
                new ProductEntity { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, ImgUrl = "https://example.com/img1.jpg", Description = "First product" },
                new ProductEntity { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, ImgUrl = "https://example.com/img2.jpg", Description = "Second product" }
            };
            _productServiceMock.Setup(s => s.GetAllProducts()).ReturnsAsync(products);

            var result = await _productController.GetAllProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProducts = Assert.IsType<List<ProductEntity>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOk_WhenNoProducts()
        {
            _productServiceMock.Setup(s => s.GetAllProducts()).ReturnsAsync(new List<ProductEntity>());

            var result = await _productController.GetAllProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProducts = Assert.IsType<List<ProductEntity>>(okResult.Value);
            Assert.Empty(returnProducts); 
        }

        [Fact]
        public async Task GetAllProductsPaged_ReturnsOk_WithPagedProducts()
        {
            var products = new List<ProductEntity>
            {
                new ProductEntity { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, ImgUrl = "https://example.com/img1.jpg", Description = "First product" },
                new ProductEntity { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, ImgUrl = "https://example.com/img2.jpg", Description = "Second product" }
            };
            _productServiceMock.Setup(s => s.GetPagedProducts(1, 10)).ReturnsAsync(products);

            var result = await _productController.GetAllProductsPaged(1, 10);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProducts = Assert.IsType<List<ProductEntity>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);
        }

        [Fact]
        public async Task GetProductById_ReturnsOk_WithProduct()
        {
            var productId = Guid.NewGuid();
            var product = new ProductEntity
            {
                Id = productId,
                Name = "Product 1",
                Price = 100,
                ImgUrl = "https://example.com/img1.jpg",
                Description = "First product"
            };
            _productServiceMock.Setup(s => s.GetProductById(productId)).ReturnsAsync(product);

            var result = await _productController.GetProductById(productId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProduct = Assert.IsType<ProductEntity>(okResult.Value);
            Assert.Equal(productId, returnProduct.Id);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreated_WithProduct()
        {
            var createProductDto = new CreateProductDto
            {
                Name = "New Product",
                Price = 150,
                ImgUrl = "https://example.com/img3.jpg",
                Description = "New product description"
            };
    
            var product = new ProductEntity
            {
                Id = Guid.NewGuid(),
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                ImgUrl = createProductDto.ImgUrl,
                Description = createProductDto.Description
            };
    
            _productServiceMock.Setup(s => s.CreateProduct(It.IsAny<ProductEntity>())).Returns(Task.CompletedTask);

            var result = await _productController.CreateProduct(createProductDto);
    
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdProduct = Assert.IsType<ProductEntity>(createdResult.Value);

            Assert.NotNull(createdProduct);
            Assert.Equal(createProductDto.Name, createdProduct.Name);
            Assert.Equal(createProductDto.Price, createdProduct.Price);
            Assert.Equal(createProductDto.ImgUrl, createdProduct.ImgUrl);
            Assert.Equal(createProductDto.Description, createdProduct.Description);
        }

        [Fact]
        public async Task PatchProduct_ReturnsOk_WithUpdatedProduct()
        {
            var productId = Guid.NewGuid();
            var updateProductDto = new UpdateProductDto
            {
                Name = "Updated Product",
                Price = 200,
                ImgUrl = "https://example.com/img2.jpg",
                Description = "Updated description"
            };
            var product = new ProductEntity
            {
                Id = productId,
                Name = updateProductDto.Name,
                Price = updateProductDto.Price,
                ImgUrl = updateProductDto.ImgUrl,
                Description = updateProductDto.Description
            };
            _productServiceMock.Setup(s => s.UpdateProduct(It.IsAny<ProductEntity>())).Returns(Task.CompletedTask);

            var result = await _productController.PatchProduct(productId, updateProductDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseValue = okResult.Value.ToString();
            Assert.Contains("successfully updated", responseValue);
            Assert.NotNull(responseValue);
        }
    }
}