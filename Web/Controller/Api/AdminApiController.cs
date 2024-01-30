using System.Collections.Generic;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
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
        public UsernamesDto Index()
        {
            UsernamesDto model = new UsernamesDto();
            model.UserRole = new Dictionary<string, string>();
            var users = _userService.GetAllUsers();
            foreach (var user in users)
            {
                model.UserRole.Add(user.UserName, _userService.GetUserRole(user));
            }
            return model;
        }
        
        [HttpPost]
        public IActionResult MakeAdmin([FromBody] string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user != null)
            {
                _userService.MakeUserAdmin(user);
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpDelete]
        public IActionResult RemoveAdmin([FromBody] string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user != null)
            {
                _userService.RemoveUserAdmin(user);
                return Ok();
            }
            return BadRequest();
        }
    }
}