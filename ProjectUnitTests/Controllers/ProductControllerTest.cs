using H3_project.Models;

namespace ProjectUnitTests.Controllers
{
    public class ProductControllerTest
    {
        private readonly ProductController productController;
        private Mock<IProductRepository> ProductRepositoryMock = new();

        public ProductControllerTest()
        {
            //creating an instance of the CategoryController class and initializing it with a mock object of the category repository
            productController = new ProductController(ProductRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenProductExists()
        {
            // arrange
            List<Product> products = new();

            products.Add(new Product
            {
                ProductID = 1,
                ProductName = "Lenovo",
                Price = 9000
            });

            ProductRepositoryMock.Setup(x => x.GetProduct()).ReturnsAsync(products);

            //Act

            var result = (IStatusCodeActionResult)await productController.GetAllProduct();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_NoProductsExists()
        {
            // arrange
            List<Product> products = new();

            ProductRepositoryMock.Setup(x => x.GetProduct()).ReturnsAsync(products);

            //Act

            var result = (IStatusCodeActionResult)await productController.GetAllProduct();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenProductRepositoryReturnsNull()
        {
            // arrange

            ProductRepositoryMock.Setup(x => x.GetProduct()).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await productController.GetAllProduct();

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenProductExceptionIsRaised()
        {
            // Arrange
            ProductRepositoryMock.Setup(x => x.GetProduct()).ReturnsAsync(() => throw new Exception("This is an Exception"));

            // Act
            var result = (IStatusCodeActionResult)await productController.GetAllProduct();

            // Assert
            Assert.Equal(500, result.StatusCode);



        }

        [Fact]
        public async void GetByID_ShouldReturnStatusCode200_WhenProductDataExists()
        {
            // arrange
            int productId = 1;

            Product product = new()
            {
                ProductID = productId,
                ProductName = "Lenovo",
                Price = 9000
            };

            ProductRepositoryMock.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(product);

            //Act

            var result = (IStatusCodeActionResult)await productController.GetProductById(productId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenProductsDoesNotExists()
        {
            // arrange
            int productId = 1;

            ProductRepositoryMock.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await productController.GetProductById(productId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenProductExceptionIsRaised()
        {
            // arrange

            ProductRepositoryMock.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await productController.GetProductById(1);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode201_WhenProductDataExists()
        {
            // arrange

            Product newProduct = new()
            {
                ProductID = 1,
                ProductName = "Lenovo",
                Price = 9000
            };

            int productId = 1;

            Product Product = new()
            {
                ProductID = productId,
                ProductName = "Lenovo",
                Price = 9000
            };


            ProductRepositoryMock.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(Product);

            //Act

            var result = await productController.PostProduct(newProduct);

            //Assert
            var statusCodeResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenProductExceptionIsRaised()
        {
            // arrange

            Product newProduct = new()
            {
                ProductID = 1,
                ProductName = "Lenovo",
                Price = 9000
            };


            ProductRepositoryMock.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = await productController.PostProduct(newProduct);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenProductIsUpdatedSuccesfully()
        {
            //arrange

            Product updateProduct = new()
            {
                ProductID = 1,
                ProductName = "Lenovo",
                Price = 9000
            };

            int productId = 1;

            Product ProductResponse = new()
            {
                ProductID = productId,
                ProductName = "Lenovo thinkpad",
                Price = 10000
            };


            ProductRepositoryMock.Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<Product>())).ReturnsAsync(ProductResponse);

            //Act

            var result = (IStatusCodeActionResult)await productController.PutProduct(updateProduct, productId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenProductExceptionIsRaised()
        {
            // arrange

            Product updateProduct = new()
            {
                ProductID = 1,
                ProductName = "Lenovo",
                Price = 9000
            };


            int categoryId = 1;

            ProductRepositoryMock.Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<Product>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await productController.PutProduct(updateProduct, categoryId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenProductIsDeleted()
        {
            // arrange
            int productId = 1;

            Product ProductResponse = new()
            {
                ProductID = productId,
                ProductName = "Lenovo thinkpad",
                Price = 10000
            };

            ProductRepositoryMock.Setup(x => x.DeleteProduct(It.IsAny<int>())).ReturnsAsync(ProductResponse);

            //Act

            var result = (IStatusCodeActionResult)await productController.DeleteProduct(productId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenProductDoesNotExists()
        {
            // arrange
            int productId = 1;

            ProductRepositoryMock.Setup(x => x.DeleteProduct(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await productController.DeleteProduct(productId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenProductExceptionIsRaised()
        {
            // arrange
            int productId = 1;

            ProductRepositoryMock.Setup(x => x.DeleteProduct(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await productController.DeleteProduct(productId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
