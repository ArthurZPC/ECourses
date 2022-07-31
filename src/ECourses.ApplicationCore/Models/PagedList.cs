namespace ECourses.ApplicationCore.Models
{
    public class PagedList<T> 
    {
        public int Count { get; set; }

        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
