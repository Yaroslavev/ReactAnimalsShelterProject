namespace Data.Enteties
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Months { get; set; }
        public int GenderId { get; set; }
        public int AnimalTypeId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public Gender? Gender { get; set; }
        public AnimalType? AnimalType { get; set; }
    }
}
