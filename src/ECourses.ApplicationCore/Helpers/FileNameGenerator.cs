using Microsoft.AspNetCore.Http;

namespace ECourses.ApplicationCore.Helpers
{
    public static class FileNameGenerator
    {
        public static string Generate(IFormFile file)
        {
            if (file is null)
            {
                return $"{Guid.NewGuid()}";
            }

            return $"{Guid.NewGuid()}-{file.FileName}";
        }
    }
}
