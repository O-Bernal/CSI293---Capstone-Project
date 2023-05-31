using System.ComponentModel.DataAnnotations;

namespace MelodyRider_Back_End_System.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        [StringLength(100)]
        public string GameName { get; set; }

        // Navigation properties
        public ICollection<Score> Scores { get; set; }
    }

}
