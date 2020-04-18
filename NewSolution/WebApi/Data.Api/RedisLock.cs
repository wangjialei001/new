using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Data.Api
{
    public class RedisLock
    {
        //redis连接管理类
        private readonly static ConnectionMultiplexer connectionMultiplexer = null;
        //redis数据操作类
        private IDatabase database = null;
        public RedisLock()
        {
            database = connectionMultiplexer.GetDatabase();
            lockObj = Guid.NewGuid().ToString();
        }
        static RedisLock()
        {
            connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379,abortConnect=false");
            //ConfigurationOptions config = new ConfigurationOptions
            //{
            //    EndPoints =
            //    {
            //        { "redis0", 6379 },
            //        //{ "redis1", 6380 }
            //    },
            //    CommandMap = CommandMap.Create(new HashSet<string>
            //    { // EXCLUDE a few commands
            //        "INFO", "CONFIG", "CLUSTER",
            //        "PING", "ECHO", "CLIENT"
            //    }, available: false),
            //    KeepAlive = 180,
            //    DefaultVersion = new Version(2, 8, 8),
            //    //Password = "changeme"
            //};
            //connectionMultiplexer = ConnectionMultiplexer.Connect(config);
        }
        private readonly string lockObj = string.Empty;
        private readonly static string lockKey = "lock_key_redis";//锁名
        public void Lock()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"{lockObj}尝试获取锁");
            bool flag = false;
            while (!flag)
            {
                flag = database.LockTake(lockKey, lockObj, TimeSpan.FromSeconds(30));
                if (flag)
                    break;
                Thread.Sleep(1000);
            }
            stopwatch.Stop();
            Console.WriteLine($"{lockObj}成功获取锁;耗时：{stopwatch.ElapsedMilliseconds}");
        }

        public void UnLock()
        {
            Console.WriteLine($"{lockObj}释放锁");
            database.LockRelease(lockKey, lockObj);
        }
    }
}
