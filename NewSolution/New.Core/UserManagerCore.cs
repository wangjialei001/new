using System;
using System.Threading.Tasks;
using New.Common;
using New.Model.User;
using Infrastructure.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Infrastructure.SyncData.ConfigManager;
using Infrastructure.Logger;

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
            await LogCore.LogInfoAsync("Internel.Api", "GetUserInfo", $"Request:{JsonConvert.SerializeObject(input)};Response:{JsonConvert.SerializeObject(data)}");
            await LogCore.LogErrorAsync("Internel.Api", "GetUserInfo", $"Request:{JsonConvert.SerializeObject(input)};Response:{JsonConvert.SerializeObject(data)}");
            return data;
        }
    }
}
