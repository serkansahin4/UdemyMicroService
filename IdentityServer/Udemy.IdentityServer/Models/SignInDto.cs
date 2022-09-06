using System.ComponentModel.DataAnnotations;

namespace Udemy.IdentityServer.Models
{
    public class SignInDto
    {
        [Required(ErrorMessage ="Email Adresi Gereklidir.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Parola Gereklidir")]
        
        public string Password { get; set; }
    }
}
