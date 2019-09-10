using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace New.Service
{
    public class TaskBackgroundService : BackgroundService
    {
        public TaskBackgroundService()
        {

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("任务启动。。。");
            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
