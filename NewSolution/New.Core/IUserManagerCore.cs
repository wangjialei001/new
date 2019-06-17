using New.Common;
using New.Model.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace New.Core
{
    public interface IUserManagerCore
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResultWrapper<UserInfoDto>> GetUserInfo(GetUserInfoDto input);
    }
}
