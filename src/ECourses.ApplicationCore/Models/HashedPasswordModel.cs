namespace ECourses.ApplicationCore.Models
{
    public record HashedPasswordModel(string PasswordHash, string PasswordSalt);
}
