using EshopWebApi.backend.interfaces.product_module;
using EshopWebApi.backend.shared.entities;
using EshopWebApi.backend.modules.product_module.dto;
using EshopWebApi.backend.modules.product_module.errors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace EshopWebApi.backend.modules.product_module
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all products", Description = "Retrieves a list of all products available in the store.")]
        [SwaggerResponse(200, "Returns a list of products", typeof(List<ProductEntity>))]
        [SwaggerResponse(404, "No products found")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("v2")]
        [SwaggerOperation(Summary = "Get products with pagination (v2)", Description = "Retrieves a paginated list of products, default page size is 10.")]
        [SwaggerResponse(200, "Returns a paginated list of products", typeof(List<ProductEntity>))]
        [SwaggerResponse(404, "No products found")]
        public async Task<IActionResult> GetAllProductsPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetPagedProducts(page, pageSize);
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(Summary = "Get product by ID", Description = "Retrieves a product by its unique identifier.")]
        [SwaggerResponse(200, "Returns the requested product", typeof(ProductEntity))]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return Ok(product);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new product", Description = "Creates a new product with the provided details.")]
        [SwaggerRequestExample(typeof(CreateProductDto), typeof(ProductRequestExample))]
        [SwaggerResponse(201, "Product created successfully", typeof(ProductEntity))]
        [SwaggerResponse(400, "Invalid input data")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            var product = new ProductEntity
            {
                Name = createProductDto.Name,
                ImgUrl = createProductDto.ImgUrl,
                Price = createProductDto.Price,
                Description = createProductDto.Description,
            };

            try
            {
                await _productService.CreateProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (ProductCreateFailedException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        [HttpPatch("{id:guid}")]
        [SwaggerOperation(Summary = "Partially update an existing product", Description = "Updates the details of an existing product.")]
        [SwaggerRequestExample(typeof(UpdateProductDto), typeof(ProductUpdateRequestExample))]
        [SwaggerResponse(200, "Product successfully updated")]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> PatchProduct(Guid id, [FromBody] UpdateProductDto updateProductDto)
        {
            var hasUpdates = !string.IsNullOrEmpty(updateProductDto.Name) ||
                             !string.IsNullOrEmpty(updateProductDto.ImgUrl) ||
                             updateProductDto.Price > 0 ||
                             !string.IsNullOrEmpty(updateProductDto.Description);

            if (!hasUpdates)
            {
                return Ok(new { message = "Nothing to update. No data provided." });
            }

            var product = new ProductEntity
            {
                Id = id,
                Name = updateProductDto.Name,
                ImgUrl = updateProductDto.ImgUrl,
                Price = updateProductDto.Price,
                Description = updateProductDto.Description,
            };

            await _productService.UpdateProduct(product);

            return Ok(new { message = "Product successfully updated" });
        }

    }
}