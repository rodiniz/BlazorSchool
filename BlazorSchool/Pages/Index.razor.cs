using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared;

namespace BlazorSchool.Pages
{
    public partial class Index
    {

        private readonly LoginModel _loginModel = new LoginModel();
        private async Task HandleRegistration()
        {
            var result = await AuthService.Login(_loginModel);

            if (result.Successful)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Snackbar.Add(result.Error, Severity.Error);
            }
        }
    }
}
