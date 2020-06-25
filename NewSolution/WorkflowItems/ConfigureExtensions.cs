using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using System.Collections.Generic;
using WorkflowCore.Interface;
using WorkflowCore.Primitives;
using WorkflowCore.Services;
using WorkflowCore.Services.DefinitionStorage;
using WorkflowItems.EdcStep;

namespace WorkflowItems
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

            
            //var appLifetime = app.ApplicationServices.GetService(typeof(IHostApplicationLifetime)) as IHostApplicationLifetime;
            var appLifetime = app.ApplicationServices.GetService(typeof(IApplicationLifetime)) as IApplicationLifetime;
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
