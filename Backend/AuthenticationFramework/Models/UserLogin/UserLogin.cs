using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationFramework.Configs;
using AuthenticationFramework.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationFramework.Models.UserLogin
{
    public class UserLogin : IUserLogin
    {
        private readonly IOptions<Secrets> _options;

        public UserLogin()
        {

        }

        public UserLogin(IOptions<Secrets> options)
        {
            _options = options;
        }

        public string Authenticate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Jwt);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddSeconds(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return token;
        }

        public bool VerifyPassword(string claimPassword, string salt, string hash)
        {
            return hash == Convert.ToBase64String(KeyDerivation.Pbkdf2(claimPassword, Encoding.UTF8.GetBytes(salt), KeyDerivationPrf.HMACSHA512, 10000, 256 / 8));
        }
    }
}
