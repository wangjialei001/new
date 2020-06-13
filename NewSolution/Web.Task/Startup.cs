using Hangfire;
using Hangfire.MySql;
using Hangfire.MySql.Core;
using Hangfire.SqlServer;
using Infrastructure.SyncData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using Web.Tasks.MessageTask;

namespace Web.Tasks
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
            services.AddSyncData(Configuration);
            // Add Hangfire services.
            //services.AddHangfire(configuration => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            //    {
            //        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //        QueuePollInterval = TimeSpan.Zero,
            //        UseRecommendedIsolationLevel = true,
            //        UsePageLocksOnDequeue = true,
            //        DisableGlobalLocks = true
            //    }));
            string hangfireMysqlConnStr = Configuration.GetConnectionString("HangfireMySqlConnection");
            services.AddHangfire(configuration => configuration.UseStorage(
                new MySqlStorage(
                    hangfireMysqlConnStr,
                    new MySqlStorageOptions
                    {
                        TransactionIsolationLevel = IsolationLevel.ReadCommitted, // 事务隔离级别。默认是读取已提交。
                        QueuePollInterval = TimeSpan.FromSeconds(15),             //- 作业队列轮询间隔。默认值为15秒。
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),       //- 作业到期检查间隔（管理过期记录）。默认值为1小时。
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),      //- 聚合计数器的间隔。默认为5分钟。
                        PrepareSchemaIfNecessary = true,                          //- 如果设置为true，则创建数据库表。默认是true。
                        DashboardJobListLimit = 50000,                            //- 仪表板作业列表限制。默认值为50000。
                        TransactionTimeout = TimeSpan.FromMinutes(1),             //- 交易超时。默认为1分钟。
                        TablePrefix = "Hangfire"                                  //- 数据库中表的前缀。默认为none
                    })));
            // Add the processing server as IHostedService
            services.AddHangfireServer();
            services.AddTransient<IMessageSerivce, MessageSerivce>();//注册
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBackgroundJobClient backgroundJobs)
        {
            app.UseHangfireDashboard();
            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            RecurringJob.AddOrUpdate(() => Console.WriteLine($"start hangfire {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}"), "0/10 * * * * ? ");
            RecurringJob.AddOrUpdate<IMessageSerivce>(a => a.SendMessage("hello"), "0 0/5 * * * ?");//使用
            RecurringJob.AddOrUpdate<IMessageSerivce>(a => a.ReceiveMessage("hello"), "0 0/5 * * * ?");//使用
            app.UseMvc();
        }
    }
}
