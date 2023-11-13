using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessConnect.Areas.Identity.Data;

public class ApplicationDBContext : IdentityDbContext<
    ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
    ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<MessageModel> Messages { get; set; }
    public DbSet<Contact> Contact { get; set; }
    public DbSet<LoggerModel> Logger { get; set; }
    public virtual DbSet<PermissionModel> Permission { get; set; }
    public virtual DbSet<RolePermission> RolePermission { get; set; }
    public virtual DbSet<ApplicationUserRole> UserRoles { get; set; }
    public virtual DbSet<ApplicationRole> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Entity<ApplicationRole>().Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Entity<ApplicationUserRole>();
        builder.Entity<MessageModel>()
           .ToTable("Messages")
           .Property(x => x.Id)
           .ValueGeneratedOnAdd();
        builder.Entity<Contact>()
           .ToTable("Contact")
           .Property(x => x.Id)
           .ValueGeneratedOnAdd();
        builder.Entity<LoggerModel>()
           .ToTable("Logger")
           .Property(x => x.Id)
           .ValueGeneratedOnAdd();
        builder.Entity<PermissionModel>()
           .ToTable("Permission")
           .Property(x => x.Id)
           .ValueGeneratedOnAdd();

        builder.Entity<RolePermission>()
            .ToTable("RolePermission")
            .HasKey(x => new { x.PermissionId, x.RoleId });
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
