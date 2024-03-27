using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using BlazorSchoolShared.Dto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Services;

public class CourseCycleService : ICrudService<CourseCycleDto, int>
{
    private readonly SchoolContext _context;
    private readonly IValidator<CourseCycleDto> _validator;
    public CourseCycleService(SchoolContext schoolContext,
        IValidator<CourseCycleDto> validator)
    {
        _context = schoolContext;
        _validator = validator;
    }

    public async Task<IResult> Get(int id)
    {
        var courseCycle = await (from cc in _context.CourseCycles
                                 join ro in _context.Users on cc.TeacherId equals ro.Id
                                 select new CourseCycleDto
                                 {
                                     Id = cc.Id,
                                     CourseId = cc.Course.Id,
                                     Year = cc.Year,
                                     CourseName = cc.Course.Description,
                                     TeacherId = cc.TeacherId,
                                     TeacherName = ro.Name
                                 }).SingleOrDefaultAsync(c => c.Id == id);
        return courseCycle == null ?
            TypedResults.NotFound() : TypedResults.Ok(courseCycle);
    }

    public async Task<IResult> Create(CourseCycleDto model)
    {
        var result = await _validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            return TypedResults.BadRequest(result.Errors);
        }

        var courseCycle = new CourseCycle
        {
            TeacherId = model.TeacherId,
            Course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == model.CourseId),
            Year = model.Year.Value
        };
        await _context.CourseCycles.AddAsync(courseCycle);

        await _context.SaveChangesAsync();
        return TypedResults.Created();
    }

    public async Task<IResult> Update(int id, CourseCycleDto model)
    {
        var result = await _validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            return TypedResults.BadRequest(result.Errors);
        }

        try
        {
            var affected = await _context.CourseCycles
                 .Where(b => b.Id == id)
                 .ExecuteUpdateAsync(setters => setters
                     .SetProperty(b => b.TeacherId, model.TeacherId)
                     .SetProperty(b => b.Year, model.Year)
                     .SetProperty(b => b.CourseId, model.CourseId)
                 );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.ToString());
        }

    }

    public async Task<IResult> Delete(int idEntity)
    {
        var affected = await _context.CourseCycles.Where(c => c.Id == idEntity).ExecuteDeleteAsync();
        return affected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
    }

    public Task<IResult> GetAll()
    {
        var dto = (from cc in _context.CourseCycles
                   join course in _context.Courses on cc.Course.Id equals course.Id
                   join ro in _context.Users on cc.TeacherId equals ro.Id
                   select new CourseCycleDto
                   {
                       Id = cc.Id,
                       CourseId = course.Id,
                       Year = cc.Year,
                       CourseName = course.Description,
                       TeacherId = cc.TeacherId,
                       TeacherName = ro.Name
                   }).ToList();
        return Task.FromResult<IResult>(TypedResults.Ok(dto));
    }

    public Task<IResult> GetPaged(TableStateDto tableStateDto)
    {
        throw new NotImplementedException();
    }
}