using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Internel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public HealthController(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        // GET api/values
        [HttpGet]
        //[ResultFilter]
        public string Get()
        {
            return Configuration["port"];
        }
    }
}
