namespace ProjectUnitTests.Repositories
{

    public class CategoryRepositoryTests
    {
        private DbContextOptions<DatabaseContext> options; //server
        private DatabaseContext context; //udsignet på serveren
        private CategoryRepository categoryRepository;


        public CategoryRepositoryTests()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "CategoryRepository")
            .Options;

            context = new DatabaseContext(options);

            categoryRepository = new(context);
            //context.Database.EnsureDeletedAsync();

            ////data
            //context.Category.Add(new Category { CategoryID = 1, CategoryName = "Gaming", Id = 1 });
            //context.Category.Add(new Category { CategoryID = 2, CategoryName = "Accessories", Id = 2 });
            //context.Category.Add(new Category { CategoryID = 3, CategoryName = "Merch", Id = 3 });
            //context.SaveChanges();


        }
        [Fact]
        public async Task GetAll_ShouldReturnListOfCategories_WhenCategoriesExists()
        {
            //Arrange
            //create data
            await context.Database.EnsureDeletedAsync();

            context.Category.Add(new Category 
            { 
                CategoryID = 1, 
                CategoryName = "Gaming", 
                Id = 1 
            });
            context.Category.Add(new Category 
            { 
                CategoryID = 2, 
                CategoryName = "Accessories", 
                Id = 2
            });
            context.Category.Add(new Category 
            { 
                CategoryID = 3, 
                CategoryName = "Merch", 
                Id = 3
            });
            context.SaveChanges();

            //Act
            //call method
            var result = await categoryRepository.GetCategory();

            //Assert
            //confirm result
            //(expected, result)
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfCategories_WhenNoCategoriesExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();
            //Act
            var result = await categoryRepository.GetCategory();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Empty(result);
        }


        [Fact]
        public async void GetById_ShouldReturnCategories_WhenCategoriesExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int categoryId = 1;

            context.Category.Add(new()
            {
                CategoryID = categoryId,
                CategoryName = "Gaming"
            });

            await context.SaveChangesAsync();


            //Act
            var result = await categoryRepository.GetCategoryById(categoryId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.CategoryID);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenCategoriesDeosNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await categoryRepository.GetCategoryById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldAddNewIdToCategory_WhenSavingToDatabase()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Category category = new()
            {
                CategoryID = expectedNewId,
                CategoryName = "Gaming"
            };

            //Act
            var result = await categoryRepository.CreateCategory(category);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(expectedNewId, result.CategoryID);
        }

        [Fact]
        public async void Create_ShouldFailToAddNewIdToCategory_WhenCategoryIdAlreadyExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            Category category = new()
            {
                CategoryID = 1,
                CategoryName = "Gaming"
            };

            await categoryRepository.CreateCategory(category);

            //Act
            async Task action() => await categoryRepository.CreateCategory(category);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnCategory_WhenCategoryExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int categoryId = 1;

            Category newCategory = new()
            {
                CategoryID = categoryId,
                CategoryName = "Gaming",
                
            };
            context.Category.Add(newCategory);
            await context.SaveChangesAsync();
            Category updateCategory = new()
            {
                CategoryID = categoryId,
                CategoryName = "New Gaming",
                
            };
            var result = await categoryRepository.UpdateCategory(categoryId, updateCategory);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.CategoryID);
            Assert.Equal(updateCategory.CategoryName, result.CategoryName);
            
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenCategoryDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int categoryId = 1;

            Category updateCategory = new()
            {
                CategoryID = categoryId,
                CategoryName = "Gaming",
                Id = 1
            };

            //Act
            var result = await categoryRepository.UpdateCategory(categoryId, updateCategory);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedCategory_WhenCategoryIsDeleted()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int categoryId = 1;

            Category newCategory = new()
            {
                CategoryID = categoryId,
                CategoryName = "Gaming"
            };

            context.Category.Add(newCategory);
            await context.SaveChangesAsync();

            //Act
            var result = await categoryRepository.DeleteCategory(categoryId);
            var category = await categoryRepository.GetCategoryById(categoryId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.CategoryID);

            Assert.Null(category);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenCategoryDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await categoryRepository.DeleteCategory(1);

            //Assert
            Assert.Null(result);
        }

    }
}

//private DbContextOptions<DatabaseContext> options;
//private DatabaseContext context;

//public GenreRepositoriesTest()
//{
//    options = new DbContextOptionsBuilder<DatabaseContext>()
//      .UseInMemoryDatabase(databaseName: "cinema2023")
//      .Options;

//    context = new DatabaseContext(options);
//    context.Database.EnsureDeleted();

//    context.Genre.Add(new Genre { Id = 1, name = "action" });
//    context.Genre.Add(new Genre { Id = 2, name = "comedy" });
//    context.Genre.Add(new Genre { Id = 3, name = "funny" });
//    context.SaveChanges();

//category A=NULL
//category c= new category();
