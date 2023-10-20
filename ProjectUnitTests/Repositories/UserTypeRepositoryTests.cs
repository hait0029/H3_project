using H3_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUnitTests.Repositories
{
    public class UserTypeRepositoryTests
    {
        private DbContextOptions<DatabaseContext> options; //server
        private DatabaseContext context; //udsignet på serveren
        private UserTypeRepository userTypeRepository;


        public UserTypeRepositoryTests()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "UserTypeRepository")
            .Options;

            context = new DatabaseContext(options);

            userTypeRepository = new(context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfUserTypes_WhenUserTypesExists()
        {
            //Arrange
            //create data
            await context.Database.EnsureDeletedAsync();

            context.UserType.Add(new UserType
            {
                UsertypeID = 1,
                UserNameType = "Admin",
                Id = 1
            });
            context.UserType.Add(new UserType
            {
                UsertypeID = 2,
                UserNameType = "Customer",
                Id = 2
            });

            context.SaveChanges();

            //Act
            //call method
            var result = await userTypeRepository.GetUserType();

            //Assert
            //confirm result
            //(expected, result)
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfUserTypes_WhenNoUserTypesExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();
            //Act
            var result = await userTypeRepository.GetUserType();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserType>>(result);
            Assert.Empty(result);
        }


        [Fact]
        public async void GetById_ShouldReturnUserTypes_WhenUserTypesExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int userTypesId = 1;

            context.UserType.Add(new()
            {
                UsertypeID = userTypesId,
                UserNameType = "Admin",
                Id = 1
            });

            await context.SaveChangesAsync();


            //Act
            var result = await userTypeRepository.GetUserTypeById(userTypesId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserType>(result);
            Assert.Equal(userTypesId, result.UsertypeID);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenUserTypesDeosNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await userTypeRepository.GetUserTypeById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldAddNewIdToUserType_WhenSavingToDatabase()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            UserType userType = new()
            {
                UsertypeID = expectedNewId,
                UserNameType = "Admin",
                Id = 1
            };

            //Act
            var result = await userTypeRepository.CreateUserType(userType);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserType>(result);
            Assert.Equal(expectedNewId, result.UsertypeID);
        }

        [Fact]
        public async void Create_ShouldFailToAddNewIdToUserType_WhenUserTypeIdAlreadyExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            UserType userType = new()
            {
                UsertypeID = 1,
                UserNameType = "Admin",
                Id = 1
            };

            await userTypeRepository.CreateUserType(userType);

            //Act
            async Task action() => await userTypeRepository.CreateUserType(userType);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnUserType_WhenUserTypeExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int userTypeId = 1;

            UserType newUserType = new()
            {
                UsertypeID = userTypeId,
                UserNameType = "Admin",
                Id = 1
            };
            context.UserType.Add(newUserType);
            await context.SaveChangesAsync();
            UserType updateUserType = new()
            {
                UsertypeID = userTypeId,
                UserNameType = "Employee",
                Id = 1

            };
            var result = await userTypeRepository.UpdateUserType(userTypeId, updateUserType);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserType>(result);
            Assert.Equal(userTypeId, result.UsertypeID);
            Assert.Equal(updateUserType.UserNameType, result.UserNameType);

        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenUserTypeDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int userTypeId = 1;

            UserType updateUserType = new()
            {
                UsertypeID = userTypeId,
                UserNameType = "Employee",
                Id = 1
            };

            //Act
            var result = await userTypeRepository.UpdateUserType(userTypeId, updateUserType);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedUserType_WhenUserTypeIsDeleted()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int userTypeId = 1;

            UserType newUserType = new()
            {
                UsertypeID = userTypeId,
                UserNameType = "Admin",
                Id = 1
            };

            context.UserType.Add(newUserType);
            await context.SaveChangesAsync();

            //Act
            var result = await userTypeRepository.DeleteUserType(userTypeId);
            var category = await userTypeRepository.GetUserTypeById(userTypeId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserType>(result);
            Assert.Equal(userTypeId, result.UsertypeID);

            Assert.Null(category);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenUserTypeDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await userTypeRepository.DeleteUserType(1);

            //Assert
            Assert.Null(result);
        }
    }
}
