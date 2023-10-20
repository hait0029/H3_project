namespace H3_project.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetCategory();
        public Task<Category> GetCategoryById(int categoryId);
        public Task<Category> CreateCategory(Category category);
        public Task<Category> UpdateCategory(int categoryId, Category category);
        public Task<Category> DeleteCategory(int categoryId);
    }
}
