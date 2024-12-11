using Microsoft.AspNetCore.Http;

namespace Core.IServices
{
    public interface IFilesService
    {
        Task<string> SaveImage(IFormFile image);
        Task<string> EditImage(IFormFile image, string oldPath);
        Task DeleteImage(string path);
    }
}
