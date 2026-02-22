using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocuEye.Web.Controllers
{
    [Route("api/app")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IAntiforgery forgeryService;
        public AppController(IAntiforgery forgeryService)
        {
            this.forgeryService = forgeryService;
        }

        [Route("context")]
        [HttpGet]
        public IActionResult Context()
        {
            return this.Ok(new
            {
                displayName ="",
                lang = "pl",
                systemRole = ""
            });

        }

        [Route("compatibility/info")]
        [HttpGet]
        public IActionResult CompatibilityInfo()
        {
            // Use format defined by NuGet versioning
            // https://learn.microsoft.com/en-us/nuget/concepts/package-versioning?tabs=semver20sort
            return this.Ok(new
            {
                acceptedVersions = "[1.3.0,2.0.0]"
            });

        }

        [Route("antiforgery/token")]
        [HttpGet]
        [Authorize(Policy = "General")]
        public IActionResult AntiForgeryToken()
        {
            var tokens = forgeryService.GetAndStoreTokens(this.HttpContext);
            return this.Ok(new
            {
                Token = tokens.RequestToken!
            });
        }
    }
}
