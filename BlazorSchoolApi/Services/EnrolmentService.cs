using BlazorSchoolApi.Data;
using BlazorSchoolApi.Interfaces;
using BlazorSchoolShared;
using BlazorSchoolShared.Dto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApi.Services;

public class EnrolmentService : ICrudService<EnrolmentDto, int>
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
                                 select new EnrolmentDto
                                 {
                                     Id = en.Id,
                                     StudentId = en.Student.Id,
                                     StudentName = en.Student.Name
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

        //TODO
        await _context.Enrollments.AddAsync(
            new Enrollment
            {
                CourseCycle = _context.CourseCycles.SingleOrDefault(c => c.Id == model.CourseCycleId),
                Student = _context.Users.SingleOrDefault(c => c.Id == model.StudentId)

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
                .SetProperty(b => b.StudentId, model.StudentId)
                .SetProperty(b => b.CourseCycleId, model.CourseCycleId)
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

                                 select new EnrolmentDto
                                 {
                                     Id = en.Id,
                                     StudentId = en.Student.Id,
                                     StudentName = en.Student.Name
                                 }).ToListAsync();
        return TypedResults.Ok(courseCycle);
    }

    public Task<IResult> GetPaged(TableStateDto tableStateDto)
    {
        throw new NotImplementedException();
    }
}