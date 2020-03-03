using Consul;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Consul
{
    public static class ConsulConfig
    {
        public static IWebHost AddConsul(this IWebHost webHost)
        {
            try
            {
                var configuration = webHost.Services.GetService<IConfiguration>();
                IApplicationLifetime appLifeTime = webHost.Services.GetService<IApplicationLifetime>();
                string ip = configuration["ip"];
                Console.WriteLine("命令行输入IP："+ip);
                string port = configuration["port"];
                Console.WriteLine("命令行输入Port：" + port);
                string serviceName = configuration["consul:serviceName"];
                string serviceId = serviceName + Guid.NewGuid().ToString("N");

                string address = configuration["consul:address"];
                string datacenter = configuration["consul:datacenter"];
                //注册
                var consulClientConfig = new ConsulClientConfiguration { Address = new Uri(address), Datacenter = datacenter };
                using (var consulClient = new ConsulClient(config => { config = consulClientConfig; }))
                {
                    AgentServiceRegistration asr = new AgentServiceRegistration()
                    {
                        Address = ip,//ip地址
                        Port = Convert.ToInt32(port),//端口
                        ID = serviceId,//唯一ID
                        Name = serviceName,//组名称
                        Check = new AgentServiceCheck
                        {
                            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60),//60秒后移除
                            HTTP = $"http://{ip}:{port}/api/Health",
                            Interval = TimeSpan.FromSeconds(10),//间隔时间
                            Timeout = TimeSpan.FromSeconds(5),//检测等待时间
                        }
                    };
                    consulClient.Agent.ServiceRegister(asr).Wait();
                }
                //注销
                appLifeTime.ApplicationStopped.Register(() =>
                {
                    using (var consulClient = new ConsulClient(config => { config = consulClientConfig; }))
                    {
                        Console.WriteLine("应用退出，开始从consul注销");
                        consulClient.Agent.ServiceDeregister(serviceId).Wait();
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("注册失败：" + ex.Message);
            }
            return webHost;
        }
    }
}
