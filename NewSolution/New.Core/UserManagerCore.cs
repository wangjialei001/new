using System;
using System.Threading.Tasks;
using New.Common;
using New.Model.User;
using Infrastructure.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace New.Core
{
    public class UserManagerCore : IUserManagerCore
    {
        private readonly IConfiguration configuration;
        public UserManagerCore(IConfiguration configuration)
        {
            this.configuration = configuration;
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
            await HttpHelper.HttpPostAsync(configuration["logUrl"], JsonConvert.SerializeObject(new
            {
                AppName = "GetUserInfo",
                Level = "info",
                Message = $"Request:{JsonConvert.SerializeObject(input)};Response:{JsonConvert.SerializeObject(data)}"
            }));
            await HttpHelper.HttpPostAsync(configuration["logUrl"], JsonConvert.SerializeObject(new
            {
                AppName = "GetUserInfo",
                Level = "error",
                Message = $"Request:{JsonConvert.SerializeObject(input)};Response:{JsonConvert.SerializeObject(data)}"
            }));
            return data;
        }
    }
}
