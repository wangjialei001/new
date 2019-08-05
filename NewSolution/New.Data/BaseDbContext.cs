using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Infrastructure.SyncData;
using New.Data.SqlServer;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace New.Data
{
    public class BaseDbContext
    {
        private static ConcurrentDictionary<string, SqlServerSugerRepository> sugarSqlServerDic = new ConcurrentDictionary<string, SqlServerSugerRepository>();
        public static SqlServerSugerRepository SqlServerDb(string dbKey)
        {
            SqlServerSugerRepository sqlServerSugerRepository = null;
            if (!sugarSqlServerDic.TryGetValue(dbKey, out sqlServerSugerRepository) || sqlServerSugerRepository == null)
            {
                var connStr = ConfigHelper.GetValue(dbKey);
                try
                {
                    sqlServerSugerRepository = new SqlServerSugerRepository();
                    sqlServerSugerRepository.DbContext = InitDataBase(connStr.Split('|').ToList());
                    sugarSqlServerDic.AddOrUpdate(dbKey, sqlServerSugerRepository, (_dbKey,_sqlSuger) => {
                        return sqlServerSugerRepository;
                    });
                }
                catch (Exception ex)
                {

                }
            }
            return sqlServerSugerRepository;
        }
        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="listConn">连接字符串</param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static SqlSugarClient InitDataBase(List<string> listConn, DbType dbType = DbType.SqlServer)
        {
            var connStr = "";//主库
            var slaveConnectionConfigs = new List<SlaveConnectionConfig>();//从库集合
            for (var i = 0; i < listConn.Count; i++)
            {
                if (i == 0)
                {
                    connStr = listConn[i];//主数据库连接
                }
                else
                {
                    slaveConnectionConfigs.Add(new SlaveConnectionConfig()
                    {
                        HitRate = i * 2,
                        ConnectionString = listConn[i]
                    });
                }
            }

            //如果配置了 SlaveConnectionConfigs那就是主从模式,所有的写入删除更新都走主库，查询走从库，
            //事务内都走主库，HitRate表示权重 值越大执行的次数越高，如果想停掉哪个连接可以把HitRate设为0 
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connStr,
                DbType = dbType,
                IsAutoCloseConnection = true,
                SlaveConnectionConfigs = slaveConnectionConfigs,
                IsShardSameThread = false,
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    EntityService = (property, column) =>
                    {
                        var attributes = property.GetCustomAttributes(true);//get all attributes     
                        if (attributes.Any(it => it is KeyAttribute))//根据自定义属性    
                        {
                            column.IsPrimarykey = true;
                        }
                        if (attributes.Any(it => it is ColumnAttribute))
                        {
                            column.DbColumnName = (attributes.First(it => it is ColumnAttribute) as ColumnAttribute).Name;
                        }
                    },
                    EntityNameService = (type, entity) =>
                    {
                        var attributes = type.GetCustomAttributes(true);
                        if (attributes.Any(it => it is TableAttribute))
                        {
                            entity.DbTableName = (attributes.First(it => it is TableAttribute) as TableAttribute).Name;
                        }
                    }
                }
            });
            db.Ado.CommandTimeOut = 30000;//设置超时时间
            db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
            {
                //LogHelper.WriteLog($"执行时间：{db.Ado.SqlExecutionTime.TotalMilliseconds}毫秒 \r\nSQL如下：{sql} \r\n参数：{GetParams(pars)} ", "SQL执行");
            };
            db.Aop.OnLogExecuting = (sql, pars) => //SQL执行前事件
            {
                if (db.TempItems == null) db.TempItems = new Dictionary<string, object>();
            };
            db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                //LogHelper.WriteLog($"SQL错误:{exp.Message}\r\nSQL如下：{exp.Sql}", "SQL执行");
                Console.WriteLine($"SQL错误:{exp.Message}\r\nSQL如下：{exp.Sql}", "SQL执行");
                Console.WriteLine(exp.StackTrace);
                //GwHomeLogger.LogErrorAsync("OpenApi", "执行Sql", $"{exp.Message},{exp.Sql}", exp, exp.StackTrace);
                throw new Exception(exp.Message);
            };
            //db.Aop.OnExecutingChangeSql = (sql, pars) => //SQL执行前 可以修改SQL
            //{
            //    return new KeyValuePair<string, SugarParameter[]>(sql, pars);
            //};
            db.Aop.OnDiffLogEvent = (it) => //可以方便拿到 数据库操作前和操作后的数据变化。
            {
                //var editBeforeData = it.BeforeData;
                //var editAfterData = it.AfterData;
                //var sql = it.Sql;
                //var parameter = it.Parameters;
                //var data = it.BusinessData;
                //var time = it.Time;
                //var diffType = it.DiffType;//枚举值 insert 、update 和 delete 用来作业务区分

                //你可以在这里面写日志方法
            };
            return db;
        }
    }
}
