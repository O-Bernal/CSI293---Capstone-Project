using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MelodyRider_Back_End_System.Models
{
	public class User : IdentityUser
	{
		[StringLength(50)]
		public string Role { get; set; }

        public bool IsDeleted { get; set; }

        // Navigation property
        public ICollection<Score> Scores { get; set; }
	}

}
