using Microsoft.EntityFrameworkCore;
using MelodyRider_Back_End_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MelodyRider_Back_End_System.Data
{
    public class GameDbContext : IdentityDbContext<User>
    {
		public DbSet<Score> Scores { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        { }

		// Seeding the database with test data
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed users
			var admin1 = new User { Id = "1", UserName = "Admin1", Email = "Admin1@example.com", Role = "Admin" };
			var user1 = new User { Id = "2", UserName = "User1", Email = "user1@example.com", Role = "User" };

			modelBuilder.Entity<User>().HasData(admin1, user1);

			// Seed games
			modelBuilder.Entity<Game>().HasData(
				new Game { GameId = 1, GameName = "Melody Riders - Game Mode 1" }
			// ... More games can be added later if I develop game modes in the Unity Web Game later ...
			);

			// Seed scores
			modelBuilder.Entity<Score>().HasData(
				new Score { ScoreId = 1, UserId = "2", GameId = 1, ScoreValue = 1000 },
                new Score { ScoreId = 2, UserId = "2", GameId = 1, ScoreValue = 900 },
                new Score { ScoreId = 3, UserId = "2", GameId = 1, ScoreValue = 800 },
                new Score { ScoreId = 4, UserId = "2", GameId = 1, ScoreValue = 700 },
                new Score { ScoreId = 5, UserId = "2", GameId = 1, ScoreValue = 600 }
            );

			// Seed achievements
			modelBuilder.Entity<Achievement>().HasData(
				new Achievement { AchievementId = 1, Name = "First Play", Description = "Play a game for the first time" },
				new Achievement { AchievementId = 2, Name = "First Clear", Description = "Clear a game for the first time" },
				new Achievement { AchievementId = 3, Name = "First Failure", Description = "Fail a game for the first time" }
			);

			// Seed user achievements
			modelBuilder.Entity<UserAchievement>().HasData(
				new UserAchievement { UserId = user1.Id, AchievementId = 1, DateEarned = DateTime.Now }
			);

			modelBuilder.Entity<UserAchievement>().HasKey(
				ua => new { ua.UserId, ua.AchievementId }
			);
		}
	}
}
