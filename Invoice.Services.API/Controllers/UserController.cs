using Invoice.Applicaion.Interfaces;
using Invoice.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        private string generatedToken = string.Empty;
        public UserController(ITokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserModel userModel)
        {
            if(userModel.UserName == "username" && userModel.Password == "password")
            {
                generatedToken = _tokenService.BuildToken(_configuration["Jwt:key"], _configuration["Jwt:Issuer"], userModel);
                return Ok(new { token = generatedToken });
            }
            return Unauthorized();
        }
    }
}
