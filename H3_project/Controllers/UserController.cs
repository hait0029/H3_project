namespace H3_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository;

        public UserController(IUserRepository temp)
        {
            userRepository = temp;
        }

        //(Read)
        //Get: api/User
        [HttpGet ]
        public async Task<ActionResult> GetAllUser()
        {
            try
            {
                var user = await userRepository.GetUser();

                if (user == null)
                {
                    return Problem("Nothing was returned from user, this is unexpected");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //(Read)
        //Get: api/User/Id
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await userRepository.GetUserById(userId);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //(Update)
        //Put: api/user/Id
        [HttpPut("{userId}")]
        public async Task<ActionResult> PutUser(User user, int userId)
        {
            try
            {
                var userResult = await userRepository.UpdateUser(userId, user);

                if (user == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(user);
        }


        //(Create)
        //Post: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                var createUser = await userRepository.CreateUser(user);

                if (createUser == null)
                {
                    return StatusCode(500, "User was not created. Something failed...");
                }
                return CreatedAtAction("PostUser", new { userId = createUser.UserID }, createUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the UserType {ex.Message}");
            }
        }

        //(Delete)
        //Delete: api/User/Id
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            try
            {
                var user = await userRepository.DeleteUser(userId);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}