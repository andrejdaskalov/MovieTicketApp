using System.Security.Claims;
using System.Threading.Tasks;
using Domain.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller.Api
{
    [Route("api/login")]
    [ApiController]
    public class LoginApiController
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LoginApiController(IJwtService jwtService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            // if (user != null && !user.EmailConfirmed)
            // {
            //     ModelState.AddModelError("message", "Email not confirmed yet");
            //     return View(model);
            //
            // }
            if (await _userManager.CheckPasswordAsync(user, userLoginDto.Password) == false)
            {
                return new UnauthorizedResult();
            }

            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password, true, true);

            if (result.Succeeded)
            {
                var jwtDto = new JwtDto
                {
                    Email = userLoginDto.Email,
                    Role = await _userManager.IsInRoleAsync(user, "Admin") ? "Admin" : "User"
                };
                return _jwtService.GenerateToken(jwtDto);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}