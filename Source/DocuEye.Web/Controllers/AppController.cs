using Microsoft.AspNetCore.Mvc;

namespace DocuEye.Web.Controllers
{
    [Route("api/app")]
    [ApiController]
    public class AppController : ControllerBase
    {
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
                acceptedVersions = "[1.0.0,2.0.0]"
            });

        }
    }
}
