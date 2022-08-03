namespace ECourses.ApplicationCore.ViewModels.CreateViewModels
{
    public class CreateRatingViewModel
    {
        public int Value { get; set; }

        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
    }
}
