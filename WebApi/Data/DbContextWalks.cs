using Microsoft.EntityFrameworkCore;
using WebApi.Models.Domain;

namespace WebApi.Data
{
    public class DbContextWalks : DbContext    
    {
        public DbContextWalks(DbContextOptions<DbContextWalks> dbContextOptions): base(dbContextOptions)
        {            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("9e6ce577-1469-4137-8fcd-45dc5049ad1d"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("8298bd60-08d1-4a81-8aab-deb0c91ac08f"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("a97f3970-0261-4678-8387-39535c8802e6"),
                    Name = "Hard"
                }

            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("e022f144-e5ad-45ae-84c5-40dfa3f08f9d"),
                    Name = "Kyiv",
                    Code = "KV",
                    ImageRegionUri = "hello-world.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("e6546eb9-0c71-4c3a-8519-3acbea6eb313"),
                    Name = "Odesa",
                    Code = "ODS",
                    ImageRegionUri = "bb.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("510c9613-7f9a-420f-9ec3-42254fee2fa0"),
                    Name = "Lviv",
                    Code = "LV",
                    ImageRegionUri = "world.jpg"
                },

            };

            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
