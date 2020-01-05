using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.WebApi.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Client.WebApi
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
            services.AddMvcCore(config => {
                config.Filters.Add(new TestAuthorizationFilter());
            })
            .AddAuthorization()
            .AddJsonFormatters();
            //指定认证方案
            services.AddAuthentication("Bearer")
                //添加Token验证服务到DI
                //.AddIdentityServerAuthentication(options =>
                .AddJwtBearer("Bearer", options=>
                {
                    //指定授权地址
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "api";
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //添加认证中间件到Pipeline
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
