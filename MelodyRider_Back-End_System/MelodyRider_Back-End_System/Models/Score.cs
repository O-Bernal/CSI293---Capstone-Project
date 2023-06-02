using System.ComponentModel.DataAnnotations;

namespace MelodyRider_Back_End_System.Models
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }

		[Required]
		public string UserId { get; set; } // In the context of ASP.NET Core Identity, UserId is a string by default. This is because the IdentityUser class, which the User model is inheriting from, has its Id property as a string.

		[Required]
        public int GameId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ScoreValue { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Game Game { get; set; }
    }

}
