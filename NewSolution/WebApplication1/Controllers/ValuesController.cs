using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IJavaProviderService _javaProviderService;

        public ValuesController(IJavaProviderService javaProviderService)
        {
            _javaProviderService = javaProviderService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return id.ToString();
        }

        // GET api/values
        // .Net 通过Eureka调用java服务
        [Route("java")]
        [HttpGet]
        public async Task<string> Getsss()
        {
            return $"client { await _javaProviderService.GetValueAsync()}";
        }
    }
}
