using Shared;

namespace BlazorSchool.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task<RegisterResultModel> Register(LoginModel loginModel);
        Task Logout();
    }
}
