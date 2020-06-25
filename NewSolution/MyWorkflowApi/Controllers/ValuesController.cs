using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowItems.EdcStep;

namespace MyWorkflowApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly ILogger<ValuesController> _logger;
        private readonly IWorkflowController _workflowService;
        public ValuesController(ILogger<ValuesController> logger, IWorkflowController workflowService)
        {
            _logger = logger;
            _workflowService = workflowService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
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

        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            await _workflowService.StartWorkflow("EdcWorkflow", new EdcData { Id = id });
            return "";
        }
        [HttpGet]
        public async Task<string> GetApproval(string messsage)
        {
            await _workflowService.StartWorkflow("Approval");
            return "";
        }
    }
}
