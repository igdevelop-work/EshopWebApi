using EshopWebApi.backend.interfaces.base_interfaces.entity_layer;
using EshopWebApi.backend.modules.product_module.errors;
using EshopWebApi.backend.shared.cqrs;
using EshopWebApi.backend.shared.entities;
using EshopWebApi.infrasctructure;
using Microsoft.EntityFrameworkCore;

public class ProductWriteService : BaseWriteService<ProductEntity>, IWriteService<ProductEntity>
{
    private readonly AppDbContext _context;

    public ProductWriteService(AppDbContext context) : base(context.Products)
    {
        _context = context;
    }

    public async Task Create(ProductEntity entity)
    {
        await _create(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ProductEntity entity)
    {
        var existingProduct = await _context.Products.FindAsync(entity.Id);
        if (existingProduct == null)
        {
            throw new ProductNotFoundException(entity.Id);
        }

        if (!string.IsNullOrEmpty(entity.Name))
            existingProduct.Name = entity.Name;
    
        if (!string.IsNullOrEmpty(entity.ImgUrl))
            existingProduct.ImgUrl = entity.ImgUrl;
    
        if (entity.Price > 0)
            existingProduct.Price = entity.Price;

        if (!string.IsNullOrEmpty(entity.Description))
            existingProduct.Description = entity.Description;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            throw new ProductNotFoundException(id);
        }

        await _delete(product);
        await _context.SaveChangesAsync();
    }
}