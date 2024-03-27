using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorSchool.Pages
{
    public class BaseListPage : ComponentBase
    {
        [Inject] protected HttpClient? HttpClient { get; set; }
        [Inject] protected IDialogService? DialogService { get; set; }

        [Inject] protected NavigationManager? Manager { get; set; }
    }
}
