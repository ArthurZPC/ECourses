namespace ECourses.Data.Common
{
    public class PagedList<T>
    {
        public int Count { get; set; }

        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
