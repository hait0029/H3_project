using H3_project.Models;

namespace ProjectUnitTests.Controllers
{
    public class LoginControllerTest
    {
        private readonly LoginController loginController;
        private Mock<ILoginRepository> LoginRepositoryMock = new();

        public LoginControllerTest()
        {
            //creating an instance of the LoginController class and initializing it with a mock object of the login repository
            loginController = new LoginController(LoginRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenLoginsExists()
        {
            // arrange
            List<Login> logins = new();

            logins.Add(new Login
            {
                LoginID = 1,
                Email = "Admin@gmail.com",
                Password = "password"
            });

            LoginRepositoryMock.Setup(x => x.GetLogin()).ReturnsAsync(logins);

            //Act

            var result = (IStatusCodeActionResult)await loginController.GetAllLogin();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_NoLoginsExists()
        {
            // arrange
            List<Login> logins = new();

            LoginRepositoryMock.Setup(x => x.GetLogin()).ReturnsAsync(logins);

            //Act

            var result = (IStatusCodeActionResult)await loginController.GetAllLogin();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenLoginRepositoryReturnsNull()
        {
            // arrange

            LoginRepositoryMock.Setup(x => x.GetLogin()).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await loginController.GetAllLogin();

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenLoginExceptionIsRaised()
        {
            // Arrange
            LoginRepositoryMock.Setup(x => x.GetLogin()).ReturnsAsync(() => throw new Exception("This is an Exception"));

            // Act
            var result = (IStatusCodeActionResult)await loginController.GetAllLogin();

            // Assert
            Assert.Equal(500, result.StatusCode);



        }

        [Fact]
        public async void GetByID_ShouldReturnStatusCode200_WhenLoginDataExists()
        {
            // arrange
            int loginId = 1;

            Login login = new()
            {
                LoginID = 1,
                Email = "Admin@gmail.com",
                Password = "password"
            };

            LoginRepositoryMock.Setup(x => x.GetLoginById(It.IsAny<int>())).ReturnsAsync(login);

            //Act

            var result = (IStatusCodeActionResult)await loginController.GetLoginById(loginId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenLoginsDoesNotExists()
        {
            // arrange
            int loginId = 1;

            LoginRepositoryMock.Setup(x => x.GetLoginById(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await loginController.GetLoginById(loginId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenLoginExceptionIsRaised()
        {
            // arrange

            LoginRepositoryMock.Setup(x => x.GetLoginById(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await loginController.GetLoginById(1);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode201_WhenLoginDataExists()
        {
            // arrange

            Login newLogin = new()
            {
                Email = "Admin@gmail.com"
            };

            int loginId = 1;

            Login login = new()
            {
                LoginID = 1,
                Email = "Admin@gmail.com",
                Password = "password"
            };


            LoginRepositoryMock.Setup(x => x.CreateLogin(It.IsAny<Login>())).ReturnsAsync(login);

            //Act

            var result = await loginController.PostLogin(newLogin);

            //Assert
            var statusCodeResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenLoginExceptionIsRaised()
        {
            // arrange

            Login newLogin = new()
            {
                Email = "Admin@gmail.com"
            };

            LoginRepositoryMock.Setup(x => x.CreateLogin(It.IsAny<Login>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = await loginController.PostLogin(newLogin);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenLoginIsUpdatedSuccesfully()
        {
            //arrange

            Login updateLogin = new()
            {
                Email = "Admin@gmail.com"
            };

            int loginId = 1;

            Login loginResponse = new()
            {
                LoginID = 1,
                Email = "Admin@gmail.com",
                Password = "password"
            };



            LoginRepositoryMock.Setup(x => x.UpdateLogin(It.IsAny<int>(), It.IsAny<Login>())).ReturnsAsync(loginResponse);

            //Act

            var result = (IStatusCodeActionResult)await loginController.PutLogin(updateLogin, loginId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenLoginExceptionIsRaised()
        {
            // arrange

            Login updateLogin = new()
            {
                Email = "Admin@gmail.com"
            };

            int loginId = 1;

            LoginRepositoryMock.Setup(x => x.UpdateLogin(It.IsAny<int>(), It.IsAny<Login>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await loginController.PutLogin(updateLogin, loginId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenLoginIsDeleted()
        {
            // arrange
            int loginId = 1;

            Login LoginResponse = new()
            {
                LoginID = loginId,
                Email = "Admin@gmail.com",
                Password = "password"

            };

            LoginRepositoryMock.Setup(x => x.DeleteLogin(It.IsAny<int>())).ReturnsAsync(LoginResponse);

            //Act

            var result = (IStatusCodeActionResult)await loginController.DeleteLogin(loginId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenLoginDoesNotExists()
        {
            // arrange
            int loginId = 1;

            LoginRepositoryMock.Setup(x => x.DeleteLogin(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await loginController.DeleteLogin(loginId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenLoginExceptionIsRaised()
        {
            // arrange
            int loginId = 1;

            LoginRepositoryMock.Setup(x => x.DeleteLogin(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await loginController.DeleteLogin(loginId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
