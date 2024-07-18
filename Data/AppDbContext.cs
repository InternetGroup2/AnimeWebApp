using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AnimeWebApp.Models;

namespace AnimeWebApp.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AnimeModel> AnimeModels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DiscussionModel> DiscussionModel { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AnOrigin> AnOrigins { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Example of renaming ASP.NET Identity table names
            builder.Entity<IdentityUser>(entity => {
                entity.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole>(entity => {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<string>>(entity => {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity => {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity => {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity => {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity => {
                entity.ToTable("UserTokens");
            });
        }
    }
}
