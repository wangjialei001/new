using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowItems
{
    public class ApprovedWrokflowStep : StepBody
    {
        public ApprovalMessage approvalMessage { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            //  Console.WriteLine(approval.UserId + "已审批，审批状态：" + approval.State + "；审批意见：" + approval.Reason + "；内容：" + approval.ApprovalMessage.Message);
            Console.WriteLine(approvalMessage.Id + "已审批，审批状态：" + approvalMessage.State + "；审批意见：");
            return ExecutionResult.Next();
        }
    }
}
