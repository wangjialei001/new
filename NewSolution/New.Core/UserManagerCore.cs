using Infrastructure.Logger;
using Infrastructure.SyncData.ConfigManager;
using Microsoft.Extensions.Configuration;
using New.Common;
using New.Data;
using New.Data.SqlServer;
using New.Entity;
using New.Model.User;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace New.Core
{
    public class UserManagerCore : IUserManagerCore
    {
        private readonly IConfiguration configuration;
        private readonly IConfig config;
        private readonly ISqlServerSugerHandler sqlServer;
        public UserManagerCore(IConfiguration configuration, IConfig config, ISqlServerSugerHandler sqlServer)
        {
            this.configuration = configuration;
            this.config = config;
            this.sqlServer = sqlServer;
        }

        public async Task<string> GetUser(string voucherId)
        {
            string r = string.Empty;
            //try
            //{
            //    using (var db = BaseDbContext.SqlServerDb("dbSqlServer"))
            //    {
            //        var result = await db.FirstAsync<VoucherSync>(t => t.VoucherId == voucherId);
            //        if (result != null)
            //            r = JsonConvert.SerializeObject(result);
            //        await db.AddAsync(new VoucherSync { VoucherId = Guid.NewGuid().ToString("N"), CreateTime = DateTime.Now });
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            
            await sqlServer.Db("dbSqlServer").AddAsync(new VoucherSync { VoucherId = Guid.NewGuid().ToString("N"), CreateTime = DateTime.Now });
            var result = await sqlServer.Db("dbSqlServer").FirstAsync<VoucherSync>(t => t.VoucherId == voucherId);
            if (result != null)
                r = JsonConvert.SerializeObject(result);
            return r;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResultWrapper<UserInfoDto>> GetUserInfo(GetUserInfoDto input)
        {
            var data = new ResultWrapper<UserInfoDto>
            {
                Code = 200,
                Data = new UserInfoDto
                {
                    Name = input.Name
                }
            };
            string message =JsonConvert.SerializeObject(input);
            await LogCore.LogInfoAsync("Internel.Api", "GetUserInfo", $"Request:{message};Response:{JsonConvert.SerializeObject(data)}");
            //LogCore.LogInfo("Internel.Api", "GetUserInfo", $"Request:{message};Response:{JsonConvert.SerializeObject(data)}");
            //await LogCore.LogErrorAsync("Internel.Api", "GetUserInfo", $"Request:{message};Response:{JsonConvert.SerializeObject(data)}");
            return data;
        }
        private void Log(string message)
        {
            try
            {
                string filename = DateTime.Now.ToString("yyyyMMdd") + ".log";
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"logs");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                Console.WriteLine(path);
                FileInfo file = new FileInfo(Path.Combine(path,filename)); //如果是web程序，这个的变成Http什么的
                StreamWriter sw = null;
                if (!file.Exists)
                {
                    sw = file.CreateText();
                    sw.WriteLine(message);
                }
                else
                {
                    sw = file.AppendText();
                    sw.WriteLine(message);
                }
                sw.Close();
                sw.Flush();
                sw.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
