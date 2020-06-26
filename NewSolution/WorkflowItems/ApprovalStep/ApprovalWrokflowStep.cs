using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowItems
{
    public class ApprovalWrokflowStep : StepBody
    {
        public ApprovalMessage approvalMessage { get; set; }
        //public int UserId { get; set; }
        //public string Message { get; set; }
        //public int State { get; set; }
        //public int Id { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine(approvalMessage.UserId + "，发起申请，内容：" + approvalMessage.Message);
            return ExecutionResult.Next();
        }
    }
}
