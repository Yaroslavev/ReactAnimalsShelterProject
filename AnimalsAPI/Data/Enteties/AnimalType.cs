﻿namespace Data.Enteties
{
    public class AnimalType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Animal>? Animals { get; set; }
    }
}
