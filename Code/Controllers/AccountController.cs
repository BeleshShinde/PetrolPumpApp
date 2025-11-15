using PetrolPumpApp.Helpers;
using PetrolPumpApp.Models;
using System.Web.Http;

namespace PetrolPumpApp.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        // Hardcoded credentials for testing (as per requirement)
        private const string ValidUsername = "admin";
        private const string ValidPassword = "admin123";

        /// <summary>
        /// Login endpoint - Validates credentials and returns JWT token
        /// POST: api/account/login
        /// </summary>
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Username and password are required");
            }

            // Validate credentials
            if (model.Username == ValidUsername && model.Password == ValidPassword)
            {
                // Generate JWT token
                string token = JwtTokenHelper.GenerateToken(model.Username);

                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token
                });
            }

            return Ok(new LoginResponse
            {
                Success = false,
                Message = "Invalid username or password",
                Token = null
            });
        }

        /// <summary>
        /// Test endpoint to verify authentication
        /// GET: api/account/test
        /// </summary>
        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            return Ok(new { message = "API is working!" });
        }
    }
}
