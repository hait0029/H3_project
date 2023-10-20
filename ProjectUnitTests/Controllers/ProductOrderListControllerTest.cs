using H3_project.Models;

namespace ProjectUnitTests.Controllers
{
    public class ProductOrderListControllerTest
    {
        private readonly ProductOrderListController productOrderListController;
        private Mock<IProductOrderListRepository> ProductOrderListRepositoryMock = new();

        public ProductOrderListControllerTest()
        {
            //creating an instance of the CategoryController class and initializing it with a mock object of the category repository
            productOrderListController = new ProductOrderListController(ProductOrderListRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenProductOrderListExists()
        {
            // arrange
            List<ProductOrderList> productOrderList = new();

            productOrderList.Add(new ProductOrderList
            {
                ProductOrderListID = 1,
                Quantity = 32
            });

            ProductOrderListRepositoryMock.Setup(x => x.GetProductOrderList()).ReturnsAsync(productOrderList);

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.GetAllProductOrderList();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_NoProductsExists()
        {
            // arrange
            List<ProductOrderList> productOrderList = new();

            ProductOrderListRepositoryMock.Setup(x => x.GetProductOrderList()).ReturnsAsync(productOrderList);

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.GetAllProductOrderList();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenProductOrderListRepositoryReturnsNull()
        {
            // arrange

            ProductOrderListRepositoryMock.Setup(x => x.GetProductOrderList()).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.GetAllProductOrderList();

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenProductOrderListExceptionIsRaised()
        {
            // Arrange
            ProductOrderListRepositoryMock.Setup(x => x.GetProductOrderList()).ReturnsAsync(() => throw new Exception("This is an Exception"));

            // Act
            var result = (IStatusCodeActionResult)await productOrderListController.GetAllProductOrderList();

            // Assert
            Assert.Equal(500, result.StatusCode);



        }

        [Fact]
        public async void GetByID_ShouldReturnStatusCode200_WhenProductOrderListDataExists()
        {
            // arrange
            int productOrderListId = 1;

            ProductOrderList productOrderList = new()
            {
               ProductOrderListID = productOrderListId,
               Quantity = 32
            };

            ProductOrderListRepositoryMock.Setup(x => x.GetProductOrderListById(It.IsAny<int>())).ReturnsAsync(productOrderList);

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.GetproductOrderListById(productOrderListId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenProductOrderListDoesNotExists()
        {
            // arrange
            int productOrderListId = 1;

            ProductOrderListRepositoryMock.Setup(x => x.GetProductOrderListById(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.GetproductOrderListById(productOrderListId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenProductOrderListExceptionIsRaised()
        {
            // arrange

            ProductOrderListRepositoryMock.Setup(x => x.GetProductOrderListById(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.GetproductOrderListById(1);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode201_WhenProductOrderListDataExists()
        {
            // arrange

            ProductOrderList newProductOrderList = new()
            {
                ProductOrderListID = 1,
                Quantity = 32
            };

            int productOrderListId = 1;

            ProductOrderList productOrderList = new()
            {
                ProductOrderListID = 1,
                Quantity = 323
            };


            ProductOrderListRepositoryMock.Setup(x => x.CreateProductOrderList(It.IsAny<ProductOrderList>())).ReturnsAsync(productOrderList);

            //Act

            var result = await productOrderListController.PostProductOrderList(newProductOrderList);

            //Assert
            var statusCodeResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenProductOrderListExceptionIsRaised()
        {
            // arrange

            ProductOrderList newProductOrderList = new()
            {
                ProductOrderListID = 1,
                Quantity = 32
            };


            ProductOrderListRepositoryMock.Setup(x => x.CreateProductOrderList(It.IsAny<ProductOrderList>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = await productOrderListController.PostProductOrderList(newProductOrderList);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenProductOrderListIsUpdatedSuccesfully()
        {
            //arrange

            ProductOrderList updateProductOrderList = new()
            {
                ProductOrderListID = 1,
                Quantity = 32
            };

            int ProductOrderListId = 1;

            ProductOrderList ProductOrderListResponse = new()
            {

                ProductOrderListID = ProductOrderListId,
                Quantity = 32
            };


            ProductOrderListRepositoryMock.Setup(x => x.UpdateProductOrderList(It.IsAny<int>(), It.IsAny<ProductOrderList>())).ReturnsAsync(ProductOrderListResponse);

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.PutProductOrderList(updateProductOrderList, ProductOrderListId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenProductOrderListExceptionIsRaised()
        {
            // arrange

            ProductOrderList updateProductOrderList = new()
            {
                ProductOrderListID = 1,
                Quantity = 32
            };

            int ProductOrderListId = 1;

            ProductOrderListRepositoryMock.Setup(x => x.UpdateProductOrderList(It.IsAny<int>(), It.IsAny<ProductOrderList>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.PutProductOrderList(updateProductOrderList, ProductOrderListId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenProductOrderListIsDeleted()
        {
            // arrange
            int ProductOrderListId = 1;

            ProductOrderList ProductOrderListResponse = new()
            {
                ProductOrderListID = ProductOrderListId,
                Quantity = 32
            };


            ProductOrderListRepositoryMock.Setup(x => x.DeleteProductOrderList(It.IsAny<int>())).ReturnsAsync(ProductOrderListResponse);

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.DeleteProductOrderList(ProductOrderListId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenProductOrderListDoesNotExists()
        {
            // arrange
            int productOrderListId = 1;

            ProductOrderListRepositoryMock.Setup(x => x.DeleteProductOrderList(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.DeleteProductOrderList(productOrderListId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenProductOrderListExceptionIsRaised()
        {
            // arrange
            int productOrderListId = 1;

            ProductOrderListRepositoryMock.Setup(x => x.DeleteProductOrderList(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await productOrderListController.DeleteProductOrderList(productOrderListId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
