namespace EshopWebApi.backend.shared.entities
{
    public interface IProductOwnAttributes
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string ImgUrl { get; set; }
        decimal? Price { get; set; }
        string Description { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }

    public interface IProductCreationAttributes
    {
        string Name { get; set; }
        string ImgUrl { get; set; }
        decimal Price { get; set; }
        string Description { get; set; }
    }

    public interface IProductKeysAttributes
    {
        Guid Id { get; set; }
    }

    public class ProductEntity : IProductOwnAttributes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ProductEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}