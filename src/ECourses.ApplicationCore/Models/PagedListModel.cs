namespace ECourses.ApplicationCore.Models
{
    public class PagedListModel<T> 
    {
        public int Count { get; set; }

        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
