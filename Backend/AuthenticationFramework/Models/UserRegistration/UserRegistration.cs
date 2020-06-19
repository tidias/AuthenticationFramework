using System;
using System.Security.Cryptography;
using System.Text;
using AuthenticationFramework.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AuthenticationFramework.Models.UserRegistration
{
    public class UserRegistration : IUserRegistration
    {
        public User HashPassword(UserRegistrationDTO userRegistration)
        {
            var user = Mappings.Mapping.Mapper.Map<User>(userRegistration);
            var randomBytes = new byte[128 / 8];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
            }

            user.Salt = Convert.ToBase64String(randomBytes);
            user.Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(userRegistration.Password, Encoding.UTF8.GetBytes(user.Salt), KeyDerivationPrf.HMACSHA512, 10000, 256 / 8));

            return user;
        }

        public UserRegistrationDTO TrimFields(UserRegistrationDTO userRegistration)
        {
            userRegistration.Password = userRegistration.Password.Trim();
            userRegistration.Email = userRegistration.Email.Trim();
            userRegistration.FirstName = userRegistration.FirstName.Trim();
            userRegistration.LastName = userRegistration.LastName.Trim();
            userRegistration.UserName = userRegistration.UserName.Trim();

            return userRegistration;

        }
        public bool CheckWhitespace(UserRegistrationDTO userRegistration)
        {
            if (userRegistration.UserName.Contains(" ") || userRegistration.Email.Contains(" ") || userRegistration.FirstName.Contains(" ") || userRegistration.LastName.Contains(" ") || userRegistration.Password.Contains(" "))
            {
                return true;
            }

            return false;
        }
    }
}
