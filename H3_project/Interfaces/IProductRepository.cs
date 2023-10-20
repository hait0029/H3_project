namespace H3_project.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProduct();
        public Task<Product> GetProductById(int productId);
        public Task<Product> CreateProduct(Product product);
        public Task<Product> UpdateProduct(int productId, Product product);
        public Task<Product> DeleteProduct(int productId);
    }
}
