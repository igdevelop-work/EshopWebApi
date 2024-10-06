using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EshopWebApi.backend.interfaces.base_interfaces.entity_layer;
using EshopWebApi.backend.shared.entities;
using EshopWebApi.infrasctructure;
using Microsoft.EntityFrameworkCore;

namespace EshopWebApi.backend.modules.product_module.entity_layer
{
    public class ProductReadService : IReadService<ProductEntity>
    {
        private readonly AppDbContext _context;

        public ProductReadService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<ProductEntity> FindAll()
        {
            return _context.Products.AsQueryable();
        }
        
        public async Task<IEnumerable<ProductEntity>> FindAllPaged(int page, int pageSize)
        {
            return await _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        
        public async Task<ProductEntity> FindById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}