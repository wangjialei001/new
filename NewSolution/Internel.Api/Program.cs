using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Internel.Api.Consul;
using Microsoft.Extensions.Configuration;

namespace Internel.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();
            CreateWebHostBuilder(args).UseConfiguration(config).Build().AddConsul().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
