namespace ProjectUnitTests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController userController;
        private Mock<IUserRepository> UserRepositoryMock = new();

        public UserControllerTests()
        {
            //creating an instance of the CategoryController class and initializing it with a mock object of the category repository
            userController = new UserController(UserRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenUsersExists()
        {
            // arrange
            List<User> users = new();

            users.Add(new User
            {
                UserID = 1,
                FName = "Haittham",
                LName = "cams",
                Address = "ffeaf st.tv",
                Phone = 9913131
            });

            UserRepositoryMock.Setup(x => x.GetUser()).ReturnsAsync(users);

            //Act

            var result = (IStatusCodeActionResult)await userController.GetAllUser();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_NoUserExists()
        {
            // arrange
            List<User> users = new();

            UserRepositoryMock.Setup(x => x.GetUser()).ReturnsAsync(users);

            //Act

            var result = (IStatusCodeActionResult)await userController.GetAllUser();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenUserRepositoryReturnsNull()
        {
            // arrange

            UserRepositoryMock.Setup(x => x.GetUser()).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await userController.GetAllUser();

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenUserExceptionIsRaised()
        {
            // Arrange
            UserRepositoryMock.Setup(x => x.GetUser()).ReturnsAsync(() => throw new Exception("This is an Exception"));

            // Act
            var result = (IStatusCodeActionResult)await userController.GetAllUser();

            // Assert
            Assert.Equal(500, result.StatusCode);



        }

        [Fact]
        public async void GetByID_ShouldReturnStatusCode200_WhenUserDataExists()
        {
            // arrange
            int userId = 1;

            User user = new()
            {
                UserID = userId,
                FName = "Haittham",
                LName = "cams",
                Address = "ffeaf st.tv",
                Phone = 9913131
            };

            UserRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(user);

            //Act

            var result = (IStatusCodeActionResult)await userController.GetUserById(userId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenUserDoesNotExists()
        {
            // arrange
            int userId = 1;

            UserRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await userController.GetUserById(userId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenUserExceptionIsRaised()
        {
            // arrange

            UserRepositoryMock.Setup(x => x.GetUserById(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await userController.GetUserById(1);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode201_WhenCategoryDataExists()
        {
            // arrange

            User newUser = new()
            {
                UserID = 1,
                FName = "Haittham",
                LName = "cams",
                Address = "ffeaf st.tv",
                Phone = 9913131
            };

            int userId = 1;

            User user = new()
            {
                UserID = userId,
                FName = "Haittham",
                LName = "black",
                Address = "fatso st.tv",
                Phone = 9913131
            };


            UserRepositoryMock.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(user);

            //Act

            var result = await userController.PostUser(newUser);

            //Assert
            var statusCodeResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenUserExceptionIsRaised()
        {
            // arrange

            User newUser = new()
            {
                UserID = 1,
                FName = "Haittham",
                LName = "cams",
                Address = "ffeaf st.tv",
                Phone = 9913131
            };


            UserRepositoryMock.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = await userController.PostUser(newUser);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUserIsUpdatedSuccesfully()
        {
            //arrange

            User updateUser = new()
            {
                FName = "Haittham"
            };

            int userId = 1;

            User UserResponse = new()
            {
                UserID = 1,
                FName = "Haittham",
                LName = "cams",
                Address = "ffeaf st.tv",
                Phone = 9913131
            };


            UserRepositoryMock.Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(UserResponse);

            //Act

            var result = (IStatusCodeActionResult)await userController.PutUser(updateUser, userId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenUserExceptionIsRaised()
        {
            // arrange

            User updateUser = new()
            {
                FName = "Haittham"
            };


            int userId = 1;

            UserRepositoryMock.Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await userController.PutUser(updateUser, userId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenUserIsDeleted()
        {
            // arrange
            int userId = 1;

            User UserResponse = new()
            {
                UserID = 1,
                FName = "Haittham",
                LName = "cams",
                Address = "ffeaf st.tv",
                Phone = 9913131
            };

            UserRepositoryMock.Setup(x => x.DeleteUser(It.IsAny<int>())).ReturnsAsync(UserResponse);

            //Act

            var result = (IStatusCodeActionResult)await userController.DeleteUser(userId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenUserDoesNotExists()
        {
            // arrange
            int userId = 1;

            UserRepositoryMock.Setup(x => x.DeleteUser(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await userController.DeleteUser(userId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenUserExceptionIsRaised()
        {
            // arrange
            int userId = 1;

            UserRepositoryMock.Setup(x => x.DeleteUser(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await userController.DeleteUser(userId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
