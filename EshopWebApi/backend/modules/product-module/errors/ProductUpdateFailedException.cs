namespace EshopWebApi.backend.modules.product_module.errors
{
    public class ProductUpdateFailedException : Exception
    {
        public ProductUpdateFailedException(Guid productId, string message)
            : base($"Failed to update the product with ID {productId}. {message}")
        {
        }
    }
}