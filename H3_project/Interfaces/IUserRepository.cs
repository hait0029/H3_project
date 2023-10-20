namespace H3_project.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUser();
        public Task<User> GetUserById(int userId);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(int userId, User user);
        public Task<User> DeleteUser(int userId);
    }
}
