using H3_project.Database;
using Microsoft.EntityFrameworkCore;

namespace H3_project.Repositories
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private DatabaseContext Context { get; set; }
        public UserTypeRepository(DatabaseContext context)
        {
            Context = context;
        }
        public async Task<UserType> CreateUserType(UserType newuserType)
        {
            Context.UserType.Add(newuserType);
            await Context.SaveChangesAsync();
            return newuserType;
        }

        public async Task<UserType> DeleteUserType(int userTypeId)
        {
            UserType userType = await GetUserTypeById(userTypeId);
            if (userType != null)
            {
                Context.UserType.Remove(userType);
                await Context.SaveChangesAsync();
            }
            return userType;
        }

        public async Task<List<UserType>> GetUserType()
        {
            return await Context.UserType.ToListAsync();
        }

        public async Task<UserType> GetUserTypeById(int userTypeId)
        {
            return await Context.UserType.FirstOrDefaultAsync(e => e.UsertypeID == userTypeId);
        }

        public async Task<UserType> UpdateUserType(int userTypeId, UserType updateUserType)
        {
            UserType userType = await GetUserTypeById(userTypeId);
            if (userType != null && updateUserType != null)
            {
                userType.UserNameType = updateUserType.UserNameType;
                userType.Id = updateUserType.Id;

                await Context.SaveChangesAsync();
            }
            return userType;
        }
    }
}
