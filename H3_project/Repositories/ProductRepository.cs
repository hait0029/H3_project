using H3_project.Models;

namespace H3_project.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DatabaseContext Context { get; set; }
        public ProductRepository(DatabaseContext context)
        {
            Context = context;
        }
        public async Task<Product> CreateProduct(Product newProduct)
        {

            if (newProduct.CategoryId.HasValue)
            {
                newProduct.category = await Context.Category.FirstOrDefaultAsync(e => e.CategoryID == newProduct.CategoryId);
            }

            Context.Product.Add(newProduct);
            await Context.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            Product product = await GetProductById(productId);
            if (product != null)
            {
                Context.Product.Remove(product);
                await Context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<List<Product>> GetProduct()
        {
            //return await Context.Product.ToListAsync();
            return await Context.Product.Include(e => e.category).ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            //return await Context.Product.FirstOrDefaultAsync(e => e.ProductID == productId);
            return await Context.Product.Include(e => e.category).FirstOrDefaultAsync(e => e.ProductID == productId);
        }

        public async Task<Product> UpdateProduct(int productId, Product updateProduct)
        {
            Product product = await GetProductById(productId);

            if (product != null)
            {
                product.ProductID = updateProduct.ProductID;
                product.ProductName = updateProduct.ProductName;
                product.Price = updateProduct.Price;
                product.CategoryId = updateProduct.CategoryId;
                if (updateProduct.category != null)
                {
                    product.category = await Context.Category.FirstOrDefaultAsync(e => e.CategoryID == updateProduct.category.CategoryID);
                }
                else
                {
                    product.category = null; // Clear the User if null is provided
                }

                Context.Entry(product).State = EntityState.Modified;


                await Context.SaveChangesAsync();
                return await GetProductById(productId);
            }
            return null;

        }
    }
}
