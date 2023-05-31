using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WomensTechForum2._0.Areas.Identity.Data;

namespace WomensTechForum2._0.Data;

public class WomensTechForum2_0Context : IdentityDbContext<WomensTechForum2_0User>
{
    public WomensTechForum2_0Context(DbContextOptions<WomensTechForum2_0Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Models.MainCategory> MainCategory { get; set; } = default!;
    public DbSet<Models.Post> Post { get; set; } = default!;
    public DbSet<Models.SubCategory> SubCategory { get; set; } = default!;
    public DbSet<Models.PostThread> PostThread { get; set; } = default!;
    public DbSet<Models.LikePostThread> LikePostThread { get; set; } = default!;
    public DbSet<Models.LikePost> LikePost { get; set; } = default!;
}
