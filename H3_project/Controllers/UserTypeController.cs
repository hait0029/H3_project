namespace H3_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private IUserTypeRepository userTypeRepository;

        public UserTypeController(IUserTypeRepository temp)
        {
            userTypeRepository = temp;
        }

        // GetAll method:
        [HttpGet]
        public async Task<ActionResult> GetAllUserType()
        {
            try
            {
                var userType = await userTypeRepository.GetUserType();

                if (userType == null)
                {
                    return Problem("Nothing was returned from userType, this is unexpected");
                }

                return Ok(userType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Get by id Method
        [HttpGet("{userTypeId}")]

        public async Task<ActionResult> GetUserTypesById(int userTypeId)
        {
            try
            {
                var userType = await userTypeRepository.GetUserTypeById(userTypeId);

                if (userType == null)
                {
                    return NotFound();
                }
                return Ok(userType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Update Method
        [HttpPut("{userTypeId}")]

        public async Task<ActionResult> PutUserType(UserType userType, int userTypeId)
        {
            try
            {
                var userTypeResult = await userTypeRepository.UpdateUserType(userTypeId, userType);

                if (userType == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(userType);
        }

        // Create Method
        [HttpPost]
        public async Task<ActionResult<UserType>> PostUserTypes(UserType userType)
        {
            try
            {
                var createUserType = await userTypeRepository.CreateUserType(userType);

                if (createUserType == null)
                {
                    return StatusCode(500, "UserType was not created. Something failed...");
                }
                return CreatedAtAction("PostUserTypes", new { userTypeId = createUserType.UsertypeID }, createUserType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the UserType {ex.Message}");
            }
        }




        // Delete Method
        [HttpDelete("{userTypeId}")]

        public async Task<ActionResult> DeleteUserTypes(int userTypeId)
        {
            try
            {
                var userType = await userTypeRepository.DeleteUserType(userTypeId);

                if (userType == null)
                {
                    return NotFound();
                }
                return Ok(userType);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
