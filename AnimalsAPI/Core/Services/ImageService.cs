using Core.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace Core.Services
{
    public class ImageService(IWebHostEnvironment env, IConfiguration configuration) : IImageService
    {
        readonly string imageFolder = configuration.GetValue<string>("ImageFolders:BaseImageFolder")!;
        readonly string imageSmallFolder = configuration.GetValue<string>("ImageFolders:SmallImageFolder")!;
        readonly string imageMediumFolder = configuration.GetValue<string>("ImageFolders:MediumImageFolder")!;
        readonly string imageLargeFolder = configuration.GetValue<string>("ImageFolders:LargeImageFolder")!;
        readonly int smallImgWidth = configuration.GetValue<int>("ImageWidth:SmallImageWidth")!;
        readonly int mediumImgWidth = configuration.GetValue<int>("ImageWidth:MediumImageWidth")!;
        readonly int largeImgWidth = configuration.GetValue<int>("ImageWidth:LargeImageWidth")!;
        readonly int smallImgHeight = configuration.GetValue<int>("ImageHeight:SmallImageHeight")!;
        readonly int mediumImgHeight = configuration.GetValue<int>("ImageHeight:MediumImageHeight")!;
        readonly int largeImgHeight = configuration.GetValue<int>("ImageHeight:LargeImageHeight")!;

        public async Task ChangeSize(IFormFile imageFile, string pathToUpload, int width, int height)
        {
            using (var stream = imageFile.OpenReadStream())
            {
                using (var image = await Image.LoadAsync(stream))
                {
                    image.Mutate(x => x.Resize(new Size(width, height)));
                    var encoder = new WebpEncoder() { Quality = 90 };

                    await image.SaveAsync(pathToUpload, encoder);
                }
            }
        }

        public async Task ChangeToSmall(string path, IFormFile imageFile)
        {
            string imageName = Path.GetFileName(path);
            string pathToUpload = Path.Combine(env.WebRootPath, imageFolder, imageSmallFolder, imageName);

            await ChangeSize(imageFile, pathToUpload, smallImgWidth, smallImgHeight);
        }

        public async Task ChangeToMedium(string path, IFormFile imageFile)
        {
            string imageName = Path.GetFileName(path);
            string pathToUpload = Path.Combine(env.WebRootPath, imageFolder, imageMediumFolder, imageName);

            await ChangeSize(imageFile, pathToUpload, mediumImgWidth, mediumImgHeight);
        }

        public async Task ChangeToLarge(string path, IFormFile imageFile)
        {
            string imageName = Path.GetFileName(path);
            string pathToUpload = Path.Combine(env.WebRootPath, imageFolder, imageLargeFolder, imageName);

            await ChangeSize(imageFile, pathToUpload, largeImgWidth, largeImgHeight);
        }

        public async Task ChangeToAllFormats(string path, IFormFile imageFile)
        {
            Console.WriteLine(imageFolder);
            Console.WriteLine(imageSmallFolder);
            Console.WriteLine(imageMediumFolder);
            Console.WriteLine(imageLargeFolder);
            Console.WriteLine(smallImgWidth);
            Console.WriteLine(mediumImgWidth);
            Console.WriteLine(largeImgWidth);
            Console.WriteLine(smallImgHeight);
            Console.WriteLine(mediumImgHeight);
            Console.WriteLine(largeImgHeight);
            await ChangeToSmall(path, imageFile);
            await ChangeToMedium(path, imageFile);
            await ChangeToLarge(path, imageFile);
        }
    }
}
