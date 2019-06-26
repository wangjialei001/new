using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.SyncData;
using Internel.Api.Injection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Internel.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSyncData(Configuration:Configuration);
            services.AddSwaggerGen(c=> {
                //配置第一个Doc
                c.SwaggerDoc("v1", new Info { Title = "My API_1", Version = "v1" });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            return RegisterAutofac(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSyncData();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "swagger";

            });
            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
        /// <summary>
        /// 使用Autofac 替换默认IOC
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {
            //实例化Autofac容器
            ContainerBuilder builder = new ContainerBuilder();
            //将Services中的服务填充到Autofac中
            builder.Populate(services);
            //新模块组件注册
            builder.RegisterModule<Evaluation>();
            //AspectCore 针对 Autofac 实现AOP的处理
            builder.RegisterDynamicProxy();
            //创建容器
            IContainer Container = builder.Build();
            //第三方IOC接管 core内置DI容器 
            return new AutofacServiceProvider(Container);
        }
    }
}
