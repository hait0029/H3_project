using H3_project.Models;

namespace H3_project.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        
       private DatabaseContext Context { get; set; }
        public CategoryRepository(DatabaseContext context)
        {
            Context = context;
        }

        public async Task<Category> CreateCategory(Category newCategory)
        {
            Context.Category.Add(newCategory);
            await Context.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category> DeleteCategory(int categoryId)
        {
            Category category = await GetCategoryById(categoryId);
            if (category != null)
            {
                Context.Category.Remove(category);
                await Context.SaveChangesAsync();
            }
            return category;
        }

        public async Task<List<Category>> GetCategory()
        {
            return await Context.Category.Include(e => e.product).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            //return await Context.Category.FirstOrDefaultAsync(e => e.CategoryID == categoryId);
            return await Context.Category.Include(e => e.product).FirstOrDefaultAsync(e => e.CategoryID == categoryId);
        }

        public async Task<Category> UpdateCategory(int categoryId, Category updateCategory)
        {
            Category category = await GetCategoryById(categoryId);
            if (category != null && updateCategory != null)
            {
                category.CategoryName = updateCategory.CategoryName;
                category.Id = updateCategory.Id;

                await Context.SaveChangesAsync();
            }
            return category;
        }
    }
}
