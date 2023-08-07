using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AnotherContactBook.Model
{
    public class AppUser : IdentityUser
    {
        //[Key]
        //public Guid Id { get; set; }
        //public string Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string Avatar { get; set; } = "";
        //public byte[] PasswordSalt { get; set; }
        //public byte[] PasswordHash { get; set; }
        //public bool IsAdmin { get; set; } // add this property
    }
}
