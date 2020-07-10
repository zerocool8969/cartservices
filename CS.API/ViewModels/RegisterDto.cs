using CS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CS.API.ViewModels
{
    public class RegisterDto
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
        public UserType UserType { get; set; }
    }
}
