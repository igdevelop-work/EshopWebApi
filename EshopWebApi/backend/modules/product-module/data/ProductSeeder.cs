using EshopWebApi.backend.shared.entities;
using Bogus;
using StackExchange.Redis;
using Newtonsoft.Json;

public static class ProductSeeder
{
    public static async Task SeedAsync(IDatabase db)
    {
        if (await db.StringGetAsync("mockProducts") == RedisValue.Null)
        {
            var products = GenerateFakeProducts(300);

            await db.StringSetAsync("mockProducts", JsonConvert.SerializeObject(products));
        }
    }

    private static List<ProductEntity> GenerateFakeProducts(int count)
    {
        var faker = new Faker<ProductEntity>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000))
            .RuleFor(p => p.ImgUrl, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.CreatedAt, f => DateTime.UtcNow)
            .RuleFor(p => p.UpdatedAt, f => DateTime.UtcNow);

        return faker.Generate(count);
    }
}