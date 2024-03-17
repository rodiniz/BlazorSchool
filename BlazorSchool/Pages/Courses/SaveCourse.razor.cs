using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorSchool.Pages.Courses
{
    
    public partial class SaveCourse
    {
        public SaveCourse():base("Course")
        {
            
        }       
         

       private async Task SubmitValidForm()
        {
             var sucess=await  Save();
             if(sucess){
                _Manager.NavigateTo("/Course/List");
             }
            else{
                Snackbar.Add("Error saving  Course", Severity.Error);
            }
            
        }
            
    }
}
