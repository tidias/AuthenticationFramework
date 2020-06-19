using System.ComponentModel.DataAnnotations;

namespace AuthenticationFramework.Models.UserRegistration
{
    public class UserRegistrationDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ReCaptchaToken { get; set; }
    }
}
