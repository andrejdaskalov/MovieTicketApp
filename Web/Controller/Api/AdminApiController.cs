using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller.Api
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        // GET
        [HttpGet]
        public List<UserRoleDto> Index()
        {
            var users = _userService.GetAllUsers();
            return users.Select(u => 
                new UserRoleDto{Username = u.UserName, Role = _userService.GetUserRole(u)}).ToList();
            
        }
        
        [HttpPost("makeadmin")]
        public async Task<IActionResult> MakeAdmin([FromBody] string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user != null)
            {
                await _userService.MakeUserAdmin(user);
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpPost("removeadmin")]
        public async Task<IActionResult> RemoveAdmin([FromBody] string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user != null)
            {
                await _userService.RemoveUserAdmin(user);
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpPost("import")]
        public async Task<IActionResult> ImportUsers(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var path = Path.GetTempFileName() + ".xlsx";

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var users = _userService.GetUsersFromXlsFile(path);

            users.ForEach(async u =>
            {
                await _userService.CreateUser(u.Email, u.Password);
            });

            return Ok();
        }
    }
    
    
}