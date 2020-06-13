using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Internel.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using New.Core;
using Newtonsoft.Json.Linq;

namespace Internel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[CusExceptionFilter]
    [CusFactoryFilter(typeof(CusExceptionFilter))]
    //[ServiceFilter(typeof(CusExceptionFilter)))]//和IFactoryFilter一样
    //[TypeFilter(typeof(CusExceptionFilter))))]//不需要再容器中注入
    public class ValuesController : ControllerBase
    {
        private readonly IScopeService scopeService;
        private readonly ITransientService transientService;
        public ValuesController(IScopeService _scopeService, ITransientService _transientService)
        {
            transientService = _transientService;
            scopeService = _scopeService;
        }
        // GET api/values
        [HttpGet]
        //[ResultFilter]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            Console.WriteLine($"testService哈希code是:{transientService.GetHashCode()}");
            await scopeService.SayHello(transientService);
            //Task.Run(()=> {
            //    Console.WriteLine($"testService:{testService.GetHashCode()}");
            //    testService.SayHello();
            //});
            //Console.WriteLine($"singletonService哈希code是:{singletonService.GetHashCode()}");
            //singletonService.SayHello();
            Console.WriteLine("get无参数请求");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [EncryRequired]
        public ActionResult<string> Get(int id)
        {
            Console.WriteLine("get请求");
            throw new Exception("错误");
            return id.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [Route("postUser")]
        [EncryRequired]
        [HttpPost]
        public TestUser PostUser(TestUser tests)
        {
            //return tests.FirstOrDefault();
            return tests;
        }
    }
    public class TestUser
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }
}
