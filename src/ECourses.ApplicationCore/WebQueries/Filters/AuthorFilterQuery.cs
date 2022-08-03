namespace ECourses.ApplicationCore.WebQueries.Filters
{
    public class AuthorFilterQuery
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        public Guid UserId { get; set; }
    }
}
