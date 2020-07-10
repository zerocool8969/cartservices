using CS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CS.API.ViewModels
{
    public class SignInDto
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
        public UserType UserType { get; set; }
        public string UserName { get; set; }
    }
}
