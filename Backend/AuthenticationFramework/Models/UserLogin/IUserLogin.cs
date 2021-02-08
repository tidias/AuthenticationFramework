using AuthenticationFramework.Entities;

namespace AuthenticationFramework.Models.UserLogin
{
    public interface IUserLogin
    {
        string Authenticate(User user);
        bool VerifyPassword(string claimPassword, string salt, string hash);
    }
}
