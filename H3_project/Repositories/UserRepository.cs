using H3_project.Models;

namespace H3_project.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext Context { get; set; }
        public UserRepository(DatabaseContext context)
        {
            Context = context;
        }
        public async Task<User> CreateUser(User newUser)
        {
            if (newUser.LoginId.HasValue)
            {
                newUser.login = await Context.Login.FirstOrDefaultAsync(e => e.LoginID == newUser.LoginId);
            }

            Context.User.Add(newUser);
            await Context.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> DeleteUser(int userId)
        {
            User user = await GetUserById(userId);
            if (user != null)
            {
                Context.User.Remove(user);
                await Context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<List<User>> GetUser()
        {
            return await Context.User.Include(e => e.login).ThenInclude(e => e.userType).ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await Context.User.Include(e => e.login).ThenInclude(e => e.userType).FirstOrDefaultAsync(e => e.UserID == userId);
        }

        public async Task<User> UpdateUser(int userId, User updateUser)
        {

            User user = await GetUserById(userId);

            if (user != null)
            {
                user.FName = updateUser.FName;
                user.LName = updateUser.LName;
                user.Address = updateUser.Address;
                user.Phone = updateUser.Phone;

                if (updateUser.login != null)
                {
                    user.login = await Context.Login.FirstOrDefaultAsync(e => e.LoginID == updateUser.login.LoginID);
                }
                else
                {
                    user.login = null; // Clear the UserType if null is provided
                }

                Context.Entry(user).State = EntityState.Modified;


                await Context.SaveChangesAsync();
                return await GetUserById(userId);
            }
            return null;
        }
    }
}
