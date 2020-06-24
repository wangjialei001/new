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
                .If(data => data.Id < 3).Do(then => then.StartWith<PrintMessage>().Input(step => step.Message, data => "Id小于3"))
                //.If(data => data.Id >= 3 && data.Id < 5).Do(data => data.StartWith<StartScheduleStep>().Schedule(StartScheduleStep => TimeSpan.FromSeconds(10)).Do(StartScheduleStep => Console.WriteLine("5秒执行StartScheduleStep")))
                .If(data => data.Id >= 3 && data.Id < 5).Do(then => then.StartWith<PrintMessage>().Input(step => step.Message, data => "Id大于等于3，小于5"))
                .If(data => data.Id >= 5).Do(schedule=>schedule.StartWith<PrintMessage>().Delay(schdedule=> TimeSpan.FromSeconds(10)).Then(then=>Console.WriteLine("大于等于5")))
                .Then<GoodByeWorld>();
        }
    }
}
