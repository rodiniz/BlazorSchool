using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using BlazorSchoolShared.Dto;
using Microsoft.AspNetCore.Identity;

namespace BlazorSchoolApi.Services;


public class TeacherService:ICrudService<TeacherDto,string>
{
    private readonly IUserService _userService;
    private readonly UserManager<IdentityUser> _userManager;
    
    public TeacherService(
        UserManager<IdentityUser> userManager, 
        IUserService userService)
    {
        _userManager = userManager;
        _userService = userService;
    }

    public Task<IResult> Get(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> Create(TeacherDto model)
    {
        var id= await _userService.CreateUser(model.Email,model.Email,"Teacher");
        if (string.IsNullOrEmpty(id))
        {
            return TypedResults.BadRequest();
        }
        return TypedResults.Created();
    }

    public async Task<IResult> Update(string id, TeacherDto model)
    {
        var user = new IdentityUser
        {
            Id = id,
            UserName = model.Name,
            Email = model.Email
        };
        var result= await _userManager.UpdateAsync(user);
        return result.Succeeded ? TypedResults.Ok() : TypedResults.BadRequest(result.Errors);
    }

    public async Task<IResult> Delete(string idEntity)
    {
        var user = new IdentityUser
        {
            Id = idEntity
        };
        var result= await _userManager.DeleteAsync(user);
        return result.Succeeded ? TypedResults.Ok() : TypedResults.BadRequest(result.Errors);
    }

    public async Task<IResult> GetAll()
    {
        var users = await _userManager.GetUsersInRoleAsync("Teacher");
        return TypedResults.Ok(
            users.Select(c => 
                new TeacherDto { 
                    Id = c.Id, 
                    Email = c.Email, 
                    Name = c.UserName 
                }));
    }

    public Task<IResult> GetPaged(TableStateDto tableStateDto)
    {
        throw new NotImplementedException();
    }
}