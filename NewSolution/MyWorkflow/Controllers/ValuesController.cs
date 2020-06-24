using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Services;
using WorkflowItems.EdcStep;

namespace MyWorkflow.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ValuesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ValuesController> _logger;
        private readonly IWorkflowController _workflowService;
        public ValuesController(ILogger<ValuesController> logger, IWorkflowController workflowService)
        {
            _logger = logger;
            _workflowService = workflowService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Summaries;
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
