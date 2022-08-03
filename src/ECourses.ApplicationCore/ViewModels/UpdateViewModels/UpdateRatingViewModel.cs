namespace ECourses.ApplicationCore.ViewModels.UpdateViewModels
{
    public class UpdateRatingViewModel
    {
        public Guid Id { get; set; }
        public int? Value { get; set; }

        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
    }
}
