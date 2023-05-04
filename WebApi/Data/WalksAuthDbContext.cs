using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data;

public class WalksAuthDbContext : IdentityDbContext
{
    public WalksAuthDbContext(DbContextOptions<WalksAuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var readerId = "6ca2b7d4-b05d-4f33-aea9-418c315953f9";
        var writerId = "4016fb5e-cfbd-4eaa-9cc0-42f1421ae294";

        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerId,
                ConcurrencyStamp = readerId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper()
            },
            new IdentityRole
            {
                Id = writerId,
                ConcurrencyStamp = writerId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper()
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}
