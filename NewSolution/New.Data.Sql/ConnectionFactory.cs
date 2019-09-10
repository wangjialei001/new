using System;
using System.Text.RegularExpressions;

namespace New.Data.Sql
{
    public class ConnectionFactory
    {
        private bool IsRead(string sql)
        {
            var sqlLower = sql.ToLower();
            var result = Regex.IsMatch(sqlLower, "[ ]*select[ ]") && !Regex.IsMatch(sqlLower, "[ ]*insert[ ]|[ ]*update[ ]|[ ]*delete[ ]");
            return result;
        }
        //private void InitParameters(ref string sql, SugarParameter[] parameters)
        //{
        //    if (parameters.HasValue())
        //    {
        //        foreach (var item in parameters)
        //        {
        //            if (item.Value != null)
        //            {
        //                var type = item.Value.GetType();
        //                if ((type != UtilConstants.ByteArrayType && type.IsArray) || type.FullName.IsCollectionsList())
        //                {
        //                    var newValues = new List<string>();
        //                    foreach (var inValute in item.Value as IEnumerable)
        //                    {
        //                        newValues.Add(inValute.ObjToString());
        //                    }
        //                    if (newValues.IsNullOrEmpty())
        //                    {
        //                        newValues.Add("-1");
        //                    }
        //                    if (item.ParameterName.Substring(0, 1) == ":")
        //                    {
        //                        sql = sql.Replace("@" + item.ParameterName.Substring(1), newValues.ToArray().ToJoinSqlInVals());
        //                    }
        //                    sql = sql.Replace(item.ParameterName, newValues.ToArray().ToJoinSqlInVals());
        //                    item.Value = DBNull.Value;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
