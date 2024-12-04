using System.ComponentModel.DataAnnotations;

namespace SeccionsProgamAPI.Models
{
    public class UserDTO
    {
        [Required]
        [Display(Name = "Login")]
        public required string Login { get; set; }

        [Required]
        [Display(Name = "Password")]
        public required string Password { get; set; }
    }
}
