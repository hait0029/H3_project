using H3_project.Interfaces;
using H3_project.Models;
using H3_project.Repositories;

namespace H3_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginRepository loginRepository;

        public LoginController(ILoginRepository temp)
        {
            loginRepository = temp;
        }

        //(Read)
        //Get: api/Login
        [HttpGet]
        public async Task<ActionResult> GetAllLogin()
        {
            try
            {
                var login = await loginRepository.GetLogin();

                if (login == null)
                {
                    return Problem("Nothing was returned from login, this is unexpected");
                }
                
                return Ok(login);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //(Read)
        //Get: api/Login/Id
        [HttpGet("{loginId}")]
        public async Task<ActionResult> GetLoginById(int loginId)
        {
            try
            {
                var login = await loginRepository.GetLoginById(loginId);

                if (login == null)
                {
                    return NotFound();
                }
                return Ok(login);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //(Update)
        //Put: api/Login/Id
        [HttpPut("{loginId}")]
        public async Task<ActionResult> PutLogin(Login login, int loginId)
        {
            try
            {
                var loginResult = await loginRepository.UpdateLogin(loginId, login);

                if (login == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(login);
        }

        //(Create)
        //Post: api/Login
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login login)
        {
            try
            {
                var createdLogin = await loginRepository.CreateLogin(login);

                if (createdLogin == null)
                {
                    return StatusCode(500, "Login was not created. Something failed...");
                }
                return CreatedAtAction("PostLogin", new { loginId = createdLogin.LoginID }, createdLogin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the Login: {ex.Message}");
            }
        }

        //(Delete)
        //Delete: api/Login/Id
        [HttpDelete("{loginId}")]
        public async Task<ActionResult> DeleteLogin(int loginId)
        {
            try
            {
                var login = await loginRepository.DeleteLogin(loginId);

                if (login == null)
                {
                    return NotFound();
                }
                return Ok(login);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
