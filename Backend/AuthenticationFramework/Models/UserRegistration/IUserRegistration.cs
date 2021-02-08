using AuthenticationFramework.Entities;

namespace AuthenticationFramework.Models.UserRegistration
{
    public interface IUserRegistration
    {
        User HashPassword(UserRegistrationDTO user);
        bool CheckWhitespace(UserRegistrationDTO userRegistration);
        UserRegistrationDTO TrimFields(UserRegistrationDTO userRegistration);
    }
}
