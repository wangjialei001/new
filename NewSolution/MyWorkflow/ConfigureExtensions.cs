using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;
using WorkflowItems;
using WorkflowItems.EdcStep;

namespace MyWorkflow
{
    public static class ConfigureExtensions
    {
        public static IApplicationBuilder UseWorkflow(this IApplicationBuilder app)
        {
            var workflowHost = app.ApplicationServices.GetService(typeof(IWorkflowHost)) as IWorkflowHost;
            workflowHost.RegisterWorkflow<HelloWorldWorkflow>();
            workflowHost.RegisterWorkflow<EdcWorkflow, EdcData>();
            workflowHost.Start();
            //workflowHost.StartWorkflow("HelloWorld");


            var appLifetime = app.ApplicationServices.GetService(typeof(IHostApplicationLifetime)) as IHostApplicationLifetime;
            appLifetime.ApplicationStopping.Register(() =>
            {
                workflowHost.Stop();
            });

            var loader = app.ApplicationServices.GetService(typeof(IDefinitionLoader)) as IDefinitionLoader;
            var config1 = new WorkflowConfig
            {
                Id = "Approval",
                Version = 1,
                Steps = new List<Step> {
                    new Step{
                    Id="LaunchApproval",
                    StepType="WorkflowItems.ApprovalWrokflow,WorkflowItems",
                    NextStepId="ApprovalOne"
                    },
                    new Step{
                    Id="ApprovalOne",
                    StepType="WorkflowItems.ApprovalWrokflow,WorkflowItems"
                    }
                }
            };
            var setting = new JsonSerializerSettings { };
            setting.NullValueHandling = NullValueHandling.Ignore;
            loader.LoadDefinition(JsonConvert.SerializeObject(config1, setting), Deserializers.Json);
            return app;
        }
    }
}
