using System.ComponentModel.DataAnnotations;

namespace EshopWebApi.backend.modules.product_module.dto
{
    public class UpdateProductDto
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The product name must be between 3 and 100 characters.")]
        public string? Name { get; set; }

        [Url(ErrorMessage = "The image URL must be a valid URL.")]
        public string? ImgUrl { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than zero.")]
        public decimal? Price { get; set; }

        [StringLength(500, ErrorMessage = "The description must not exceed 500 characters.")]
        public string? Description { get; set; }
    }

    public class ProductUpdateRequestExample : Swashbuckle.AspNetCore.Filters.IExamplesProvider<UpdateProductDto>
    {
        public UpdateProductDto GetExamples()
        {
            return new UpdateProductDto
            {
                Name = "Updated Product Name",
                ImgUrl = "https://example.com/updated-product-image.jpg",
                Price = 25.99M,
                Description = "This is an updated product description with no more than 500 characters."
            };
        }
    }
}