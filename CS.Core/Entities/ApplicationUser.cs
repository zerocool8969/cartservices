using CS.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace CS.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public UserType UserType { get; set; }
    }
}
