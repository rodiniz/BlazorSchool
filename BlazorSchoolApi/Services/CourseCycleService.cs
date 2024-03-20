using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using BlazorSchoolShared.Dto;

namespace BlazorSchoolApi.Services;

public class CourseCycleService: ICrudService<CourseCycleDto,int>
{ 
    private readonly SchoolContext _schoolContext;
    
    public CourseCycleService(SchoolContext schoolContext)
    {
        _schoolContext = schoolContext;
    }
   
    public Task<IResult> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> Create(CourseCycleDto model)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> Update(int id, CourseCycleDto model)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> Delete(int idEntity)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> GetAll()
    {
        var dto = (from cc in _schoolContext.CourseCycles
            select new CourseCycleDto
            {
                Id = cc.Id, 
                CourseId = cc.Course.Id, 
                Year = cc.Year,
                CourseName = cc.Course.Description,
                TeacherId = cc.TeacherId
            }).ToList();
        return  Task.FromResult<IResult>(TypedResults.Ok(dto));
    }

    public Task<IResult> GetPaged(TableStateDto tableStateDto)
    {
        throw new NotImplementedException();
    }
}