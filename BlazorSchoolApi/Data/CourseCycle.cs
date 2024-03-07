namespace BlazorSchoolApi.Data
{
    public class CourseCycle
    {
        public int Id { get; set; }
        public virtual Course Course { get; set; }
        public string TeacherId { get; set; }
        public int Year { get; set; }
    }   
}
