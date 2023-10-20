namespace ProjectUnitTests.Controllers
{
    public class CategoryControllerTests
    {
        private readonly CategoryController categoryController;
        private Mock<ICategoryRepository> CategoryRepositoryMock = new();

        public CategoryControllerTests()
        {
            //creating an instance of the CategoryController class and initializing it with a mock object of the category repository
            categoryController = new CategoryController(CategoryRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenCategoriesExists()
        {
            // arrange
            List<Category> categories = new();

            categories.Add(new Category
            {
                CategoryID = 1,
                CategoryName = "Gaming"
            });

            CategoryRepositoryMock.Setup(x => x.GetCategory()).ReturnsAsync(categories);

            //Act

            var result = (IStatusCodeActionResult)await categoryController.GetAllCategoryType();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_NoCategoriesExists()
        {
            // arrange
            List<Category> categories = new();

            CategoryRepositoryMock.Setup(x => x.GetCategory()).ReturnsAsync(categories);

            //Act

            var result = (IStatusCodeActionResult)await categoryController.GetAllCategoryType();

            //Assert
           
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenCategoryRepositoryReturnsNull()
        {
            // arrange

            CategoryRepositoryMock.Setup(x => x.GetCategory()).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await categoryController.GetAllCategoryType();

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenCategoryExceptionIsRaised()
        {
            // Arrange
            CategoryRepositoryMock.Setup(x => x.GetCategory()).ReturnsAsync(() => throw new Exception("This is an Exception"));

            // Act
            var result = (IStatusCodeActionResult)await categoryController.GetAllCategoryType();

            // Assert
            Assert.Equal(500, result.StatusCode);



        }

        [Fact]
        public async void GetByID_ShouldReturnStatusCode200_WhenCategoryDataExists()
        {
            // arrange
            int categoryId = 1;

            Category category = new()
            {
                CategoryID = categoryId,
                CategoryName = "Gaming",
                Id = 1
            };

            CategoryRepositoryMock.Setup(x => x.GetCategoryById(It.IsAny<int>())).ReturnsAsync(category);

            //Act

            var result = (IStatusCodeActionResult)await categoryController.GetCategoryById(categoryId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenCategoriesDoesNotExists()
        {
            // arrange
            int categoryId = 1;

            CategoryRepositoryMock.Setup(x => x.GetCategoryById(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await categoryController.GetCategoryById(categoryId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenCategoryExceptionIsRaised()
        {
            // arrange

            CategoryRepositoryMock.Setup(x => x.GetCategoryById(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await categoryController.GetCategoryById(1);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode201_WhenCategoryDataExists()
        {
            // arrange

            Category newCategory = new()
            {
                CategoryName = "Gaming"
            };

            int categoryId = 1;

            Category category = new()
            {
                CategoryID = categoryId,
                CategoryName = "Gaming"
            };


            CategoryRepositoryMock.Setup(x => x.CreateCategory(It.IsAny<Category>())).ReturnsAsync(category);

            //Act

            var result = await categoryController.PostCategories(newCategory);

            //Assert
            var statusCodeResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenCategoryExceptionIsRaised()
        {
            // arrange

            Category newCategory = new()
            {
                CategoryName = "Test"
            };


            CategoryRepositoryMock.Setup(x => x.CreateCategory(It.IsAny<Category>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = await categoryController.PostCategories(newCategory);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenCategoryIsUpdatedSuccesfully()
        {
            //arrange

            Category updateCategory = new()
            {
                CategoryName = "Test"
            };

            int categoryId = 1;

            Category CategoryResponse = new()
            {
                CategoryID = categoryId,
                CategoryName = "Test"
            };


            CategoryRepositoryMock.Setup(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<Category>())).ReturnsAsync(CategoryResponse);

            //Act

            var result = (IStatusCodeActionResult)await categoryController.PutCategory(updateCategory, categoryId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        
        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenCategoryExceptionIsRaised()
        {
            // arrange

            Category updateCategory = new()
            {
                CategoryName = "Test"
            };

            int categoryId = 1;

            CategoryRepositoryMock.Setup(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<Category>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await categoryController.PutCategory(updateCategory, categoryId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenCategoryIsDeleted()
        {
            // arrange
            int categoryId = 1;

            Category CategoryResponse = new()
            {
                CategoryID = categoryId,
                CategoryName = "Test"
            };

            CategoryRepositoryMock.Setup(x => x.DeleteCategory(It.IsAny<int>())).ReturnsAsync(CategoryResponse);

            //Act

            var result = (IStatusCodeActionResult)await categoryController.DeleteCategory(categoryId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenCategoryDoesNotExists()
        {
            // arrange
            int categoryId = 1;

            CategoryRepositoryMock.Setup(x => x.DeleteCategory(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await categoryController.DeleteCategory(categoryId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenCategoryExceptionIsRaised()
        {
            // arrange
            int categoryId = 1;

            CategoryRepositoryMock.Setup(x => x.DeleteCategory(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await categoryController.DeleteCategory(categoryId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
