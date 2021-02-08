namespace AuthenticationFramework.Models.Recaptcha
{
    public interface IReCaptcha
    {
        bool Validate(string reCaptchaToken);
    }
}
