using System;
using System.Collections.Generic;
using System.Text;

namespace New.Cache
{
    public class CacheHelper
    {
        private static RedisUtil redisUtil = new RedisUtil();
        public static long GenerateId(string prefix = "")
        {
            string time = DateTime.Now.ToString("yyMMddHHmm");
            var generateId = redisUtil.GetRedis().Increment("GenerateId",1);
            var id = prefix + time + generateId;
            return long.Parse(id);
        }
    }
}
