using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public class FilesService(IWebHostEnvironment env, IImageService imageService, IConfiguration configuration) : IFilesService
    {
        readonly string imgFolder = configuration.GetValue<string>("ImageFolders:BaseImageFolder")!;
        readonly string smallImgFolder = configuration.GetValue<string>("ImageFolders:SmallImageFolder")!;
        readonly string mediumImgFolder = configuration.GetValue<string>("ImageFolders:MediumImageFolder")!;
        readonly string largeImgFolder = configuration.GetValue<string>("ImageFolders:LargeImageFolder")!;

        public async Task<string> SaveImage(IFormFile image)
        {
            string root = env.WebRootPath;
            string name = Guid.NewGuid().ToString();
            string ext = Path.GetExtension(image.FileName);
            string relativePath = Path.Combine(imgFolder, name + ext);
            string fullPath = Path.Combine(root, relativePath);

            await imageService.ChangeToAllFormats(fullPath, image);
            await DeleteImage(relativePath);

            return Path.DirectorySeparatorChar + relativePath;
        }

        public async Task<string> EditImage(IFormFile image, string oldPath)
        {
            await DeleteImageAllFormats(oldPath);

            return await SaveImage(image);
        }

        public Task DeleteImage(string path)
        {
            string fullPath = Path.Combine(env.WebRootPath, path);

            if (File.Exists(fullPath))
            {
                return Task.Run(() => File.Delete(fullPath));
            }

            return Task.CompletedTask;
        }

        public async Task DeleteImageAllFormats(string path)
        {
            string imageName = path.Split("\\").Last();
            string relativePath = Path.Combine(env.WebRootPath, imgFolder);
            string smallImgPath = Path.Combine(relativePath, smallImgFolder, imageName);
            string mediumImgPath = Path.Combine(relativePath, mediumImgFolder, imageName);
            string largeImgPath = Path.Combine(relativePath, largeImgFolder, imageName);

            if (File.Exists(smallImgPath))
            {
                await Task.Run(() => File.Delete(smallImgPath));
            }
            if (File.Exists(mediumImgPath))
            {
                await Task.Run(() => File.Delete(mediumImgPath));
            }
            if (File.Exists(largeImgPath))
            {
                await Task.Run(() => File.Delete(largeImgPath));
            }
        }
    }
}
