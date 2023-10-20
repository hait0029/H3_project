using H3_project.Interfaces;
using H3_project.Models;
using H3_project.Repositories;

namespace ProjectUnitTests.Repositories
{
    public class ProductRespositoryTests
    {
        private DbContextOptions<DatabaseContext> options; //server
        private DatabaseContext context; //udsignet på serveren
        private ProductRepository productRepository;

        public ProductRespositoryTests()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
           .UseInMemoryDatabase(databaseName: "ProductRepository")
           .Options;

            context = new DatabaseContext(options);

            productRepository = new(context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfProducts_WhenProductsExists()
        {
            //Arrange
            //create data
            await context.Database.EnsureDeletedAsync();

            context.Product.Add(new Product
            {
                ProductID = 1,
                ProductName = "Lenovo",
                Price = 3000
            });
            context.Product.Add(new Product
            {
                ProductID = 2,
                ProductName = "Lenovo thinkpad",
                Price = 5000
            });
            context.Product.Add(new Product
            {
                ProductID = 3,
                ProductName = "Lenovo x1 carbon",
                Price = 10000
            });
            context.SaveChanges();

            //Act
            //call method
            var result = await productRepository.GetProduct();

            //Assert
            //confirm result
            //(expected, result)
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfProducts_WhenNoProductsExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();
            //Act
            var result = await productRepository.GetProduct();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Empty(result);
        }


        [Fact]
        public async void GetById_ShouldReturnProducts_WhenProductsExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int productId = 1;

            context.Product.Add(new()
            {
                ProductID = productId,
                ProductName = "Lenovo",
                Price = 3000
            });

            await context.SaveChangesAsync();


            //Act
            var result = await productRepository.GetProductById(productId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.ProductID);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenProductsDeosNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await productRepository.GetProductById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldAddNewIdToProduct_WhenSavingToDatabase()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Product product = new()
            {
                ProductID = expectedNewId,
                ProductName = "Lenovo",
                Price = 3000
            };

            //Act
            var result = await productRepository.CreateProduct(product);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(expectedNewId, result.ProductID);
        }

        [Fact]
        public async void Create_ShouldFailToAddNewIdToProducts_WhenProductIdAlreadyExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            Product product = new()
            {
                ProductID = 1,
                ProductName = "Lenovo",
                Price = 3000
            };

            await productRepository.CreateProduct(product);

            //Act
            async Task action() => await productRepository.CreateProduct(product);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnProduct_WhenProductExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int productId = 1;

            Product newProduct = new()
            {
                ProductID = productId,
                ProductName = "Lenovo",
                Price = 3000

            };
            context.Product.Add(newProduct);
            await context.SaveChangesAsync();
            Product updateProduct = new()
            {
                ProductID = productId,
                ProductName = "Lenovo thinkpad",
                Price = 4000

            };
            var result = await productRepository.UpdateProduct(productId, updateProduct);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.ProductID);
            Assert.Equal(updateProduct.ProductName, result.ProductName);

        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenProductDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int productId = 1;

            Product updateProduct = new()
            {
                ProductID = productId,
                ProductName = "Lenovo",
                Price = 3000

            };

            //Act
            var result = await productRepository.UpdateProduct(productId, updateProduct);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedProduct_WhenProductIsDeleted()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int productId = 1;

            Product newProduct = new()
            {
                ProductID = productId,
                ProductName = "Lenovo",
                Price = 3000

            };

            context.Product.Add(newProduct);
            await context.SaveChangesAsync();

            //Act
            var result = await productRepository.DeleteProduct(productId);
            var product = await productRepository.GetProductById(productId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.ProductID);

            Assert.Null(product);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenProductDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await productRepository.DeleteProduct(1);

            //Assert
            Assert.Null(result);
        }
    }
}
