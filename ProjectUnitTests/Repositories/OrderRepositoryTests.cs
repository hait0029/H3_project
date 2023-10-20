using H3_project.Interfaces;
using H3_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProjectUnitTests.Repositories
{
    public class OrderRepositoryTests
    {
        private DbContextOptions<DatabaseContext> options; //server
        private DatabaseContext context; //udsignet på serveren
        private OrderRepository orderRepository;

        public OrderRepositoryTests()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "OrderRepository")
            .Options;

            context = new DatabaseContext(options);

            orderRepository = new(context);
        }

        [Fact]
        public async void GetAllOrders_ShouldReturnListOfOrders_WhenOrdersExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            List<Order> orderLine = new();
            User user = new()
            {
                UserID = 1,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341
            };

            context.Order.Add(new()
            {
                OrderID = 1,
                OrderDate = DateTime.Now,
                user = user
            });

            await context.SaveChangesAsync();

            //Act
            var result = await orderRepository.GetOrder();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Order>>(result);
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void GetAllOrders_ShouldReturnEmptyListOfOrders_WhenNoOrdersExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await orderRepository.GetOrder();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Order>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetOrderById_ShouldReturnOrder_WhenOrderExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int orderId = 1;

            List<Order> orders = new();
            User user = new()
            {
                UserID = 1,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341
            };

            context.Order.Add(new()
            {
                OrderID = 1,
                OrderDate = DateTime.Now,
                user = user
            });

            await context.SaveChangesAsync();


            //Act
            var result = await orderRepository.GetOrderById(orderId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(orderId, result.OrderID);
        }

        [Fact]
        public async void GetOrderById_ShouldReturnNull_WhenOrderDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await orderRepository.GetOrderById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateOrder_ShouldAddNewOrder_WhenSavingToDatabase()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int newOrdersId = 1;

            List<Order> orders = new();
            Order order = new()
            {
                OrderID = newOrdersId,
                OrderDate = DateTime.Now,
                user = new()
                {
                    UserID = 1,
                    FName = "Ben",
                    LName = "jack",
                    Address = "HejVej 2",
                    Phone = 12341
                }

            };

            //Act
            var result = await orderRepository.CreateOrder(order);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(newOrdersId, result.OrderID);
        }

        [Fact]
        public async void CreateOrder_ShouldFailToAddNewOrder_WhenOrderAlreadyExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            List<Order> orders = new();
            Order order = new()
            {
                OrderID = 1,
                OrderDate = DateTime.Now,
                user = new()
                {
                    UserID = 1,
                    FName = "Ben",
                    LName = "jack",
                    Address = "HejVej 2",
                    Phone = 12341
                }
            };

            await orderRepository.CreateOrder(order);

            //Act
            async Task action() => await orderRepository.CreateOrder(order);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateOrder_ShouldChangeValuesOnOrder_WhenOrderExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            List<Order> orders = new();
            User user = new()
            {
                UserID = 1,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341
            };

            int orderId = 1;


            Order newOrder = new()
            {
                OrderID = orderId,
                OrderDate = DateTime.Now,
                user = user,

            };
            context.Order.Add(newOrder);
            await context.SaveChangesAsync();

            Order updateOrder = new()
            {
                OrderID = orderId,
                OrderDate = new DateTime(2022, 11, 22),
                user = user
            };

            //Act
            var result = await orderRepository.UpdateOrder(orderId, updateOrder);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(orderId, result.OrderID);
            Assert.Equal(updateOrder.OrderDate, result.OrderDate);
            Assert.Equal(updateOrder.user, result.user);

        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenOrderDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int orderId = 1;

            Order updateOrder = new()
            {
                OrderID = orderId,
                OrderDate = new DateTime(2022, 11, 22),
                
            };

            //Act
            var result = await orderRepository.UpdateOrder(orderId, updateOrder);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedOrder_WhenOrderIsDeleted()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int orderId = 1;

            Order newOrder = new()
            {
                OrderID = orderId,
                OrderDate = new DateTime(2022, 11, 22),

            };

            context.Order.Add(newOrder);
            await context.SaveChangesAsync();

            //Act
            var result = await orderRepository.DeleteOrder(orderId);
            var category = await orderRepository.GetOrderById(orderId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(orderId, result.OrderID);

            Assert.Null(category);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenCategoryDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await orderRepository.DeleteOrder(1);

            //Assert
            Assert.Null(result);
        }

    }
}
