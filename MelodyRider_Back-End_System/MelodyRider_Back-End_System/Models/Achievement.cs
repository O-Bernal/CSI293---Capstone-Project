﻿using System.ComponentModel.DataAnnotations;

namespace MelodyRider_Back_End_System.Models
{
    public class Achievement
    {
        [Key]
        public int AchievementId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Navigation properties
        public ICollection<UserAchievement> UserAchievements { get; set; }
    }

}
