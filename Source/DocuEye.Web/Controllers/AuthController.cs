using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DocuEye.Web.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
       
        [Route("login")]
        public IActionResult Login(string? returnUrl)
        {
            returnUrl ??= "/";
            return new ChallengeResult(
                OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties()
                {
                    RedirectUri = returnUrl
                });
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            return SignOut(
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
