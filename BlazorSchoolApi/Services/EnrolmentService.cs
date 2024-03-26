using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using BlazorSchoolShared.Dto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Services;

public class EnrolmentService: ICrudService<EnrolmentDto,int>
{
    private readonly SchoolContext _context;
    private readonly IValidator<EnrolmentDto> _validator;

    public EnrolmentService(SchoolContext context, IValidator<EnrolmentDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IResult> Get(int id)
    {
        var courseCycle = await (from en in _context.Enrollments
            join cc in _context.CourseCycles on en.CourseCycle.Id equals cc.Id
            join student in _context.Users on  en.StudentId equals student.Id
            select new EnrolmentDto
            {
                Id = cc.Id,
                CourseCycleId = cc.Id,
                StudentId = en.StudentId,
                CourseCycleName = cc.Course.Description + cc.Year,
                StudentName = student.Name
            }).SingleOrDefaultAsync(c => c.Id == id);
        return courseCycle == null ? 
            TypedResults.NotFound() : TypedResults.Ok(courseCycle);
    }

    public async Task<IResult> Create(EnrolmentDto model)
    {
        var result = await _validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            return TypedResults.BadRequest(result.Errors);
        }

        var cc = await _context.CourseCycles.SingleOrDefaultAsync(c => c.Id == model.CourseCycleId);
        if (cc is null)
        {
            return TypedResults.BadRequest("Invalid course cycle");
        }
        await _context.Enrollments.AddAsync(
            new Enrollment
            {
               CourseCycle =cc,
               StudentId = model.StudentId
            });
        await _context.SaveChangesAsync();
        return TypedResults.Created();
    }

    public async Task<IResult> Update(int id, EnrolmentDto model)
    {
        var result = await _validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            return TypedResults.BadRequest(result.Errors);
        }
        var cc = await _context.CourseCycles.SingleOrDefaultAsync(c => c.Id == model.CourseCycleId);
        await _context.Enrollments
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(b => b.CourseCycle,cc)
                .SetProperty(b => b.StudentId, model.StudentId)
            );
        return TypedResults.Ok();
    }

    public async Task<IResult> Delete(int idEntity)
    {
        var affected = await _context.Enrollments.Where(c => c.Id == idEntity).ExecuteDeleteAsync();
        return affected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
    }

    public async Task<IResult> GetAll()
    {
        var courseCycle = await (from en in _context.Enrollments
            join cc in _context.CourseCycles on en.CourseCycle.Id equals cc.Id
            join student in _context.Users on  en.StudentId equals student.Id
            select new EnrolmentDto
            {
                Id = cc.Id,
                CourseCycleId = cc.Id,
                StudentId = en.StudentId,
                CourseCycleName = cc.Course.Description + cc.Year,
                StudentName = student.Name
            }).ToListAsync();
        return TypedResults.Ok(courseCycle);
    }

    public Task<IResult> GetPaged(TableStateDto tableStateDto)
    {
        throw new NotImplementedException();
    }
}