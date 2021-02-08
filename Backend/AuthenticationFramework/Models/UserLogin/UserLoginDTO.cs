using System.ComponentModel.DataAnnotations;

namespace AuthenticationFramework.Models.UserLogin
{
    public class UserLoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ReCaptchaToken { get; set; }
    }
}
