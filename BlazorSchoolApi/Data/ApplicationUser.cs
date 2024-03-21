using Microsoft.AspNetCore.Identity;

namespace BlazorSchoolApi.Data;

public class ApplicationUser:IdentityUser
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Address { get; set; }
}