using System.Collections.Generic;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        // GET
        public IActionResult Index()
        {
            UsernamesDto model = new UsernamesDto();
            model.UserRole = new Dictionary<string, string>();
            var users = _userService.GetAllUsers();
            foreach (var user in users)
            {
                model.UserRole.Add(user.UserName, _userService.GetUserRole(user));
            }
            return View(model);
        }
        
        [HttpPost]
        public IActionResult MakeAdmin(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user != null)
            {
                _userService.MakeUserAdmin(user);
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult RemoveAdmin(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user != null)
            {
                _userService.RemoveUserAdmin(user);
            }
            return RedirectToAction("Index");
        }
    }
}