using MudBlazor;

namespace BlazorSchool.Pages.Teachers;

public partial class SaveTeacher
{
    public SaveTeacher() : base("Teacher")
    {
    }
    private async Task SubmitValidForm()
    {
        if(await Save()){
            _Manager.NavigateTo("/Teachers/List");
        }
        else{
            Snackbar.Add("Error saving  teacher", Severity.Error);
        }
    }
}