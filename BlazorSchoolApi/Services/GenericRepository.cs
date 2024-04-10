using BlazorSchoolApi.Data;
using FluentValidation;

namespace BlazorSchoolApi.Services;

public class GenericRepository<T>
{
    private readonly SchoolContext _context;
    private readonly IValidator<T> _validator;
    public GenericRepository(
        SchoolContext context, 
        IValidator<T> validator)
    {
        _context = context;
        _validator = validator;
    }
    public async Task<IResult> Create(T model)
    {
        var result = await _validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            return TypedResults.BadRequest(result.Errors);
        }

        var entity = model.ToEntity();
        await _context.CourseCycles.AddAsync(entity);
        return TypedResults.Ok(entity);
    }
}