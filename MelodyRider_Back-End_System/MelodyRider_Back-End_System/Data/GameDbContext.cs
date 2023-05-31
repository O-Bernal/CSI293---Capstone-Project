using Microsoft.EntityFrameworkCore;
using MelodyRider_Back_End_System.Models;

namespace MelodyRider_Back_End_System.Data
{
    public class GameDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAchievement>()
                .HasKey(ua => new { ua.UserId, ua.AchievementId });

            // Other model configurations...
        }
    }
}
