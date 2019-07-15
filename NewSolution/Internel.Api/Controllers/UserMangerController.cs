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
    [Route("api/[controller]")]
    [ApiController]
    public class UserMangerController : ControllerBase
    {
        private readonly IUserManagerCore userManager;
        public UserMangerController(IUserManagerCore userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("QueryUserInfo")]
        public async Task<ResultWrapper<UserInfoDto>> QueryUserInfo([FromBody]GetUserInfoDto input)
        {
            return await userManager.GetUserInfo(input);
        }
        /// <summary>
        /// 测试Binder
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("QueryAuthorInfo")]
        [AuthorizationInterceptor]
        public async Task<ResultWrapper<Author<Book>>> QueryAuthorInfo([FromBody]Author<Book> input)
        {
            return await Task.FromResult(new ResultWrapper<Author<Book>> { Data= input });
        }
    }
}
