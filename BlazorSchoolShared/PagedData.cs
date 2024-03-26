namespace BlazorSchoolShared
{
    public class PagedData<T>
    {
        public PagedData(List<T> records)
        {
            Records = records;
        }

        public int TotalCount { get; set; }
        public List<T> Records { get; set; }
    }
}
