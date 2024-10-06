using EshopWebApi.backend.shared.entities;
namespace EshopWebApi.backend.interfaces.product_module
{
    public interface IProductService
    {
        Task<IEnumerable<ProductEntity>> GetAllProducts();
        Task<ProductEntity> GetProductById(Guid id);
        Task CreateProduct(ProductEntity product);
        Task UpdateProduct(ProductEntity product);
        Task<IEnumerable<ProductEntity>> GetPagedProducts(int page, int pageSize);
    }
}