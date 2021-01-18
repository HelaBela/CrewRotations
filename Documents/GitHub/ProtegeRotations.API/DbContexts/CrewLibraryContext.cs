using Microsoft.EntityFrameworkCore;
using ProtegeRotations.API.Entities;

namespace ProtegeRotations.API.DbContexts
{
    public class CrewLibraryContext : DbContext
    {
        public CrewLibraryContext(DbContextOptions<CrewLibraryContext> options)
            : base(options)
        {
        }
        public DbSet<Protege> Proteges { get; set; }
        public DbSet<Crew> Crews { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Protege>().HasData(
                new Protege()
                {
                    LastName = "Franczak",
                    FirstName = "Helena"
                   
                },
                new Protege()
                {
                    LastName = "Cai",
                    FirstName = "Cindy"
                   
                }
            );

            modelBuilder.Entity<Crew>().HasData(
                new Crew
                {
                    Id = 1,
                    Name = "BiFrost",
                    Description = "BiFrost is a team which builds a migration tool."
                },
                new Crew
                {
                    Id = 2,
                    Name = "DevEx",
                    Description = "DevEx is a team which looks after Dev Ops."
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}