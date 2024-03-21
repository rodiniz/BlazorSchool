namespace BlazorSchoolApi.Interfaces;

public interface IUserService
{
    Task<string?> CreateUser(string? email, string? userName, string roleName);
    Task<string?> GetUserEmail(string userId);
}