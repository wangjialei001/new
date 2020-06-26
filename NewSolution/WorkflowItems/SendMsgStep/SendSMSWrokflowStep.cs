using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowItems.SendMsgStep;

namespace WorkflowItems
{
    public class SendSMSWrokflowStep : StepBody
    {
        public SendSMS msg;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine(msg);
            return ExecutionResult.Next();
        }
    }
}
