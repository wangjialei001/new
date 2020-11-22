using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using org.apache.zookeeper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static org.apache.zookeeper.ZooDefs;
using static org.apache.zookeeper.ZooKeeper;

namespace ZookConfig
{
    public class ConfigHelper
    {
        //private readonly IConfiguration configuration;
        private readonly int timeout;
        private readonly ZookeeperClient conf;
        public ConfigHelper(string path)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("zooConfig.json");
            var configuration = builder.Build();
            var url = configuration["Url"];
            if (!int.TryParse(configuration["TimeOut"], out timeout))
            {
                timeout = 5000;
            }
            conf = new ZookeeperClient(url, timeout);
            Console.WriteLine("客户端开始连接zookeeper服务器...");
            Console.WriteLine($"连接状态：{conf.ZK.getState()}");
            conf.QueryPath = "/" + path;
        }
        private async Task GetAllNode()
        {
            try
            {
                Console.WriteLine($"连接状态：{conf.ZK.getState()}");
                if (conf.ZK.getState() == States.CONNECTING)
                {
                    Console.WriteLine($"开始初始化：{conf.ZK.getState()}");
                    var dataResult = await conf.ZK.getDataAsync("/");
                    var resultStr = Encoding.UTF8.GetString(dataResult.Data);
                    Console.WriteLine(resultStr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 设置节点
        /// </summary>
        /// <param name="path">节点名称</param>
        /// <param name="value">值</param>
        /// <param name="createMode">1-永久，2-临时</param>
        public async Task SetNode(string path, string value, int createMode = 1)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Path或者Value不能为空！");
            }
            try
            {
                Console.WriteLine($"连接状态：{conf.ZK.getState()}");
                if (conf.ZK.getState() == States.CONNECTING)
                {

                    conf.ConfigData = Encoding.UTF8.GetBytes(value);
                    var pathExist = await conf.ZK.existsAsync(path);
                    if (pathExist == null)
                    {
                        if (createMode == 1)
                            await conf.ZK.createAsync(path, conf.ConfigData, Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                        else if (createMode == 2)
                            await conf.ZK.createAsync(path, conf.ConfigData, Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL);
                    }
                    else
                    {
                        await conf.ZK.setDataAsync(path, conf.ConfigData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<T> GetNode<T>(string path="")
        {
            if (string.IsNullOrEmpty(path))
                path = conf.QueryPath;
            else
                path = "/" + path;
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Path不能为空！");
            }
            var result = default(T);
            try
            {
                if (conf.ZK.getState() == States.CONNECTING)
                {
                    var dataResult = await conf.ZK.getDataAsync(path);
                    var resultStr = Encoding.UTF8.GetString(dataResult.Data);
                    result = JsonConvert.DeserializeObject<T>(resultStr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task ChangeData<T>(Func<string,T> action)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
