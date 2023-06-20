using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MelodyRider_Back_End_System.Models
{
    public class UserAchievement
    {
        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [Required]
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }

        [Required]
        public DateTime DateEarned { get; set; }
    }
}
