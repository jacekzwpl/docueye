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
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {

            returnUrl ??= "/";
            return new ChallengeResult(
                OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties()
                {
                    RedirectUri = returnUrl
                });
            /*
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower() == "development";
            if (isDevelopment)
            {
                returnUrl ??= "/";
                return new ChallengeResult(
                    OpenIdConnectDefaults.AuthenticationScheme,
                    new AuthenticationProperties()
                    {
                        RedirectUri = returnUrl
                    });
            }else
            {
                return Redirect("/");
            }
                */
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            if(this.User.Identity?.IsAuthenticated == true)
            {
                return SignOut(
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
            }else
            {
                return Redirect("/");
            }
            
        }
    }
}
