using Data.Enteties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbInitializer
    {
        public static void Initialize(AnimalsDbContext context)
        {
            context.Database.Migrate();

            if (context.Animals.Any() && context.AnimalTypes.Any() && context.Genders.Any())
            {
                return;
            }

            Gender[] genders =
            {
                new Gender() { Id = 1, Name = "Male" },
                new Gender() { Id = 2, Name = "Female" }
            };

            AnimalType[] animalTypes =
            {
                new AnimalType() { Id = 1, Name = "Cat" },
                new AnimalType() { Id = 2, Name = "Dog" },
                new AnimalType() { Id = 3, Name = "Hamster" },
                new AnimalType() { Id = 4, Name = "Turtle" },
                new AnimalType() { Id = 5, Name = "Other" }
            };

            Animal[] animals =
            {
                new Animal() { Id = 1, Name = "Charlie", Months = 27, GenderId = 2, AnimalTypeId = 1, Description = "Likes to sleep", ImageUrl = "https://image.petmd.com/files/inline-images/black-cat-breeds-american-shorthair.jpeg?VersionId=FHXiYOmOykNtIdlZ.V5LQC_E8wXzlgyG" },
                new Animal() { Id = 2, Name = "Pakky", Months = 13, GenderId = 1, AnimalTypeId = 2, Description = "Doesn't like silence", ImageUrl = "https://hips.hearstapps.com/hmg-prod/images/best-small-dog-breeds-chihuahua-1598967884.jpg" },
                new Animal() { Id = 3, Name = "Rik", Months = 45, GenderId = 1, AnimalTypeId = 2, Description = "Needs a lot of walking", ImageUrl = "https://www.nylabone.com/-/media/project/oneweb/nylabone/images/dog101/10-intelligent-dog-breeds/golden-retriever-tongue-out.jpg" },
                new Animal() { Id = 4, Name = "Sherry", Months = 4, GenderId = 2, AnimalTypeId = 3, Description = "Likes to eat and sleep", ImageUrl = "https://blog.omlet.us/wp-content/uploads/sites/6/2023/08/Hamster-laying-down-on-the-counter-scaled.jpg" },
                new Animal() { Id = 5, Name = "Yoru", Months = 9, GenderId = 2, AnimalTypeId = 1, Description = "Likes attention", ImageUrl = "https://www.zooplus.ie/magazine/wp-content/uploads/2021/07/Outdoor-kitten-explores-the-garden-768x512.jpeg" },
            };

            foreach (Gender gender in genders)
            {
                context.Genders.Add(gender);
            }

            foreach (AnimalType animalType in animalTypes)
            {
                context.AnimalTypes.Add(animalType);
            }

            foreach (Animal animal in animals)
            {
                context.Animals.Add(animal);
            }

            context.SaveChanges();

            context.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"Genders\"', 'Id'), (SELECT MAX(\"Id\") FROM \"Genders\"))");
            context.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"AnimalTypes\"', 'Id'), (SELECT MAX(\"Id\") FROM \"AnimalTypes\"))");
            context.Database.ExecuteSqlRaw("SELECT setval(pg_get_serial_sequence('\"Animals\"', 'Id'), (SELECT MAX(\"Id\") FROM \"Animals\"))");
        }
    }
}
