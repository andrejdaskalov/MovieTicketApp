using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Service
{
    public interface IUserService
    {
       public IdentityUser GetUser(string id);
       public IdentityUser GetUserByUsername(string username);
       public Task<IdentityResult> CreateUser(string username, string password);
       public Task<IdentityUser> MakeUserAdmin(IdentityUser user);
       public Task<IdentityUser> RemoveUserAdmin(IdentityUser user);
       public IEnumerable<IdentityUser> GetAllUsers();
       public string GetUserRole(IdentityUser user);
       // public IdentityUser GetUsersFromXlsFile(string path);
    }
}