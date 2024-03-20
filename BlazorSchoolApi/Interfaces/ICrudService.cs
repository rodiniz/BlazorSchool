using BlazorSchoolShared;

namespace BlazorSchoolApi.Interfaces
{
    public interface ICrudService<T,Key>
    {
        Task<IResult> Get(Key id);

        Task<IResult> Create(T model);

        Task<IResult> Update(Key id,T model);

        Task<IResult> Delete(Key idEntity);

        Task<IResult> GetAll();

        Task<IResult> GetPaged(TableStateDto tableStateDto);
    }
}
