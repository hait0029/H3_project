namespace H3_project.Interfaces
{
    public interface IUserTypeRepository
    {
        public Task<List<UserType>> GetUserType();
        public Task<UserType> GetUserTypeById(int userTypeId);
        public Task<UserType> CreateUserType(UserType userType);
        public Task<UserType> UpdateUserType(int userTypeId, UserType userType);
        public Task<UserType> DeleteUserType(int userTypeId);
    }
}
