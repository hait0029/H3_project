namespace H3_project.Interfaces
{
    public interface IProductOrderListRepository
    {
        public Task<List<ProductOrderList>> GetProductOrderList();
        public Task<ProductOrderList> GetProductOrderListById(int productOrderListId);
        public Task<ProductOrderList> CreateProductOrderList(ProductOrderList productOrderList);
        public Task<ProductOrderList> UpdateProductOrderList(int productOrderListId, ProductOrderList ProductOrderList);
        public Task<ProductOrderList> DeleteProductOrderList(int productOrderListId);
    }
}
