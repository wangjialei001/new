using Infrastructure.Logger;
using Infrastructure.SyncData;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace WindSerivce
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            Console.WriteLine(Directory.GetCurrentDirectory());
            IServiceCollection services = new ServiceCollection();
            services.AddSyncData(configuration);

            var rc = HostFactory.Run(x =>
            {
                x.Service<TownCrier>(s =>
                {
                    s.ConstructUsing(name => new TownCrier());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("Sample Topshelf Host");
                x.SetDisplayName("MyService100");
                x.SetServiceName("MyService100");
                x.OnException(ex=> {
                    Console.WriteLine(ex.Message);
                });
            });
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
    public class TownCrier
    {
        readonly Timer timer;
        public TownCrier()
        {
            timer = new Timer(5000) { AutoReset = true };
            timer.Elapsed += (sender, eventArgs) => Action();
        }
        public void Start() { timer.Start(); }
        public void Stop() { timer.Stop(); }
        private void Action()
        {
            Task.Run(()=> {
                LogCore.LogInfo("WindSerivce", "TownCrier", $"MyService100 Run {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });
        }
    }
}
