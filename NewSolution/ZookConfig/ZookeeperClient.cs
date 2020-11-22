using org.apache.zookeeper;
using org.apache.zookeeper.data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static org.apache.zookeeper.Watcher.Event;

namespace ZookConfig
{
    public class ZookeeperClient
    {
        public ZooKeeper ZK { get; set; }

        // 配置项
        public string QueryPath { get; set; } = "/Configuration";
        //节点状态信息
        public Stat Stat { get; set; }

        // 配置数据
        public byte[] ConfigData { get; set; } = null;


        public ZookeeperClient(string serviceAddress, int timeout)
        {
            ZK = new ZooKeeper(serviceAddress, timeout, new ConfigServiceWatcher(this));

        }

        public ZookeeperClient(string serviceAddress, int timeout, long sessionId, byte[] sessionPasswd)
        {
            ZK = new ZooKeeper(serviceAddress, timeout, new ConfigServiceWatcher2(this), sessionId, sessionPasswd);

        }

        // 读取节点的配置数据
        public async Task<string> ReadConfigDataAsync()
        {
            if (this.ZK == null)
            {
                return string.Empty;
            }

            var stat = await ZK.existsAsync(QueryPath, true);

            if (stat == null)
            {
                return string.Empty;
            }

            this.Stat = stat;

            var dataResult = await ZK.getDataAsync(QueryPath, true);

            return Encoding.UTF8.GetString(dataResult.Data);
        }



        public class ConfigServiceWatcher : Watcher
        {
            private ZookeeperClient _cs = null;

            public ConfigServiceWatcher(ZookeeperClient cs)
            {
                _cs = cs;
            }

            public override async Task process(WatchedEvent @event)
            {
                try
                {
                    Console.WriteLine($"Zookeeper链接成功:{@event.getState() == KeeperState.SyncConnected}");

                    if (@event.get_Type() == EventType.NodeDataChanged)
                    {//Node值修改通知
                        var data = await _cs.ReadConfigDataAsync();

                        Console.WriteLine("{0}收到修改此节点【{1}】值的通知，其值已被改为【{2}】。", Environment.NewLine, _cs.QueryPath, data);
                    }
                    else if (@event.get_Type() == EventType.NodeCreated)
                    {//Node创建通知
                        var data = await _cs.ReadConfigDataAsync();
                        Console.WriteLine($"{Environment.NewLine}收到新创建的节点【{_cs.QueryPath}】，值【{data}】");
                    }
                    else if (@event.get_Type() == EventType.NodeDeleted)
                    {//Node删除通知
                        var data = await _cs.ReadConfigDataAsync();
                        Console.WriteLine($"{Environment.NewLine}收到{_cs.QueryPath}删除节点通知，值【{data}】");
                    }
                    else if (@event.get_Type() == EventType.NodeChildrenChanged)
                    {//子节点修改通知
                        var data = await _cs.ReadConfigDataAsync();
                        Console.WriteLine($"{Environment.NewLine}收到子节点{_cs.QueryPath}修改通知，值【{data}】");
                    }
                    //else
                    //{
                    //    var data = await _cs.ReadConfigDataAsync();
                    //    Console.WriteLine($"{Environment.NewLine}收到其他通知{_cs.QueryPath}通知，值【{data}】");
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public class ConfigServiceWatcher2 : Watcher
        {
            private ZookeeperClient _cs = null;

            public ConfigServiceWatcher2(ZookeeperClient cs)
            {
                _cs = cs;
            }

            public override async Task process(WatchedEvent @event)
            {
                Console.WriteLine($"Zookeeper链接成功:{@event.getState() == KeeperState.SyncConnected}");

                if (@event.get_Type() == EventType.NodeDataChanged)
                {
                    var data = await _cs.ReadConfigDataAsync();

                    Console.WriteLine("{0}收到修改此节点【{1}】值的通知，其值已被改为【{2}】。", Environment.NewLine, _cs.QueryPath, data);
                }
            }
        }

        // 关闭ZooKeeper连接
        // 释放资源
        public async Task Close()
        {
            if (this.ZK != null)
            {
                await ZK.closeAsync();
            }

            this.ZK = null;
        }



    }
}
