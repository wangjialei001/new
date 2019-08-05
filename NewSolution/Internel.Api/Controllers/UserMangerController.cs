using Microsoft.AspNetCore.Mvc;
using New.Common;
using New.Common.Interceptors;
using New.Core;
using New.Model.User;
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
    }
}
