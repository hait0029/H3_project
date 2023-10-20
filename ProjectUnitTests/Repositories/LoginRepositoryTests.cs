using H3_project.Interfaces;
using H3_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProjectUnitTests.Repositories
{
    public class LoginRepositoryTests
    {
        private DbContextOptions<DatabaseContext> options; //server
        private DatabaseContext context; //udsignet på serveren
        private LoginRepository loginRepository;

        public LoginRepositoryTests()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "LoginRepository")
            .Options;

            context = new DatabaseContext(options);

            loginRepository = new(context);
        }

        [Fact]
        public async void GetAllUserLogin_ShouldReturnListOfLogin_WhenLoginsExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            context.Login.Add(new()
            {
                LoginID = 1,
                Email = "Admin@Gmail.com",
                Password = "password"
            });

            context.Login.Add(new()
            {
                LoginID = 2,
                Email = "Employee@Gmail.com",
                Password = "password",
            });

            context.Login.Add(new()
            {
                LoginID = 3,
                Email = "Customer@Gmail.com",
                Password = "password",
            });

            context.SaveChanges();

            await context.SaveChangesAsync();

            //Act
            var result = await loginRepository.GetLogin();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Login>>(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void GetAllUserLogin_ShouldReturnEmptyListOfLogin_WhenNoLoginsExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await loginRepository.GetLogin();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Login>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetUserLoginById_ShouldReturnLogin_WhenLoginsExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int UserLoginId = 1;

            context.Login.Add(new()
            {
                LoginID = UserLoginId,
                Email = "Admin@Gmail.com",
                Password = "password"
            });

            await context.SaveChangesAsync();


            //Act
            var result = await loginRepository.GetLoginById(UserLoginId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Login>(result);
            Assert.Equal(UserLoginId, result.LoginID);
        }

        [Fact]
        public async void GetUserLoginById_ShouldReturnNull_WhenLoginDeosNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await loginRepository.GetLoginById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateUserLogin_ShouldAddNewUserLogin_WhenSavingToDatabase()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Login login = new()
            {
                LoginID = expectedNewId,
                Email = "Admin@Gmail.com",
                Password = "password",
            };

            //Act
            var result = await loginRepository.CreateLogin(login);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Login>(result);
            Assert.Equal(expectedNewId, result.LoginID);
        }

        [Fact]
        public async void CreateUserLogin_ShouldFailToAddNewUserLogin_WhenLoginAlreadyExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            Login login = new()
            {
                LoginID = 1,
                Email = "Admin@Gmail.com",
                Password = "password"
            };

            await loginRepository.CreateLogin(login);

            // Act
            async Task action() => await loginRepository.CreateLogin(login);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("Login already exists", ex.Message);
        }

        [Fact]
        public async void UpdateUserLogin_ShouldChangeValuesOnProduct_WhenProductExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int UserLoginId = 1;

            Login newLogin = new()
            {
                LoginID = UserLoginId,
                Email = "Admin@Gmail.com",
                Password = "password",
            };

            context.Login.Add(newLogin);
            await context.SaveChangesAsync();

            Login updateUserLogin = new()
            {
                LoginID = UserLoginId,
                Email = "NewAdmin@Gmail.com",
                Password = "Newpassword",
            };

            //Act
            var result = await loginRepository.UpdateLogin(UserLoginId, updateUserLogin);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Login>(result);
            Assert.Equal(UserLoginId, result.LoginID);
            Assert.Equal(updateUserLogin.Email, result.Email);
            Assert.Equal(updateUserLogin.Password, result.Password);
        }

        [Fact]
        public async void UpdateUserLogin_ShouldReturnNull_WhenLoginDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int UserLoginId = 1;

            Login updateUserLogin = new()
            {
                LoginID = UserLoginId,
                Email = "NewAdmin@Gmail.com",
                Password = "Newpassword",
            };

            //Act
            var result = await loginRepository.UpdateLogin(UserLoginId, updateUserLogin);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedLogin_WhenLoginIsDeleted()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int UserLoginId = 1;

            Login UserLogin = new()
            {
                LoginID = UserLoginId,
                Email = "NewAdmin@Gmail.com",
                Password = "Newpassword",
            };

            context.Login.Add(UserLogin);
            await context.SaveChangesAsync();

            //Act
            var result = await loginRepository.DeleteLogin(UserLoginId);
            var category = await loginRepository.GetLoginById(UserLoginId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Login>(result);
            Assert.Equal(UserLoginId, result.LoginID);

            Assert.Null(category);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenCategoryDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await loginRepository.DeleteLogin(1);

            //Assert
            Assert.Null(result);
        }
    }
}
