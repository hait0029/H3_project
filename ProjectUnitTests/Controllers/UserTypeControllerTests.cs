using H3_project.Controllers;
using H3_project.Models;

namespace ProjectUnitTests.Controllers
{
    public class UserTypeControllerTests
    {
        private readonly UserTypeController userTypeController;
        private Mock<IUserTypeRepository> UserTypeRepositoryMock = new();

        public UserTypeControllerTests()
        {
            //creating an instance of the CategoryController class and initializing it with a mock object of the category repository
            userTypeController = new UserTypeController(UserTypeRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenUserTypesExists()
        {
            // arrange
            List<UserType> userTypes = new();

            userTypes.Add(new UserType
            {
                UsertypeID = 1,
                UserNameType = "Admin"
            });

            UserTypeRepositoryMock.Setup(x => x.GetUserType()).ReturnsAsync(userTypes);

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.GetAllUserType();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_NoUserTypeExists()
        {
            // arrange
            List<UserType> userTypes = new();

            UserTypeRepositoryMock.Setup(x => x.GetUserType()).ReturnsAsync(userTypes);

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.GetAllUserType();

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenUserTypeRepositoryReturnsNull()
        {
            // arrange

            UserTypeRepositoryMock.Setup(x => x.GetUserType()).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.GetAllUserType();

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenUserTypeExceptionIsRaised()
        {
            // Arrange
            UserTypeRepositoryMock.Setup(x => x.GetUserType()).ReturnsAsync(() => throw new Exception("This is an Exception"));

            // Act
            var result = (IStatusCodeActionResult)await userTypeController.GetAllUserType();

            // Assert
            Assert.Equal(500, result.StatusCode);



        }

        [Fact]
        public async void GetByID_ShouldReturnStatusCode200_WhenUserTypeDataExists()
        {
            // arrange
            int userTypeId = 1;

            UserType userType = new()
            {
                UsertypeID = userTypeId,
                UserNameType = "Admin"
            };

            

            UserTypeRepositoryMock.Setup(x => x.GetUserTypeById(It.IsAny<int>())).ReturnsAsync(userType);

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.GetUserTypesById(userTypeId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenUserTypeDoesNotExists()
        {
            // arrange
            int userTypeId = 1;

            UserTypeRepositoryMock.Setup(x => x.GetUserTypeById(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.GetUserTypesById(userTypeId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenUserTypeExceptionIsRaised()
        {
            // arrange

            UserTypeRepositoryMock.Setup(x => x.GetUserTypeById(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.GetUserTypesById(1);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode201_WhenUserTypeDataExists()
        {
            // arrange

            UserType newUserType = new()
            {
                UsertypeID = 1,
                UserNameType = "Admin"
            };

            int userTypeId = 1;

            UserType userType = new()
            {
                UsertypeID = 1,
                UserNameType = "Admin"
            };


            UserTypeRepositoryMock.Setup(x => x.CreateUserType(It.IsAny<UserType>())).ReturnsAsync(userType);

            //Act

            var result = await userTypeController.PostUserTypes(newUserType);

            //Assert
            var statusCodeResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenUserTypeExceptionIsRaised()
        {
            // arrange

            UserType newUserType = new()
            {
                UsertypeID = 1,
                UserNameType = "Admin"
            };


            UserTypeRepositoryMock.Setup(x => x.CreateUserType(It.IsAny<UserType>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = await userTypeController.PostUserTypes(newUserType);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUserTypeIsUpdatedSuccesfully()
        {
            //arrange

            UserType updateUserType = new()
            {
                UserNameType = "Admin"
            };

            int userTypeId = 1;

            UserType UserTypeResponse = new()
            {
                UsertypeID = userTypeId,
                UserNameType = "Admin"
            };


            UserTypeRepositoryMock.Setup(x => x.UpdateUserType(It.IsAny<int>(), It.IsAny<UserType>())).ReturnsAsync(UserTypeResponse);

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.PutUserType(updateUserType, userTypeId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenUserTypeExceptionIsRaised()
        {
            // arrange

            UserType updateUserType = new()
            {
                UserNameType = "Admin"
            };

            int userTypeId = 1;

            UserTypeRepositoryMock.Setup(x => x.UpdateUserType(It.IsAny<int>(), It.IsAny<UserType>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.PutUserType(updateUserType, userTypeId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenUserTypeIsDeleted()
        {
            // arrange
            int userTypeId = 1;

            UserType UserTypeResponse = new()
            {
                UsertypeID = userTypeId,
                UserNameType = "Admin"
            };

            UserTypeRepositoryMock.Setup(x => x.DeleteUserType(It.IsAny<int>())).ReturnsAsync(UserTypeResponse);

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.DeleteUserTypes(userTypeId);

            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenUserTypeDoesNotExists()
        {
            // arrange
            int userTypeId = 1;

            UserTypeRepositoryMock.Setup(x => x.DeleteUserType(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.DeleteUserTypes(userTypeId);

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenUserTypeExceptionIsRaised()
        {
            // arrange
            int userTypeId = 1;

            UserTypeRepositoryMock.Setup(x => x.DeleteUserType(It.IsAny<int>())).ReturnsAsync(() => throw new Exception("This is an exception"));

            //Act

            var result = (IStatusCodeActionResult)await userTypeController.DeleteUserTypes(userTypeId);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
