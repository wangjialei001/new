using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowItems;
using WorkflowItems.EdcStep;
using WorkflowItems.HelloWorldStep;

namespace MyWorkflow
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddWorkflowDSL();
            //services.AddTransient<PrintMessage>();
            //services.AddTransient<ActiveWorld>();
            //services.AddTransient<GoodByeWorld>();
            //services.AddTransient<HelloWorld>();
            //services.AddTransient<StartScheduleStep>();
            //services.AddWorkflow(x => {
            //    x.UseMySQL(@"Server=127.0.0.1;Database=workflow;User=root;Password=123456;", true, true);
            //});
            //services.AddWorkflow();
            services.AddLogging();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            
            //app.UseWorkflow();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
