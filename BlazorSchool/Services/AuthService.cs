using Blazored.LocalStorage;
using BlazorSchool.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BlazorSchool.Services
{

    public class AuthService
        : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, 
            AuthenticationStateProvider authenticationStateProvider, 
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResultModel> Register(LoginModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("identity/register",
                new RegisterModel { Email = registerModel.Email, Password = registerModel.Password });
            if (!result.IsSuccessStatusCode)
            {
                var error = await result.Content.ReadFromJsonAsync<RegisterResultModel>();
                var rawError = await result.Content.ReadAsStringAsync();
                error.errors.additionalProp1 = new[] { rawError };
                return error;
            }

            return (await result.Content.ReadFromJsonAsync<RegisterResultModel>())!;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("identity/Login", loginModel);

            if (!response.IsSuccessStatusCode)
            {
                return new LoginResult { Successful = false, Error = "Invalid user name password" };
            }

            var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
            await _localStorage.SetItemAsync("authToken", loginResult.AccessToken);
            await _localStorage.SetItemAsync("email", loginModel.Email);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResult.AccessToken);
            loginResult.Successful = true;
            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("email");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

}