using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.Models
{
    public class AddAnimalModel
    {
        [Required, MaxLength(50)]
        public required string Name { get; set; }
        [Required]
        public int Months { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public int AnimalTypeId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
