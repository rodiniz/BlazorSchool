using BlazorSchoolApi.Interfaces;

namespace BlazorSchoolApi.Data
{
    public class Course: IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }

    }
}
