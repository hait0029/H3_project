using H3_project.Models;

namespace H3_project.Repositories
{
    public class ProductOrderListRepository : IProductOrderListRepository
    {
        private DatabaseContext Context { get; set; }
        public ProductOrderListRepository(DatabaseContext context)
        {
            Context = context;
        }
        public async Task<ProductOrderList> CreateProductOrderList(ProductOrderList newProductOrderList)
        {
            if (newProductOrderList.OrderId.HasValue)
            {
                newProductOrderList.Orders = await Context.Order.FirstOrDefaultAsync(e => e.OrderID == newProductOrderList.OrderId);
            }
            else if (newProductOrderList.ProductId.HasValue)
            {
                newProductOrderList.Products = await Context.Product.FirstOrDefaultAsync(e => e.ProductID == newProductOrderList.ProductId);
            }

            Context.ProductOrderList.Add(newProductOrderList);
            await Context.SaveChangesAsync();

            return newProductOrderList;
        }

        public async Task<ProductOrderList> DeleteProductOrderList(int productOrderListId)
        {
            ProductOrderList productOrderList = await GetProductOrderListById(productOrderListId);
            if (productOrderList != null)
            {
                Context.ProductOrderList.Remove(productOrderList);
                await Context.SaveChangesAsync();
            }
            return productOrderList;
        }

        public async Task<List<ProductOrderList>> GetProductOrderList()
        {
            return await Context.ProductOrderList.Include(e => e.Products).Include(x => x.Orders).ThenInclude(x => x.user).ThenInclude(x => x.login).ThenInclude(x => x.userType).ToListAsync();
        }

        public async Task<ProductOrderList> GetProductOrderListById(int productOrderListId)
        {
            /* This is how you write a get by id code if you are not having include with it*/
            //return await context.ProductOrderList.FirstOrDefaultAsync(e => e.ProductOrderListID == productOrderListId);


            // Include method is used to include our classes or models and it specifies models based on the chosen model

            // ThenInclude method is used to include more of whatever column is inside of the chosen model, like in this example inside of include method we chose "Order class" as our main model, and used ThenInclude to navigate to our next Foreignkey property which is user.
            return await Context.ProductOrderList.Include(e => e.Products).Include(x => x.Orders).ThenInclude(x => x.user).ThenInclude(x => x.login).ThenInclude(x => x.userType).FirstOrDefaultAsync(e => e.ProductOrderListID == productOrderListId);
        }

        public async Task<ProductOrderList> UpdateProductOrderList(int productOrderListId, ProductOrderList updateProductOrderList)
        {
            ProductOrderList productOrderList = await GetProductOrderListById(productOrderListId);

            if (productOrderList != null)
            {
                // Update the main properties
                productOrderList.Quantity = updateProductOrderList.Quantity;

                // Update the related entities based on valid IDs
                if (updateProductOrderList.Orders != null && updateProductOrderList.Orders.OrderID > 0)
                {
                    productOrderList.Orders = await Context.Order.FirstOrDefaultAsync(e => e.OrderID == updateProductOrderList.Orders.OrderID);
                }
                else
                {
                    productOrderList.Orders = null; // Clear the Order if null or invalid ID is provided
                }

                if (updateProductOrderList.Products != null && updateProductOrderList.Products.ProductID > 0)
                {
                    productOrderList.Products = await Context.Product.FirstOrDefaultAsync(e => e.ProductID == updateProductOrderList.Products.ProductID);
                }
                else
                {
                    productOrderList.Products = null; // Clear the Product if null or invalid ID is provided
                }

                //Context.Entry(productOrderList).State = EntityState.Modified;

                await Context.SaveChangesAsync();
                return await GetProductOrderListById(productOrderListId);
            }
            return null;
        }
    }
}
