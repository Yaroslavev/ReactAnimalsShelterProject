namespace Core.Models
{
	public class AnimalModel
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Months { get; set; }
        public int GenderId { get; set; }
        public int AnimalTypeId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public string GenderName { get; set; } = null!;
        public string AnimalTypeName { get; set; } = null!;
	}
}
