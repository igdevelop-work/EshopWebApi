namespace EshopWebApi.infrasctructure;

using Microsoft.EntityFrameworkCore;
using backend.shared.entities;

public class AppDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


}