using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowItems
{
    public class ApprovalWrokflow : StepBody
    {
        public ApprovalMessage approvalMessage;
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("执行ApprovalWrokflow");
            if (approvalMessage != null)
                Console.WriteLine(approvalMessage);
            return ExecutionResult.Next();
        }
    }
}
