using Microsoft.AspNetCore.Mvc;
using New.Common;
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
    public class UserMangerController: ControllerBase
    {
        private readonly IUserManagerCore userManager;
        public UserMangerController(IUserManagerCore userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        public async Task<ResultWrapper<UserInfoDto>> GetUserInfo(GetUserInfoDto input)
        {
            return await userManager.GetUserInfo(input);
        }
    }
}
