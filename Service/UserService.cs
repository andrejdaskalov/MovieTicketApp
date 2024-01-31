using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Word;
using Domain.DTO;
using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Identity;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IdentityUser GetUser(string id)
        {
            return _userManager.Users.First(u => u.Id == id);
        }

        public IdentityUser GetUserByUsername(string username)
        {
            return _userManager.Users.First(u => u.UserName == username);
        }

        public async Task<IdentityResult> CreateUser(string username, string password)
        {
            return await _userManager.CreateAsync(new IdentityUser
            {
                UserName = username,
                Email = username
            }, password);
        }

        public async Task<IdentityUser> MakeUserAdmin(IdentityUser user)
        {
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }

        public async Task<IdentityUser> RemoveUserAdmin(IdentityUser user)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }
        
        public string GetUserRole(IdentityUser user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            if (roles.Count > 0)
            {
                return roles[0];
            }
            return "User";
        }

        public List<UserRegistrationDto> GetUsersFromXlsFile(string path)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            var users = new List<UserRegistrationDto>();

            var workbook = ExcelFile.Load(path);
            var worksheet = workbook.Worksheets[0];

            for (int i = 1, rowCount = worksheet.Rows.Count; i < rowCount; i++)
            {
                var row = worksheet.Rows[i];

                var user = new UserRegistrationDto
                {
                    Email = row.Cells[0].Value?.ToString(),
                    Password = row.Cells[1].Value?.ToString(),
                    ConfirmPassword = row.Cells[1].Value?.ToString(),
                };

                users.Add(user);
            }

            return users;
        }
    }
}