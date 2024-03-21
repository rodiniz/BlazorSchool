

using MudBlazor;

namespace BlazorSchool.Pages.Users
{
    
    public partial class SaveUser
    {
        public SaveUser():base("Users")
        {
            
        }
        private async Task SubmitValidForm()
        {
           if(await Save()){
            _Manager.NavigateTo("/Users/List");
           }
            else{
                Snackbar.Add("Error saving  student", Severity.Error);
            }
        }
            
    }
}
