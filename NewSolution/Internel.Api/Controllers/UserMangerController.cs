using Microsoft.AspNetCore.Mvc;
using New.Common;
using New.Common.Interceptors;
using New.Core;
using New.Model.User;
using NewLife.Log;
using NewLife.RocketMQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserMangerController : ControllerBase
    {
        private readonly IUserManagerCore userManager;
        public UserMangerController(IUserManagerCore userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        public async Task<ResultWrapper<UserInfoDto>> QueryUserInfo([FromBody]GetUserInfoDto input)
        {
            Console.WriteLine("测试");
            return await userManager.GetUserInfo(input);
        }
        /// <summary>
        /// 测试Binder
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationInterceptor]
        public virtual async Task<ResultWrapper<AuthorBase>> QueryAuthorInfo(AuthorBase input)
        {
            return await Task.FromResult(new ResultWrapper<AuthorBase> { Data= input });
        }
        [HttpGet]
        public async Task<string> GetUser(string voucherId)
        {
            return await userManager.GetUser(voucherId);
        }
        [HttpPost]
        public async Task<ResultWrapper<UserInfoDto>> QueryUserInfo1(UserInfoDto input)
        {
            try
            {
                Producer producer = new Producer
                {
                    Topic = "test_Topic",
                    Group = "test_Group",
                    NameServerAddress = "127.0.0.1:9876",
                    Log = XTrace.Log
                };
                var msg = new NewLife.RocketMQ.Protocol.Message
                {
                    Topic = "test_Topic",
                    BodyString = JsonConvert.SerializeObject(input)
                };
                producer.Start();
                var sr = producer.Publish(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return await Task.FromResult(new ResultWrapper<UserInfoDto> { Data = input });
        }
    }
}
