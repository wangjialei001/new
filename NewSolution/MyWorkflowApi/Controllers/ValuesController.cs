using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWorkflow.Entity;
using WorkflowCore.Interface;
using WorkflowItems;
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
        public async Task<string> GetApproval(string messsage,int id)
        {
            await _workflowService.StartWorkflow("Approval", new ApprovalMessage { Message = "Gooney发起申请", State = 0 ,UserId=1,Id=id});
            //await _workflowService.StartWorkflow("Approval", "Gooney发起申请");
            return "";
        }
        [HttpPost]
        public async Task<string> UserStartApproval()
        {
            var msg=TestData.ApprovalInfos.FirstOrDefault();
            await _workflowService.StartWorkflow("Approval", new ApprovalMessage { Message = msg.Message, UserId = msg.UserId, Id = msg.Id });
            
            return TestData.Users.FirstOrDefault(t => t.Id == msg.UserId).Name + ":发起申请：" + msg.Message;
        }
        [HttpPost]
        public async Task<string> UserApprovaled(UserApprovaled input)
        {
            var aprovalFlow = TestData.ApprovalFlowInfos.FirstOrDefault(t => t.UserId == input.UserId && t.State == 0 && t.ApprovalId == input.ApprovalId);
            var aprovalInfo=TestData.ApprovalInfos.FirstOrDefault(t => t.State == 0 && t.CurrentOrder == aprovalFlow.Order && t.Id== aprovalFlow.ApprovalId);

            await _workflowService.StartWorkflow("Approval", new ApprovalMessage
            {
                Message = aprovalInfo.Message,
                UserId = aprovalInfo.UserId,
                Id = aprovalInfo.Id,
                ApprovalFlow = new ApprovalFlow
                {
                    UserId = input.UserId,
                    Reason = input.Remark,
                    State = input.State
                }
            });
            aprovalFlow.State = input.State;
            aprovalFlow.Remark = input.Remark;
            aprovalInfo.CurrentOrder = aprovalInfo.CurrentOrder + 1;
            return TestData.Users.FirstOrDefault(t => t.Id == input.UserId).Name + ":已经审批：" + aprovalInfo.Message + input.State + ";备注：" + input.Remark;
        }
    }
    public class UserApprovaled
    {
        public int UserId { get; set; }
        public int State { get; set; }
        public string Remark { get; set; }
        public int ApprovalId { get; set; }
    }
}
