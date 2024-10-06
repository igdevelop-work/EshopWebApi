using EshopWebApi.backend.interfaces.base_interfaces.entity_layer;
using EshopWebApi.infrasctructure;
using Microsoft.EntityFrameworkCore;
using EshopWebApi.backend.interfaces.product_module;
using EshopWebApi.backend.modules.product_module;
using EshopWebApi.backend.modules.product_module.dto;
using EshopWebApi.backend.modules.product_module.entity_layer;
using EshopWebApi.backend.modules.product_module.errors;
using EshopWebApi.backend.shared.entities;
using Swashbuckle.AspNetCore.Filters;
using StackExchange.Redis;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

var isMockTest = args.Contains("moc-test");

Env.Load();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ExampleFilters();
});

if (!isMockTest)
{
    string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? builder.Configuration["ConnectionStrings:Host"] ?? "localhost";
    string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? builder.Configuration["ConnectionStrings:Database"] ?? "EshopDb";
    string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? builder.Configuration["ConnectionStrings:Username"] ?? "postgres";
    string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? builder.Configuration["ConnectionStrings:Password"] ?? "postgres";

    string connectionString = $"Host={dbHost};Database={dbName};Username={dbUser};Password={dbPassword}";

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

    builder.Services.AddScoped<IReadService<ProductEntity>, ProductReadService>();
    builder.Services.AddScoped<IWriteService<ProductEntity>, ProductWriteService>();
    builder.Services.AddScoped<IProductService, ProductService>();
}
else
{
    string redisHost = Environment.GetEnvironmentVariable("REDIS_HOST") ?? builder.Configuration["Redis:Host"] ?? "localhost";
    string redisPort = Environment.GetEnvironmentVariable("REDIS_PORT") ?? builder.Configuration["Redis:Port"] ?? "6379";

    builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect($"{redisHost}:{redisPort}"));
    builder.Services.AddSingleton<IProductService, RedisMockProductService>();
}

builder.Services.AddSwaggerExamplesFromAssemblyOf<ProductRequestExample>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomValidationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();