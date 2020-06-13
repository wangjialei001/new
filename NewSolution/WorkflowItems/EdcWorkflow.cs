using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowItems.EdcStep;
using WorkflowItems.HelloWorldStep;

namespace WorkflowItems
{
    public class EdcWorkflow : IWorkflow<EdcData>
    {
        public string Id => "EdcWorkflow";

        public int Version => 1;


        public void Build(IWorkflowBuilder<EdcData> builder)
        {
            builder.StartWith<HelloWorld>()
                .If(data => data.Id < 3).Do(then => then.StartWith<PrintMessage>().Input(step => step.Message, data => "Press Id is less then 5"))
                .Then<GoodByeWorld>();
        }
    }
}
