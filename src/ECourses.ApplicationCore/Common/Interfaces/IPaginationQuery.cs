namespace ECourses.ApplicationCore.Common.Interfaces
{
    public interface IPaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
