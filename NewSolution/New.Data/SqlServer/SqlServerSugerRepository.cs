using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.SyncData;
using SqlSugar;

namespace New.Data.SqlServer
{
    public class SqlServerSugerRepository : SugerRepository, ISqlServerSugerHandler
    {
        internal override SqlSugarClient DbContext { get; set; }
        public ISqlServerSugerHandler Db(string dbKey)
        {
            var connStr = ConfigHelper.GetValue(dbKey);
            this.DbContext = BaseDbContext.InitDataBase(connStr.Split('|').ToList());
            return this;
        }
    }
}
