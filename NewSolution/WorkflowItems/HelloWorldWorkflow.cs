using System;
using WorkflowCore.Interface;
using WorkflowItems.HelloWorldStep;

namespace WorkflowItems
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public string Id => "HelloWorld";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder.StartWith<HelloWorld>().Then<ActiveWorld>().Then<GoodByeWorld>();
        }
    }
}
