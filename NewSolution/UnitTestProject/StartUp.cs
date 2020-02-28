using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using New.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Configuration;
using New.Model;
using Xunit.Abstractions;
using Newtonsoft.Json;

namespace UnitTestProject
{
    public class DependencySetupFixture
    {
        public DependencySetupFixture()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<CustomInterceptorAttribute>();//注入到容器中
            //根据属性注入来配置全局拦截器
            //serviceCollection.ConfigureDynamicProxy(config =>
            //{
            //    config.Interceptors.AddTyped<CustomInterceptorAttribute>();//全局拦截的拦截器
            //});
            serviceCollection.AddTransient<ICustomService, CustomService>();
            ServiceProvider = serviceCollection.BuildDynamicProxyProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }

        
    }
    public class StartUp : IClassFixture<DependencySetupFixture>
    {
        private ICustomService customService;
        private ServiceProvider serviceProvider;
        public StartUp(DependencySetupFixture fixture, ITestOutputHelper output)
        {
            serviceProvider = fixture.ServiceProvider;
            this.output = output;
        }
        [Fact]
        public void call()
        {
            customService = serviceProvider.GetRequiredService<ICustomService>();
            Customer customer = new Customer { Id=1,Name="gooney",Birthday=DateTime.Now};
            output.WriteLine(JsonConvert.SerializeObject(customer));
            var result = customService.Call(9, customer);
            output.WriteLine(JsonConvert.SerializeObject(result));
        }


        public ITestOutputHelper output;
    }
}
