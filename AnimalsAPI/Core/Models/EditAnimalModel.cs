using Microsoft.AspNetCore.Http;

namespace Core.Models
{
    public class EditAnimalModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Months { get; set; }
        public int GenderId { get; set; }
        public int AnimalTypeId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl {  get; set; }
        public IFormFile? Image { get; set; }
    }
}
