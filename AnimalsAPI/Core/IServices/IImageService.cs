using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Core.IServices
{
    public interface IImageService
    {
        Task ChangeToSmall(string path, Image imageFile);
        Task ChangeToMedium(string path, Image imageFile);
        Task ChangeToLarge(string path, Image imageFile);
        Task ChangeToAllFormats(string path, Image imageFile);
    }
}
