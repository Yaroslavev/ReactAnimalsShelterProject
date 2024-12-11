using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Core.Services
{
    public class FilesService(IWebHostEnvironment env) : IFilesService
    {
        const string imgFolder = "img";

        public async Task<string> SaveImage(IFormFile image)
        {
            string root = env.WebRootPath;
            string name = Guid.NewGuid().ToString();
            string ext = Path.GetExtension(image.FileName);
            string relativePath = Path.Combine(imgFolder, name + ext);
            string fullPath = Path.Combine(root, relativePath);

            using FileStream fileStream = new FileStream(fullPath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            return Path.DirectorySeparatorChar + relativePath;
        }

        public async Task<string> EditImage(IFormFile image, string oldPath)
        {
            await DeleteImage(oldPath);
            return await SaveImage(image);
        }

        public Task DeleteImage(string path)
        {
            string fullPath = env.WebRootPath + path;

            if (File.Exists(fullPath))
            {
                return Task.Run(() => File.Delete(fullPath));
            }

            return Task.CompletedTask;
        }
    }
}
