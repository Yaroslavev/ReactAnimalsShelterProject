using Data.Enteties;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data
{
    public class AnimalsDbContext : IdentityDbContext<User>
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }

        public AnimalsDbContext() { }
        public AnimalsDbContext(DbContextOptions options) : base(options) { }
    }
}
