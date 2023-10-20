using H3_project.Models;

namespace ProjectUnitTests.Controllers
{
    public class OrderControllerTest
    {
        private readonly OrderController orderController;
        private Mock<IOrderRepository> OrderRepositoryMock = new();

        public OrderControllerTest()
        {
            //creating an instance of the OrderController class and initializing it with a mock object of the order repository
            orderController = new OrderController(OrderRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenOrdersExists()
        {
            // arrange
            List<Order> orders = new();

            orders.Add(new Order
            {
                OrderID = 1,
                OrderDate = DateTime.Now
            });

            OrderRepositoryMock.Setup(x => x.GetOrder()).ReturnsAsync(orders);

            //Act

            var result = (IStatusCodeActionResult)await orderController.GetAllOrder();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_NoOrdersExists()
        {
            // arrange
            List<Order> orders = new();

            OrderRepositoryMock.Setup(x => x.GetOrder()).ReturnsAsync(orders);

            //Act

            var result = (IStatusCodeActionResult)await orderController.GetAllOrder();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenOrderRepositoryReturnsNull()
        {
            // arrange

            OrderRepositoryMock.Setup(x => x.GetOrder()).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await orderController.GetAllOrder();

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenOrderExceptionIsRaised()
        {
            // Arrange
            OrderRepositoryMock.Setup(x => x.GetOrder()).ReturnsAsync(() => throw new Exception("This is an Exception"));

            // Act
            var result = (IStatusCodeActionResult)await orderController.GetAllOrder();

            // Assert
            Assert.Equal(500, result.StatusCode);



        }

        [Fact]
        public async void GetByID_ShouldReturnStatusCode200_WhenOrderDataExists()
        {
            // arrange
            int orderId = 1;

            Order order = new()
            {
                OrderID = orderId,
                OrderDate = DateTime.Now
            };

            OrderRepositoryMock.Setup(x => x.GetOrderById(It.IsAny<int>())).ReturnsAsync(order);

            //Act

            var result = (IStatusCodeActionResult)await orderController.GetOrderById(orderId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenOrdersDoesNotExists()
        {
            // arrange
            int orderId = 1;

            OrderRepositoryMock.Setup(x => x.GetOrderById(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await orderController.GetOrderById(orderId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenOrderExceptionIsRaised()
        {
            // arrange

            OrderRepositoryMock.Setup(x => x.GetOrderById(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await orderController.GetOrderById(1);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode201_WhenOrderDataExists()
        {
            // arrange

            Order newOrder = new()
            {
                OrderDate = DateTime.Now
            };

            int orderId = 1;

            Order order = new()
            {
                OrderID = orderId,
                OrderDate = DateTime.Now
            };


            OrderRepositoryMock.Setup(x => x.CreateOrder(It.IsAny<Order>())).ReturnsAsync(order);

            //Act

            var result = await orderController.PostOrder(newOrder);

            //Assert
            var statusCodeResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenOrderExceptionIsRaised()
        {
            // arrange

            Order newOrder = new()
            {
                OrderDate = DateTime.Now
            };


            OrderRepositoryMock.Setup(x => x.CreateOrder(It.IsAny<Order>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = await orderController.PostOrder(newOrder);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenOrderIsUpdatedSuccesfully()
        {
            //arrange

            Order updateOrder = new()
            {
                OrderDate = DateTime.Now
            };

            int orderId = 1;

            Order OrderResponse = new()
            {
                OrderID = orderId,
                OrderDate = DateTime.Now
            };


            OrderRepositoryMock.Setup(x => x.UpdateOrder(It.IsAny<int>(), It.IsAny<Order>())).ReturnsAsync(OrderResponse);

            //Act

            var result = (IStatusCodeActionResult)await orderController.PutOrder(updateOrder, orderId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenOrderExceptionIsRaised()
        {
            // arrange

            Order updateOrder = new()
            {
                OrderDate = DateTime.Now
            };

            int orderId = 1;

            OrderRepositoryMock.Setup(x => x.UpdateOrder(It.IsAny<int>(), It.IsAny<Order>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await orderController.PutOrder(updateOrder, orderId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenOrderIsDeleted()
        {
            // arrange
            int orderId = 1;

            Order OrderResponse = new()
            {
                OrderID = orderId,
                OrderDate = DateTime.Now
            };


            OrderRepositoryMock.Setup(x => x.DeleteOrder(It.IsAny<int>())).ReturnsAsync(OrderResponse);

            //Act

            var result = (IStatusCodeActionResult)await orderController.DeleteOrder(orderId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenOrderDoesNotExists()
        {
            // arrange
            int orderId = 1;

            OrderRepositoryMock.Setup(x => x.DeleteOrder(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await orderController.DeleteOrder(orderId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenOrderExceptionIsRaised()
        {
            // arrange
            int orderId = 1;

            OrderRepositoryMock.Setup(x => x.DeleteOrder(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await orderController.DeleteOrder(orderId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
