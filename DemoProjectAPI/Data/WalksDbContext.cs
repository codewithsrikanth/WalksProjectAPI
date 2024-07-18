using DemoProjectAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectAPI.Data
{
    public class WalksDbContext : DbContext
    {
        public WalksDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {            
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walks> Walks { get; set; }
    }
}
