using System.ComponentModel.DataAnnotations;

namespace MelodyRider_Back_End_System.Models
{
    public class UserAchievement
    {
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }

        [Required]
        public DateTime DateEarned { get; set; }
    }
}
