using AuthenticationFramework.Configs;
using AuthenticationFramework.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AuthenticationFramework.Models.Recaptcha
{
    public class ReCaptcha : IReCaptcha
    {
        private readonly IOptions<Secrets> _options;
        public ReCaptcha()
        {

        }

        public ReCaptcha(IOptions<Secrets> options)
        {
            _options = options;
        }

        public bool Validate(string reCaptchaToken)
        {
            var reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaDTO>(HttpHelpers.SendPostRequest("https://www.google.com/recaptcha/api/siteverify", $"secret={_options.Value.Recaptcha}&response={reCaptchaToken}", "application/x-www-form-urlencoded"));

            return reCaptchaResponse.Success;
        }
    }
}
