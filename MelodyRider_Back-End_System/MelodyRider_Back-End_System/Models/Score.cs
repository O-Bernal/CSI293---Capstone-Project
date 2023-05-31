using System.ComponentModel.DataAnnotations;

namespace MelodyRider_Back_End_System.Models
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }

        [Required]
        public int UserId { get; set; }

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
