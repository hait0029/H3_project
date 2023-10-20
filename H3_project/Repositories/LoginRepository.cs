namespace H3_project.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private DatabaseContext Context { get; set; }
        public LoginRepository(DatabaseContext context)
        {
            Context = context;
        }
        public async Task<Login> CreateLogin(Login newLogin)
        {
            var existingLogin = await Context.Login.FirstOrDefaultAsync(e => e.Email == newLogin.Email);

            if (existingLogin != null)
            {
                throw new ArgumentException("Login already exists", nameof(newLogin.Email));
            }

            if (newLogin.UserTypeId.HasValue)
            {
                newLogin.userType = await Context.UserType.FirstOrDefaultAsync(e => e.UsertypeID == newLogin.UserTypeId);
            }

            Context.Login.Add(newLogin);
            await Context.SaveChangesAsync();

            return newLogin;

            //newLogin.userType = await Context.UserType.FirstOrDefaultAsync(e => e.UsertypeID == newLogin.LoginID);
            ////lave et nyt object af login, newobj.email == newlogin.email
            //Context.Login.Add(newLogin);
            //await Context.SaveChangesAsync();
            //return newLogin;

            //if (newLogin.UserTypeId.HasValue)
            //{
            //    newLogin.userType = await Context.UserType.FirstOrDefaultAsync(e => e.UsertypeID == newLogin.UserTypeId);
            //}
            //Context.Login.Add(newLogin);
            //await Context.SaveChangesAsync();

            //return newLogin;


        }

        public async Task<Login> DeleteLogin(int loginId)
        {
            Login login = await GetLoginById(loginId);
            if (login != null)
            {
                Context.Login.Remove(login);
                await Context.SaveChangesAsync();
            }
            return login;
        }

        public async Task<List<Login>> GetLogin()
        {
            //return await context.Login.ToListAsync();
            return await Context.Login.Include(e => e.userType).ToListAsync();
        }

        public async Task<Login> GetLoginById(int loginId)
        {
            return await Context.Login.Include(e => e.userType).FirstOrDefaultAsync(e => e.LoginID == loginId);
        }

        public async Task<Login> UpdateLogin(int loginId, Login updateLogin)
        {
            Login login = await GetLoginById(loginId);

            if (login != null)
            {
                login.Email = updateLogin.Email;
                login.Password = !string.IsNullOrEmpty(updateLogin.Password) ? updateLogin.Password : login.Password;

                if (updateLogin.userType != null)
                {
                    login.userType = await Context.UserType.FirstOrDefaultAsync(e => e.UsertypeID == updateLogin.userType.UsertypeID);
                }
                else
                {
                    login.userType = null; // Clear the UserType if null is provided
                }

                Context.Entry(login).State = EntityState.Modified;


                await Context.SaveChangesAsync();
                return await GetLoginById(loginId);
            }
            return null;
        }

        //Authentication is the process of verifying the identity of a user, typically based on their provided credentials, like email and password.
        public async Task<Login> Logins(Login login)
        {
            return await Context.Login.Include(e => e.User).FirstOrDefaultAsync(e => e.Email == login.Email && e.Password == login.Password);
        }

        //The Register method is used to create a new user account and associated login credentials
        public async Task<Login> Register(User newUser, Login newLogin)
        {
            Login? existingLogin = await Context.Login.FirstOrDefaultAsync(e => e.Email == newLogin.Email);

            if (existingLogin != null)
            {
                Console.WriteLine($"Registered user with email: {existingLogin.Email}");
            }
            else
            {
                Console.WriteLine("Registration failed - email already exists");
            }

            newUser.LoginId = newLogin.LoginID;
            Context.User.Add(newUser);
            await Context.SaveChangesAsync();

            return newLogin;
        }
    }
}
