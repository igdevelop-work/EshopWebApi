namespace EshopWebApi.backend.modules.product_module.errors
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid productId)
            : base($"Product with ID {productId} not found.")
        {
        }
    }
}