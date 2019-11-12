using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NewLife.Log;
using NewLife.RocketMQ;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace New.Service
{
    public class TaskBackgroundService : BackgroundService
    {
        private Consumer mq;
        private IConfiguration configuration;
        public TaskBackgroundService(IConfiguration configuration)
        {
            this.configuration = configuration;
            mq = new Consumer()
            {
                Topic = configuration.GetSection("rocket_Topic").Value,
                Group = configuration.GetSection("rocket_Group").Value,
                NameServerAddress = configuration.GetSection("rocket_NameServerAddress").Value,
                BatchSize = 32,
                Log = XTrace.Log
            };
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            mq.OnConsume = (q, ms) =>
            {
                Console.WriteLine($"[{q.BrokerName}@{q.QueueId}]收到消息[{ms.Length}]");
                foreach (var item in ms)
                {
                    XTrace.Log.Info("标签：" + item.Tags + ",消息体：" + item.BodyString);
                    Console.WriteLine("消息体：" + item.BodyString);
                }
                return true;
            };
            mq.Start();
            Console.WriteLine("任务启动。。。");
            return Task.CompletedTask;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            XTrace.Log.Info("服务已停止。");
            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
