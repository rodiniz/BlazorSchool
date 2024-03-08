using BlazorSchoolShared;

namespace BlazorSchoolApi.Interfaces
{
    public interface ICrudService<T>
    {
        Task<IResult> Get(int id);

        Task<IResult> Create(T model);

        Task<IResult> Update(int id,T model);

        Task<IResult> Delete(int idEntity);

        Task<IResult> GetAll();

        Task<IResult> GetPaged(TableStateDto tableStateDto);
    }
}
