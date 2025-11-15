using PetrolPumpApp.Helpers;
using PetrolPumpApp.Models;
using System;
using System.Web.Http;

namespace PetrolPumpApp.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private const string ValidUsername = "admin";
        private const string ValidPassword = "admin123";

        
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Username and password are required");
            }

            if (model.Username == ValidUsername && model.Password == ValidPassword)
            {
                string token = JwtTokenHelper.GenerateToken(model.Username);

                System.Diagnostics.Debug.WriteLine($"Generated token length: {token.Length}");
                System.Diagnostics.Debug.WriteLine($"Token: {token.Substring(0, Math.Min(100, token.Length))}...");

                return Ok(new
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token
                });
            }

            return Ok(new
            {
                Success = false,
                Message = "Invalid username or password",
                Token = (string)null
            });
        }

        
        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            return Ok(new { message = "API is working!" });
        }
    }
}
