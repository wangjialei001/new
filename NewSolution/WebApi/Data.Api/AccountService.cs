using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace Data.Api
{
    /// <summary>
    /// 3秒钟发送60个请求，money为20
    /// 经过测试，如果部署单台服务器,消耗时间为：2020-04-18 18:18:11 234至2020-04-18 18:18:15 232
    /// 开启2个服务实例 消耗时间 2020-04-18 18:06:19 762至2020-04-18 18:06:23 933  或者  2020-04-18 18:06:20 519至2020-04-18 18:06:23 721
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly static object obj = new object();
        private readonly static string objstr = "lock_key";
        private readonly BaseRepository repository;
        public AccountService(BaseRepository _repository)
        {
            repository = _repository;
        }
        public async Task<object> Transfer2()
        {
            lock (objstr)
            {
                Thread.Sleep(100);//模拟操作数据库
                var accounts = repository.Query<AccountModel>("select * from account where id=3");
                var account = accounts.FirstOrDefault();
                if (account.Money > 0)
                {
                    account.Money--;
                    string sql = $"update account set money={account.Money} where id={account.ID}";
                    Thread.Sleep(100);//模拟操作数据库
                    var n = repository.Update(sql);
                }
                Console.WriteLine($"ID={account.ID};Money={account.Money}；{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                Console.WriteLine(obj.GetHashCode());
                Console.WriteLine(this.GetHashCode());
                Console.WriteLine(objstr.GetHashCode());
            }
            return await Task.FromResult(new { Ok = true });
        }
        public async Task<object> Transfer1()
        {
            Thread.Sleep(100);//模拟操作数据库
            var accounts = await repository.QueryAsync<AccountModel>("select * from account where id=3");
            var account = accounts.FirstOrDefault();
            if (account.Money > 0)
            {
                account.Money--;
                string sql = $"update account set money={account.Money} where id={account.ID}";
                Thread.Sleep(100);//模拟操作数据库
                var n = await repository.UpdateAsync(sql);
            }
            Console.WriteLine($"ID={account.ID};Money={account.Money}；{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            return new { Ok = true };
        }
        public async Task<object> Transfer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            RedisLock redisLock = new RedisLock();
            redisLock.Lock();
            try
            {
                Thread.Sleep(100);//模拟操作数据库
                var accounts = await repository.QueryAsync<AccountModel>("select * from account where id=3");
                var account = accounts.FirstOrDefault();
                if (account.Money > 0)
                {
                    account.Money--;
                    string sql = $"update account set money={account.Money} where id={account.ID}";
                    Thread.Sleep(100);//模拟操作数据库
                    var n = await repository.UpdateAsync(sql);
                }
                Console.WriteLine( $"扣减库存成功；ID={account.ID};Money={account.Money}；{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("扣减库存失败；失败原因：" + ex.Message);
            }
            finally
            {
                redisLock.UnLock();
                stopwatch.Stop();
                Console.WriteLine($"扣减库存耗时：{stopwatch.ElapsedMilliseconds}");
            }
            return new { Ok = true };
        }
    }
    public class AccountModel
    {
        public int ID { get; set; }
        public decimal Money { get; set; }
    }
}
