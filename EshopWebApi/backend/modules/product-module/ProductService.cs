using EshopWebApi.backend.interfaces.product_module;
using EshopWebApi.backend.shared.entities;
using EshopWebApi.backend.interfaces.base_interfaces.entity_layer;
using EshopWebApi.backend.modules.product_module.errors;
using Microsoft.EntityFrameworkCore;

namespace EshopWebApi.backend.modules.product_module
{
    public class ProductService : IProductService
    {
        private readonly IReadService<ProductEntity> _productReadService;
        private readonly IWriteService<ProductEntity> _productWriteService;

        public ProductService(IReadService<ProductEntity> productReadService, IWriteService<ProductEntity> productWriteService)
        {
            _productReadService = productReadService;
            _productWriteService = productWriteService;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProducts()
        {
            return await _productReadService.FindAll().ToListAsync();
        }

        public async Task<ProductEntity> GetProductById(Guid id)
        {
            var product = await _productReadService.FindById(id);
            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }
            return product;
        }

        public async Task CreateProduct(ProductEntity product)
        {
            var existingProduct = await _productReadService.FindAll()
                .FirstOrDefaultAsync(p => p.Name == product.Name);

            if (existingProduct != null)
            {
                throw new ProductCreateFailedException($"A product with the name '{product.Name}' already exists.");
            }

            await _productWriteService.Create(product);
        }

        public async Task UpdateProduct(ProductEntity product)
        {
            var existingProduct = await _productReadService.FindById(product.Id);
            if (existingProduct == null)
            {
                throw new ProductNotFoundException(product.Id);
            }

            if (existingProduct.Name != product.Name)
            {
                var productWithSameName = await _productReadService.FindAll()
                    .FirstOrDefaultAsync(p => p.Name == product.Name);

                if (productWithSameName != null)
                {
                    throw new ProductUpdateFailedException(product.Id, $"A product with the name '{product.Name}' already exists.");
                }
            }

            await _productWriteService.Update(product);
        }



        public async Task<IEnumerable<ProductEntity>> GetPagedProducts(int page, int pageSize)
        {
            return await _productReadService.FindAll()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}