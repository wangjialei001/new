using Log.Api.AppSettingModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Web;
using NLog.Web.LayoutRenderers;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Log.Api
{
    /*NLog官方地址
    https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-2 */
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
            var config = new ConfigurationBuilder().AddJsonFile("jsConfig.json").Build();
            services.Configure<JsConfigModel>(config);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                //配置第一个Doc
                c.SwaggerDoc("v1", new Info { Title = "My API_1", Version = "v1" });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory,IOptions<JsConfigModel> options,IConfiguration configuration)
        {
            Console.WriteLine(options.Value.Name);

            #region 解决Ubuntu Nginx 代理不能获取IP问题
            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});
            #endregion

            loggerFactory.AddNLog();
            ConfigurationItemFactory.Default.LayoutRenderers
    .RegisterDefinition("aspnet-request-ip", typeof(AspNetRequestIpLayoutRenderer));
            ConfigurationItemFactory.Default.LayoutRenderers
    .RegisterDefinition("rq-ip1", typeof(AspNetLayoutRendererBase1));
            ConfigurationItemFactory.Default.LayoutRenderers
                .RegisterDefinition("rq-ip", typeof(RQIPLayoutRender)); ;
            
            env.ConfigureNLog("Nlog.config");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            var serviceName = configuration["serviceName"];
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //    c.RoutePrefix = "swagger";
            //});

            app.UseSwagger(c =>
            {
                //c.RouteTemplate = "scheduler/api-docs/{documentName}/swagger.json";
                c.RouteTemplate = serviceName + "/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                //c.RoutePrefix = "scheduler/api-docs";
                //c.SwaggerEndpoint("/scheduler/api-docs/v1/swagger.json", "Scheduler API v1");
                c.RoutePrefix = serviceName;
                c.SwaggerEndpoint($"/{serviceName}/v1/swagger.json", "My API_1");
            });


            app.UseSwagger();
            app.UseMvc();
        }
    }
}
