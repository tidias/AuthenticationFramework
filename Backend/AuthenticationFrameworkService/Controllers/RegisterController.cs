using AuthenticationFramework.Data;
using AuthenticationFramework.Models.Recaptcha;
using AuthenticationFramework.Models.UserRegistration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationFramework.Models.ApiResponse;


namespace AuthenticationFrameworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegisterController : ControllerBase
    {
        private readonly AuthenticationFrameworkContext _context;
        private readonly IUserRegistration _userRegistration;
        private readonly IReCaptcha _reCaptcha;


        public RegisterController(AuthenticationFrameworkContext context, IUserRegistration userRegistration, IReCaptcha reCaptcha)
        {
            _context = context;
            _userRegistration = userRegistration;
            _reCaptcha = reCaptcha;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromForm] UserRegistrationDTO userRegistrationRequest)
        {
            if (!_reCaptcha.Validate(userRegistrationRequest.ReCaptchaToken))
            {
                return Ok(new Status { Success = false, Errors = new[] { "Invalid ReCaptcha." } });
            }

            var userRegistration = _userRegistration.TrimFields(userRegistrationRequest);

            if (!ModelState.IsValid)
            {
                return Ok(new Status { Success = false, Errors = new[] { "Fields missing." } });

            }

            if (_userRegistration.CheckWhitespace(userRegistration))
            {
                return Ok(new Status { Success = false, Errors = new[] { "Fields can't have whitespace." } });
            }


            if (_context.Users.Any(x => x.UserName == userRegistration.UserName))
            {
                return Ok(new Status { Success = false, Errors = new[] { "Username already exists." } });

            }

            if (_context.Users.Any(x => x.Email == userRegistration.Email))
            {
                return Ok(new Status { Success = false, Errors = new[] { "Email already exists." } });
            }

            var hashedUser = _userRegistration.HashPassword(userRegistration);

            _context.Users.Add(hashedUser);
            await _context.SaveChangesAsync();

            return Ok(new Status { Success = true });
        }
    }
}
