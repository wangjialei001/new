using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace New.Data.SqlServer
{
    public interface ISqlServerSugerHandler:ISugerHandler
    {
        ISqlServerSugerHandler Db(string dbKey);
    }
}
