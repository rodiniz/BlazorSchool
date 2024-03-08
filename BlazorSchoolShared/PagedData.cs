namespace BlazorSchoolShared
{
    public class PagedData<T>
    {
        public int TotalCount { get; set; }
        public List<T> Records { get; set; }
    }
}
