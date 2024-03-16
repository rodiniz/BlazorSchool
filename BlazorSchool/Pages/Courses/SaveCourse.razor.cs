using Microsoft.AspNetCore.Components;

namespace BlazorSchool.Pages.Courses
{
    
    public partial class SaveCourse
    {
        public SaveCourse():base("Course")
        {
            
        }
        
        [Inject] 
        private  NavigationManager _Manager { get; set; }       

        private async Task SubmitValidForm()
        {
             var sucess=await  Save();
             if(sucess){
                _Manager.NavigateTo("/Course/List");
             }
             else{

             }
            
        }
            
    }
}
