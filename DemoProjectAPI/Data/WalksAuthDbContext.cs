using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectAPI.Data
{
    public class WalksAuthDbContext : IdentityDbContext
    {
        public WalksAuthDbContext(DbContextOptions<WalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var readerRole = "e446f719-cc8b-45d6-907b-bd5784fc4b7a";
            var writerRole = "048c25ff-a01d-4ec0-8bb0-1902db65dbde";
            base.OnModelCreating(builder);
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRole,
                    ConcurrencyStamp = readerRole,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                 new IdentityRole()
                {
                    Id = writerRole,
                    ConcurrencyStamp = writerRole,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
