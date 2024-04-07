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
        var courseCycle = await (from cc in _context.CourseCycles.AsNoTracking()
                                    .Include(c=>c.CourseTeachers)
                                 select new CourseCycleDto
                                 {
                                     Id = cc.Id,
                                     Year = cc.Year,
                                     CourseTeachers = cc.CourseTeachers.Select(c => new CourseTeacherDto
                                     {
                                         CourseId = c.CourseId,
                                         CourseName = c.Course.Description, 
                                         Id = c.Id,
                                         TeacherId = c.TeacherId,
                                         TeacherName = c.Teacher.Name

                                     }).ToList()

                                 }).SingleOrDefaultAsync(c => c.Year == id);
        return courseCycle == null ?
            TypedResults.Ok() : TypedResults.Ok(courseCycle);
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
            Year = model.Year.Value,
            CourseTeachers = model.CourseTeachers
                .Select(c => new CourseTeacher
                {
                    CourseId = c.CourseId.Value,
                    TeacherId = c.TeacherId
                })
                .ToList()
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
            var courseCycle = await _context.CourseCycles.SingleOrDefaultAsync(c => c.Id == id);
            if (courseCycle == null)
            {
                return TypedResults.NotFound();
            }
            courseCycle.Year = model.Year.Value;

            courseCycle.CourseTeachers = model.CourseTeachers
                .Select(c => new CourseTeacher
                {
                    CourseId = c.CourseId.Value,
                    TeacherId = c.TeacherId
                })
                .ToList();


            await _context.SaveChangesAsync();
            return TypedResults.Ok();
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
                   select new CourseCycleDto
                   {
                       Id = cc.Id,
                       Year = cc.Year

                   }).ToList();
        return Task.FromResult<IResult>(TypedResults.Ok(dto));
    }

    public Task<IResult> GetPaged(TableStateDto tableStateDto)
    {
        throw new NotImplementedException();
    }
}