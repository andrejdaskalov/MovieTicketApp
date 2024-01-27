using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller.Api
{
    [Route("api/register")]
    [ApiController]
    public class RegisterApiController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterApiController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto request)
        {
            var userCheck = await _userManager.FindByEmailAsync(request.Email);
            if (userCheck == null)
            {
                var user = new IdentityUser
                {
                    UserName = request.Email,
                    NormalizedUserName = request.Email,
                    Email = request.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    return new OkResult();
                }

                if (result.Errors.Any())
                {
                    return new BadRequestResult();
                }
            }

            return new BadRequestResult();
        }
    }
}