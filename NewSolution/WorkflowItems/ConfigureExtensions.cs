using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Persistence.EntityFramework.Models;
using WorkflowCore.Persistence.EntityFramework.Services;
using WorkflowCore.Primitives;
using WorkflowCore.Services;
using WorkflowCore.Services.DefinitionStorage;
using WorkflowItems.CusTable;
using WorkflowItems.EdcStep;
using WorkflowItems.HelloWorldStep;

namespace WorkflowItems
{
    public class AddWorkflowItem
    {
        public string MySqlConnectionStr { get; set; }
    }
    public class WorkflowDBContext : DbContext
    {
        public WorkflowDBContext(DbContextOptions<WorkflowDBContext> option) : base(option) { }

        public DbSet<TbWorkflowItem> TbWorkflowItems { get; set; }
    }
    public static class ConfigureExtensions
    {
        public static IServiceCollection AddWorkflowItem(this IServiceCollection services, AddWorkflowItem items)
        {
            //services.AddTransient<PrintMessage>();
            //services.AddTransient<ActiveWorld>();
            //services.AddTransient<GoodByeWorld>();
            //services.AddTransient<HelloWorld>();
            //services.AddTransient<StartScheduleStep>();
            services.AddTransient<ApprovalWrokflowStep>();

            if (!string.IsNullOrEmpty(items.MySqlConnectionStr))
            {
                services.AddWorkflow(x =>
                {
                    x.UseMySQL(items.MySqlConnectionStr, true, true);
                });
                services.AddDbContext<WorkflowDBContext>(x => x.UseMySql(items.MySqlConnectionStr));
            }
            return services;
        }
        public static IApplicationBuilder UseWorkflow(this IApplicationBuilder app)
        {
            var workflowHost = app.ApplicationServices.GetService(typeof(IWorkflowHost)) as IWorkflowHost;
            //workflowHost.RegisterWorkflow<HelloWorldWorkflow>();
            //workflowHost.RegisterWorkflow<EdcWorkflow, EdcData>();
            //workflowHost.RegisterWorkflow<EdcWorkflow, EdcData>();
            //workflowHost.StartWorkflow("HelloWorld");


            //var appLifetime = app.ApplicationServices.GetService(typeof(IHostApplicationLifetime)) as IHostApplicationLifetime;
            var appLifetime = app.ApplicationServices.GetService(typeof(IApplicationLifetime)) as IApplicationLifetime;
           
            appLifetime.ApplicationStopping.Register(() =>
            {
                workflowHost.Stop();
            });

            //var loader = app.ApplicationServices.GetService(typeof(IDefinitionLoader)) as IDefinitionLoader;
            //var config1 = new WorkflowConfig
            //{
            //    Id = "Approval",
            //    Version = 1,
            //    Steps = new List<Step> {
            //        new Step{
            //        Id="LaunchApproval",
            //        StepType="WorkflowItems.ApprovalWrokflowStep,WorkflowItems",
            //        NextStepId="ApprovalOne"
            //        },
            //        new Step{
            //        Id="ApprovalOne",
            //        StepType="WorkflowItems.ApprovalWrokflowStep,WorkflowItems"
            //        }
            //    }
            //};
            //var setting = new JsonSerializerSettings { };
            //setting.NullValueHandling = NullValueHandling.Ignore;
            //string configStr = JsonConvert.SerializeObject(config1, setting);
            //loader.LoadDefinition(configStr, Deserializers.Json);
            #region 操作数据库
            var dbContext = app.ApplicationServices.GetService(typeof(WorkflowDBContext)) as WorkflowDBContext;
            var items = dbContext.TbWorkflowItems.Where(t => t.IsDeleted == 0).ToList();
            var loader = app.ApplicationServices.GetService(typeof(IDefinitionLoader)) as IDefinitionLoader;

            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.Item))
                {
                    if (!string.IsNullOrEmpty(item.Item))
                        loader.LoadDefinition(item.Item, Deserializers.Json);
                }
            }
            #endregion

            workflowHost.Start();
            return app;
        }
    }
}
