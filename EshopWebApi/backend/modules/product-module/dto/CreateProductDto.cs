using System.ComponentModel.DataAnnotations;

namespace EshopWebApi.backend.modules.product_module.dto
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "The product name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The product name must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Url(ErrorMessage = "The image URL must be a valid URL.")]
        public string ImgUrl { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The description is required.")]
        [StringLength(500, ErrorMessage = "The description must not exceed 500 characters.")]
        public string Description { get; set; }
    }
    
    public class ProductRequestExample : Swashbuckle.AspNetCore.Filters.IExamplesProvider<CreateProductDto>
    {
        public CreateProductDto GetExamples()
        {
            return new CreateProductDto
            {
                Name = "Valid Product Name",
                ImgUrl = "https://example.com/valid-product-image.jpg",
                Price = 19.99M,
                Description = "This is a valid product description with no more than 500 characters."
            };
        }
    }
}