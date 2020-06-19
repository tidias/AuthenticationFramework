using System.Linq;
using System.Net.Mime;
using AuthenticationFramework.Data;
using AuthenticationFramework.Models.ApiResponse;
using AuthenticationFramework.Models.Recaptcha;
using AuthenticationFramework.Models.UserLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationFrameworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public class LoginController : ControllerBase
    {
        private readonly AuthenticationFrameworkContext _context;
        private readonly IUserLogin _userLogin;
        private readonly IReCaptcha _reCaptcha;

        public LoginController(AuthenticationFrameworkContext context, IUserLogin userLogin, IReCaptcha reCaptcha)
        {
            _context = context;
            _userLogin = userLogin;
            _reCaptcha = reCaptcha;
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult GetInfo()
        {
            return Ok(new Status { Info = "If you're seeing this, it means you are authorized to see it." });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromForm] UserLoginDTO user)
        {

            if (!_reCaptcha.Validate(user.ReCaptchaToken))
            {
                return Ok(new Status { Success = false, Errors = new[] { "Invalid ReCaptcha." } });
            }

            var userCtx = _context.Users.SingleOrDefault(x => x.UserName == user.UserName);

            if (userCtx == null)
            {
                return Ok(new Status { Success = false, Errors = new[] { "Invalid UserName or Password." } });
            }

            if (!_userLogin.VerifyPassword(user.Password, userCtx.Salt, userCtx.Hash))
            {
                return Ok(new Status { Success = false, Errors = new[] { "Invalid UserName or Password." } });
            }

            return Ok(new Status { Success = true, Token = _userLogin.Authenticate(userCtx) });
        }
    }
}
