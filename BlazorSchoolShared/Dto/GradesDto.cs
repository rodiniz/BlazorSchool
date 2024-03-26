
namespace BlazorSchoolApi.Data
{
    public class GradesDto 
    {
        public int Id { get; set; }
        public int StudentTestId { get; set; }
        public string? StudentId { get; set; }
        public int StudentName{ get; set; }
        public string? TestName { get; set; }
        public string? Score { get; set; }  
    }
}
