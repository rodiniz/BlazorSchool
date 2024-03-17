

using MudBlazor;

namespace BlazorSchool.Pages.Students
{
    
    public partial class SaveStudent
    {
        public SaveStudent():base("Student")
        {
            
        }
        private async Task SubmitValidForm()
        {
           if(await Save()){
            _Manager.NavigateTo("/Students/List");
           }
            else{
                Snackbar.Add("Error saving  student", Severity.Error);
            }
        }
            
    }
}
