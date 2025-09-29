using Microsoft.EntityFrameworkCore;
using TerfMate.Server.Models;

namespace TurfMate.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Turf> Turfs { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Turf)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TurfId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Turf>()
                .HasIndex(t => t.Name);

            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.TurfId, b.BookingDate, b.StartTime, b.EndTime });

            // Seed initial data
            modelBuilder.Entity<Turf>().HasData(
                new Turf
                {
                    Id = 1,
                    Name = "City Sports Arena",
                    Location = "Downtown City Center",
                    Description = "Premium quality turf with professional maintenance",
                    PricePerHour = 50.00m,
                    ImageUrl = "/images/turf1.jpg",
                    IsAvailable = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Turf
                {
                    Id = 2,
                    Name = "Green Valley Turf",
                    Location = "Green Valley Area",
                    Description = "Spacious turf perfect for team practice",
                    PricePerHour = 35.00m,
                    ImageUrl = "/images/turf2.jpg",
                    IsAvailable = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Turf
                {
                    Id = 3,
                    Name = "Elite Football Ground",
                    Location = "Sports Complex Road",
                    Description = "Professional football turf with floodlights",
                    PricePerHour = 75.00m,
                    ImageUrl = "/images/turf3.jpg",
                    IsAvailable = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@turfmate.com",
                    PasswordHash = "$2a$11$5ZQ5/./pQRuD2YlNKbZq.O6c7z2z5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z5Z", // admin123
                    FirstName = "Admin",
                    LastName = "User",
                    Role = "admin",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}