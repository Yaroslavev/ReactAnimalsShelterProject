using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Core.IServices
{
    public interface IImageService
    {
        Task ChangeToSmall(string path, IFormFile imageFile);
        Task ChangeToMedium(string path, IFormFile imageFile);
        Task ChangeToLarge(string path, IFormFile imageFile);
        Task ChangeToAllFormats(string path, IFormFile imageFile);
    }
}
