using System.ComponentModel.DataAnnotations;

namespace DateApp.API.DTO
{
    public class UserForRegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "Password must be between 7 and 100 characters")]
        public string Password { get; set; }
    }
}