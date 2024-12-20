using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PetSoLive_MVC.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<PetSize> PetSizes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure UserProfile entity
            builder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.ApplicationUser)
                    .WithOne()
                    .HasForeignKey<UserProfile>(e => e.ApplicationUserId)
                    .IsRequired();
            });

            // Configure Pets entity
            builder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Species)
                    .WithMany()
                    .HasForeignKey(e => e.SpeciesId)
                    .OnDelete(DeleteBehavior.Restrict); // Specify OnDelete behavior
            });

            // Additional configuration for other entities if needed
        }
    }
}