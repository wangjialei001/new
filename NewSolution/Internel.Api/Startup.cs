﻿using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.SyncData;
using Internel.Api.Filters;
using Internel.Api.Injection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using New.Core;
using New.Model.Binder;
using New.Service;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


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
            services.AddScoped<ITransientService, TransientService>();
            services.AddTransient<IScopeService, ScopeService>();
            services.AddSingleton<ISingletonService, SingletonService>();
            services.AddTransient<CusExceptionFilter>();//注入到容器中
            services.AddHostedService<TaskBackgroundService>();
            services.AddSyncData(Configuration);
            services.AddSwaggerGen(c=> {
                //配置第一个Doc
                c.SwaggerDoc("v1", new Info { Title = "My API_1", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Internel.Api.xml");
                c.IncludeXmlComments(xmlPath);
                #region token
                var security = new Dictionary<string, IEnumerable<string>> { { "Blog.Core", new string[] { } }, };
                c.AddSecurityRequirement(security);
                c.AddSecurityDefinition("Blog.Core", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization", // jwt默认的参数名称
                    In = "header", // jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion
                //swagger中控制请求的时候发是否需要在url中增加accesstoken
                c.OperationFilter<HttpHeaderOperation>();
            });
            services.AddMvc(options=> {
                options.Filters.Add(typeof(ResourceFilterAttribute));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddMvcOptions(options => {
                    //options.ModelBinderProviders.Insert(0,new MyModelBinderProvider());
                }).AddControllersAsServices();//否则AuthorizationInterceptor不起作用
            return RegisterAutofac(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.DecryptAndEncrypt();
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
