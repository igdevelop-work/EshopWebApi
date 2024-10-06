namespace EshopWebApi.backend.modules.product_module.errors
{
    public class ProductCreateFailedException : Exception
    {
        public ProductCreateFailedException(string message)
            : base(message)
        {
        }
    }
}