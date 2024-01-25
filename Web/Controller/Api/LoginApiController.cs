using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller.Api
{
    [Route("api/login")]
    [ApiController]
    public class LoginApiController
    {
        private readonly IJwtService _jwtService;

        public LoginApiController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }
        
        [HttpPost]
        public ActionResult<string> Login([FromBody] UserLoginDto userLoginDto)
        {
            return _jwtService.GenerateToken(userLoginDto);
        }
    }
}