namespace ECourses.ApplicationCore.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;    
    }
}
