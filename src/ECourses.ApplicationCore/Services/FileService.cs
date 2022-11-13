using ECourses.ApplicationCore.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace ECourses.ApplicationCore.Services
{
    public class FileService : IFileService
    {
        private const string extensionRegExp = @"\.[\w]+$";
        public void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task UploadFormFileAsync(IFormFile formFile, string path)
        {
            if (formFile is not null)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
        }

        public void Move(string currentPath, string newPath)
        {
            File.Move(currentPath, newPath);
        }

        public bool ContainsExtension(string fileName)
        {
            return new Regex(extensionRegExp).IsMatch(fileName);
        }

        public string GetExtension(string fileName)
        {
            var regex = new Regex(extensionRegExp);

            return regex.Match(fileName).Value;
        }
    }
}
