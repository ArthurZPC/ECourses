using Microsoft.AspNetCore.Http;

namespace ECourses.ApplicationCore.Common.Interfaces.Services
{
    public interface IFileService
    {
        Task UploadFormFileAsync(IFormFile formFile, string path);
        void Delete(string path);
        void Move(string currentPath, string newPath);
        bool ContainsExtension(string fileName);
        string GetExtension(string fileName);
    }
}
