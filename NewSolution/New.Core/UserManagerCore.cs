using Infrastructure.Logger;
using Infrastructure.SyncData.ConfigManager;
using Microsoft.Extensions.Configuration;
using New.Common;
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
        public UserManagerCore(IConfiguration configuration, IConfig config)
        {
            this.configuration = configuration;
            this.config = config;
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
                    Name = "Jone"
                }
            };
            string message =JsonConvert.SerializeObject(input);
            Log(message);
            await LogCore.LogInfoAsync("Internel.Api", "GetUserInfo", $"Request:{message};Response:{JsonConvert.SerializeObject(data)}");
            await LogCore.LogErrorAsync("Internel.Api", "GetUserInfo", $"Request:{message};Response:{JsonConvert.SerializeObject(data)}");
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
