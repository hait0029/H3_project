namespace ProjectUnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private DbContextOptions<DatabaseContext> options; //server
        private DatabaseContext context; //udsignet på serveren
        private UserRepository userRepository;

        public UserRepositoryTests()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
           .UseInMemoryDatabase(databaseName: "UserRepository")
           .Options;

            context = new DatabaseContext(options);

            userRepository = new(context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfUsers_WhenUsersExists()
        {
            //Arrange
            //create data
            await context.Database.EnsureDeletedAsync();

            context.User.Add(new User
            {
                UserID = 1,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341
            });
            context.User.Add(new User
            {
                UserID = 2,
                FName = "Ron",
                LName = "spencer",
                Address = "wefVej 2",
                Phone = 122311121
            });
            
            context.SaveChanges();

            //Act
            //call method
            var result = await userRepository.GetUser();

            //Assert
            //confirm result
            //(expected, result)
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfUsers_WhenNoUsersExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();
            //Act
            var result = await userRepository.GetUser();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Empty(result);
        }


        [Fact]
        public async void GetById_ShouldReturnUsers_WhenUsersExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int userId = 1;

            context.User.Add(new()
            {
                UserID = 1,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341
            });

            await context.SaveChangesAsync();


            //Act
            var result = await userRepository.GetUserById(userId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.UserID);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenUsersDeosNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await userRepository.GetUserById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldAddNewIdToUser_WhenSavingToDatabase()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            User user = new()
            {
                UserID = expectedNewId,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341
            };

            //Act
            var result = await userRepository.CreateUser(user);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedNewId, result.UserID);
        }

        [Fact]
        public async void Create_ShouldFailToAddNewIdToUsers_WhenUserIdAlreadyExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            User user = new()
            {
                UserID = 1,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341
            };

            await userRepository.CreateUser(user);

            //Act
            async Task action() => await userRepository.CreateUser(user);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnProduct_WhenProductExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int userId = 1;

            User newUser = new()
            {
                UserID = userId,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341

            };
            context.User.Add(newUser);
            await context.SaveChangesAsync();
            User updateUser = new()
            {
                UserID = userId,
                FName = "jeff",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 123413

            };
            var result = await userRepository.UpdateUser(userId, updateUser);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.UserID);
            
            

        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenUserDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int userId = 1;

            User updateUser = new()
            {
                UserID = userId,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341

            };

            //Act
            var result = await userRepository.UpdateUser(userId, updateUser);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedUser_WhenUserIsDeleted()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            int userId = 1;

            User newUser = new()
            {
                UserID = userId,
                FName = "Ben",
                LName = "jack",
                Address = "HejVej 2",
                Phone = 12341

            };

            context.User.Add(newUser);
            await context.SaveChangesAsync();

            //Act
            var result = await userRepository.DeleteUser(userId);
            var user = await userRepository.GetUserById(userId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.UserID);

            Assert.Null(user);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenUserDoesNotExists()
        {
            //Arrange
            await context.Database.EnsureDeletedAsync();

            //Act
            var result = await userRepository.DeleteUser(1);

            //Assert
            Assert.Null(result);
        }
    }
}
