using Microsoft.EntityFrameworkCore;
using RhythmGameWebApplication.Models;
using System;

namespace RhythmGameWebApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSet properties

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=rhythmgamedb;user=root;password=!2UB$2+c,a2u,7y;",
                new MySqlServerVersion(new Version(8, 0, 32)));
        }
        public DbSet<HighScore> HighScores { get; set; }
    }
}
