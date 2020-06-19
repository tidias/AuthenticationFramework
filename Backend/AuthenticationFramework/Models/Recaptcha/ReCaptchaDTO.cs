using System;

namespace AuthenticationFramework.Models.Recaptcha
{
    public class ReCaptchaDTO
    {
        public bool Success { get; set; }
        public DateTime Challenge_Ts { get; set; }
        public string Hostname { get; set; }
        public string[] ErrorCodes { get; set; }
    }
}
