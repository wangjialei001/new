using NewLife.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace New.Cache
{
    internal class RedisUtil
    {
        private readonly FullRedis redis;
        public RedisUtil()
        {
            FullRedis.Register();
            redis = new FullRedis("127.0.0.1:6379", "", 0);
        }
        public FullRedis GetRedis()
        {
            return redis;
        }
    }
}
