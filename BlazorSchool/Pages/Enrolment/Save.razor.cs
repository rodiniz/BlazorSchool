using MudBlazor;

namespace BlazorSchool.Pages.Enrolment;

public partial class Save
{
    public Save() : base("Enrolment")
    {
    }
    private async Task SubmitValidForm()
    {
        var success=await  Save();
        if(success){
            Manager!.NavigateTo($"/{Url}/List");
        }
        else{
            Snackbar.Add("Error saving Enrolment", Severity.Error);
        }
            
    }
}