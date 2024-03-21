using BlazorSchoolApi.Data;

namespace BlazorSchoolApi.Interfaces;

public interface IUserService
{
    Task<string?> CreateUser(ApplicationUser user, string roleName);
    Task<string?> GetUserEmail(string userId);
    Task<ApplicationUser> GetUserById(string userId);
}