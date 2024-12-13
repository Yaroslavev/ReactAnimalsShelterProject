using Core.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Core.Services
{
    public class ImageService(IWebHostEnvironment env, IConfiguration configuration) : IImageService
    {
        readonly string imageFolder = configuration.GetValue<string>("ImageFolders:BaseImageFolder")!;
        readonly string imageSmallFolder = configuration.GetValue<string>("ImageFolders:SmallImageFolder")!;
        readonly string imageMediumFolder = configuration.GetValue<string>("ImageFolders:MediumImageFolder")!;
        readonly string imageLargeFolder = configuration.GetValue<string>("ImageFolders:LargeImageFolder")!;
        readonly int smallImgHeight = configuration.GetValue<int>("ImageHeight:SmallImageHeight")!;
        readonly int mediumImgHeight = configuration.GetValue<int>("ImageHeight:MediumImageHeight")!;
        readonly int largeImgHeight = configuration.GetValue<int>("ImageHeight:LargeImageHeight")!;

        public async Task ResizeAndSave(string path, int width, int height, Image img)
        {
            using (Image resizedImage = img.Clone(x => x.Resize(width, height)))
            {
                await resizedImage.SaveAsync(path);
            }
        }

        public async Task ChangeToSmall(string path, Image imageFile)
        {
            string imageName = Path.GetFileName(path);
            string pathToUpload = Path.Combine(env.WebRootPath, imageFolder, imageSmallFolder, imageName);

            double timesToDivide = (double)imageFile.Height / smallImgHeight;
            int imageWidth = (int)(imageFile.Width / timesToDivide);
            int imageHeight = (int)(imageFile.Height / timesToDivide);

            await ResizeAndSave(pathToUpload, imageWidth, imageHeight, imageFile);

        }

        public async Task ChangeToMedium(string path, Image imageFile)
        {
            string imageName = Path.GetFileName(path);
            string pathToUpload = Path.Combine(env.WebRootPath, imageFolder, imageMediumFolder, imageName);

            double timesToDivide = (double)imageFile.Height / mediumImgHeight;
            int imageWidth = (int)(imageFile.Width / timesToDivide);
            int imageHeight = (int)(imageFile.Height / timesToDivide);

            await ResizeAndSave(pathToUpload, imageWidth, imageHeight, imageFile);

        }

        public async Task ChangeToLarge(string path, Image imageFile)
        {
            string imageName = Path.GetFileName(path);
            string pathToUpload = Path.Combine(env.WebRootPath, imageFolder, imageLargeFolder, imageName);

            double timesToDivide = (double)imageFile.Height / largeImgHeight;
            int imageWidth = (int)(imageFile.Width / timesToDivide);
            int imageHeight = (int)(imageFile.Height / timesToDivide);

            await ResizeAndSave(pathToUpload, imageWidth, imageHeight, imageFile);
        }

        public async Task ChangeToAllFormats(string path, Image imageFile)
        {
            await ChangeToSmall(path, imageFile);
            await ChangeToMedium(path, imageFile);
            await ChangeToLarge(path, imageFile);
        }
    }
}
