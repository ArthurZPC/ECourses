using Microsoft.AspNetCore.Http;

namespace ECourses.ApplicationCore.Helpers
{
    public static class FileNameGenerator
    {
        public static string Generate(IFormFile file)
        {
            return $"{Guid.NewGuid()}-{file.FileName}";
        }
    }
}
