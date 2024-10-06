using StackExchange.Redis;
using EshopWebApi.backend.shared.entities;
using Newtonsoft.Json;
using EshopWebApi.backend.interfaces.product_module;
using EshopWebApi.backend.modules.product_module.errors;

namespace EshopWebApi.backend.modules.product_module
{
    public class RedisMockProductService : IProductService
    {
        private readonly IDatabase _db;

        public RedisMockProductService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProducts()
        {
            var data = await _db.StringGetAsync("mockProducts");
            if (data.IsNullOrEmpty)
            {
                await ProductSeeder.SeedAsync(_db);
                data = await _db.StringGetAsync("mockProducts");
            }

            return JsonConvert.DeserializeObject<IEnumerable<ProductEntity>>(data);
        }

        public async Task<ProductEntity> GetProductById(Guid id)
        {
            var products = await GetAllProducts();
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }

            return product;
        }

        public async Task CreateProduct(ProductEntity product)
        {
            var products = await GetAllProducts();

            if (products.Any(p => p.Name == product.Name))
            {
                throw new ProductCreateFailedException($"A product with the name '{product.Name}' already exists.");
            }

            var productList = products.ToList();
            productList.Add(product);
            await _db.StringSetAsync("mockProducts", JsonConvert.SerializeObject(productList));
        }

        public async Task UpdateProduct(ProductEntity product)
        {
            var products = await GetAllProducts();
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                throw new ProductNotFoundException(product.Id);
            }

            if (existingProduct.Name != product.Name && products.Any(p => p.Name == product.Name))
            {
                throw new ProductUpdateFailedException(product.Id, $"A product with the name '{product.Name}' already exists.");
            }

            var productList = products.ToList();
            productList.Remove(existingProduct);
            productList.Add(product);
            await _db.StringSetAsync("mockProducts", JsonConvert.SerializeObject(productList));
        }
        

        public async Task<IEnumerable<ProductEntity>> GetPagedProducts(int page, int pageSize)
        {
            var products = await GetAllProducts();
            return products.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}