using H3_project.Interfaces;
using H3_project.Repositories;

namespace ProjectUnitTests.Repositories
{
    public class ProductOrderListRepositoryTests
    {
        private DbContextOptions<DatabaseContext> options; //server
        private DatabaseContext context; //udsignet på serveren
        private ProductOrderListRepository productOrderListRepository;

        public ProductOrderListRepositoryTests()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "ProductOrderListRepository")
            .Options;

            context = new DatabaseContext(options);

            productOrderListRepository = new(context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfProductOrderLists_WhenProductOrderListsExists()
        {
            //Arrange
            //create data
            await context.Database.EnsureDeletedAsync();

            context.ProductOrderList.Add(new ProductOrderList
            {
                ProductOrderListID = 1,
                Quantity = 25

            });
            context.ProductOrderList.Add(new ProductOrderList
            {
                ProductOrderListID = 2,
                Quantity = 234
            });
            context.ProductOrderList.Add(new ProductOrderList
            {
                ProductOrderListID = 3,
                Quantity = 64
            });
            context.SaveChanges();

            //Act
            //call method
            var result = await productOrderListRepository.GetProductOrderList();

            //Assert
            //confirm result
            //(expected, result)
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfProductOrderLists_WhenNoProductOrderListsExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();
            //Act
            var result = await productOrderListRepository.GetProductOrderList();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductOrderList>>(result);
            Assert.Empty(result);
        }


        [Fact]
        public async void GetById_ShouldReturnProductOrderLists_WhenProductOrderListsExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int productOrderListsId = 1;

            context.ProductOrderList.Add(new()
            {
                ProductOrderListID = productOrderListsId,
                Quantity = 1
            });

            await context.SaveChangesAsync();


            //Act
            var result = await productOrderListRepository.GetProductOrderListById(productOrderListsId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ProductOrderList>(result);
            Assert.Equal(productOrderListsId, result.ProductOrderListID);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenProductOrderListsDeosNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await productOrderListRepository.GetProductOrderListById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldAddNewIdToProductOrderLists_WhenSavingToDatabase()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            ProductOrderList productOrderList = new()
            {
                ProductOrderListID = expectedNewId,
                Quantity = 21
            };

            //Act
            var result = await productOrderListRepository.CreateProductOrderList(productOrderList);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ProductOrderList>(result);
            Assert.Equal(expectedNewId, result.ProductOrderListID);
        }

        [Fact]
        public async void Create_ShouldFailToAddNewIdToProductOrderList_WhenProductOrderListIdAlreadyExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            ProductOrderList productOrderList = new()
            {
                ProductOrderListID = 1,
                Quantity = 21
            };

            await productOrderListRepository.CreateProductOrderList(productOrderList);

            //Act
            async Task action() => await productOrderListRepository.CreateProductOrderList(productOrderList);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnProductOrderList_WhenProductOrderListExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int productOrderListId = 1;

            ProductOrderList newProductOrderList = new()
            {
                ProductOrderListID = productOrderListId,
                Quantity = 1
            };
            context.ProductOrderList.Add(newProductOrderList);
            await context.SaveChangesAsync();
            ProductOrderList updateProductOrderList = new()
            {
                ProductOrderListID = productOrderListId,
                Quantity = 1
            };
            var result = await productOrderListRepository.UpdateProductOrderList(productOrderListId, updateProductOrderList);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ProductOrderList>(result);
            Assert.Equal(productOrderListId, result.ProductOrderListID);
            Assert.Equal(updateProductOrderList.Quantity, result.Quantity);

        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenProductOrderListDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int productOrderListId = 1;

            ProductOrderList updateProductOrderList = new()
            {
                ProductOrderListID = productOrderListId,
                Quantity = 14
            };

            //Act
            var result = await productOrderListRepository.UpdateProductOrderList(productOrderListId, updateProductOrderList);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedProductOrderList_WhenCategoryIsDeleted()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int productOrderListId = 1;

            ProductOrderList newProductOrderList = new()
            {
                ProductOrderListID = productOrderListId,
                Quantity = 14
            };

            context.ProductOrderList.Add(newProductOrderList);
            await context.SaveChangesAsync();

            //Act
            var result = await productOrderListRepository.DeleteProductOrderList(productOrderListId);
            var category = await productOrderListRepository.GetProductOrderListById(productOrderListId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ProductOrderList>(result);
            Assert.Equal(productOrderListId, result.ProductOrderListID);

            Assert.Null(category);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenProductOrderListDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await productOrderListRepository.DeleteProductOrderList(1);

            //Assert
            Assert.Null(result);
        }
    }

}
