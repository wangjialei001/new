using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
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
            workflowHost.StartWorkflow("HelloWorld");
            

            var appLifetime = app.ApplicationServices.GetService(typeof(IHostApplicationLifetime)) as IHostApplicationLifetime;
            appLifetime.ApplicationStopping.Register(() =>
            {
                workflowHost.Stop();
            });

            return app;
        }
    }
}
