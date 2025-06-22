using Microsoft.EntityFrameworkCore;
using UserManagementService.Domain.entities;

namespace UserManagementService.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Profile> Profiles => Set<Profile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<Profile>().HasKey(p => p.Id);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder
                .Entity<Profile>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Profile>(p => p.UserId);
        }
    }
}
