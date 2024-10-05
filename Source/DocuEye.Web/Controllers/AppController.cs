using Microsoft.AspNetCore.Http;
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
    }
}
