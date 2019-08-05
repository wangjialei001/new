using Microsoft.Extensions.Configuration;
using New.Common.Sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace New.Data
{
    public class SugerRepository:ISugerHandler
    {
        private readonly IConfiguration configuration;
        public SugerRepository() { }
        public SugerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public SqlSugarClient DbContext { get; set; }

        public int Add<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Add<T>(List<T> entitys) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Add<T>(Dictionary<string, object> keyValues) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync<T>(T entity) where T : class, new()
        {
            try
            {
                var result = await DbContext.Insertable(entity).ExecuteCommandAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> AddAsync<T>(List<T> entitys) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync<T>(Dictionary<string, object> keyValues) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public bool AddColumn(string tableName, DbColumnInfo column)
        {
            throw new NotImplementedException();
        }

        public bool AddPrimaryKey(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public bool AddReturnBool<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public bool AddReturnBool<T>(List<T> entitys) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddReturnBoolAsync<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddReturnBoolAsync<T>(List<T> entitys) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public T AddReturnEntity<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<T> AddReturnEntityAsync<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int AddReturnIdentity<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddReturnIdentityAsync<T>(T entity) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> AvgAsync<T>(string field) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public bool BackupDataBase(string databaseName, string fullFileName)
        {
            throw new NotImplementedException();
        }

        public bool BackupTable(string oldTableName, string newTableName, int maxBackupDataRows = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public bool CreateTable(string tableName, List<DbColumnInfo> columns, bool isCreatePrimaryKey = true)
        {
            throw new NotImplementedException();
        }

        public string CustomNumber<T>(string key, string prefix = "", int fixedLength = 4, string dateFomart = "") where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<string> CustomNumber<T>(string key, int num, string prefix = "", int fixedLength = 4, string dateFomart = "") where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(T entity, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(List<T> entity, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(Expression<Func<T, bool>> where, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<T>(T entity, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<T>(List<T> entity, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int DeleteByPrimary<T>(List<object> primaryKeyValues, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteByPrimaryAsync<T>(List<object> primaryKeyValues, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int DeleteIn<T>(List<dynamic> inValues) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteInAsync<T>(List<dynamic> inValues) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool DropColumn(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public bool DropConstraint(string tableName, string constraintName)
        {
            throw new NotImplementedException();
        }

        public bool DropTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSql(string sql, params SugarParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlAsync(string sql, params SugarParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlTrans(string sql, params SugarParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlTransAsync(string sql, params SugarParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public List<T> FieldNameIn<T>(string inFieldName, List<dynamic> inValues, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FieldNameInAsync<T>(string inFieldName, List<dynamic> inValues, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TResult> FirstAsync<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public async Task<T> FirstAsync<T>(Expression<Func<T, bool>> whereLambda) where T : class, new()
        {
            return await DbContext.Queryable<T>().With(SqlWith.NoLock).Where(whereLambda).FirstAsync();
        }

        public List<DbColumnInfo> GetColumnInfosByTableName(string tableName, bool isCache = true)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDataBaseTime()
        {
            throw new NotImplementedException();
        }

        public List<string> GetIsIdentities(string tableName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetPrimaries(string tableName)
        {
            throw new NotImplementedException();
        }

        public List<DbTableInfo> GetTableInfoList(bool isCache = true)
        {
            throw new NotImplementedException();
        }

        public List<DbTableInfo> GetViewInfoList(bool isCache = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GroupByAsync<T>(Expression<Func<T, object>> groupByLambda, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> GroupByAsync<T, TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, object>> groupByLambda, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<T> In<T>(Expression<Func<T, object>> expression, object[] inValues, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<T> In<T>(List<dynamic> values) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> InAsync<T>(Expression<Func<T, object>> expression, object[] inValues, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> InAsync<T>(List<dynamic> values) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public bool InvokeTransactionScope(IsolationLevel level = IsolationLevel.ReadCommitted, string message = "添加成功", params Func<bool>[] serviceActions)
        {
            throw new NotImplementedException();
        }

        public bool IsAnyColumn(string tableName, string column)
        {
            throw new NotImplementedException();
        }

        public bool IsAnyConstraint(string constraintName)
        {
            throw new NotImplementedException();
        }

        public bool IsAnySystemTablePermissions()
        {
            throw new NotImplementedException();
        }

        public bool IsAnyTable(string tableName, bool isCache = true)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> whereLambda) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public bool IsIdentity(string tableName, string column)
        {
            throw new NotImplementedException();
        }

        public bool IsPrimaryKey(string tableName, string column)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> MaxAsync<T, TResult>(string field) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TResult> MinAsync<T, TResult>(string field) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public T Query<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public TResult Query<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<T> QueryAsync<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TResult> QueryAsync<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int QueryCount<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<int> QueryCountAsync<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public DataSet QueryDataSet(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public DataTable QueryDataTable<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public DataTable QueryDataTable(string sql)
        {
            throw new NotImplementedException();
        }

        public DataTable QueryDataTable(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> QueryDataTableAsync<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<DataTable, int> QueryDataTablePageList<T>(QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<DataTable, int>> QueryDataTablePageListAsync<T>(QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<string> QueryJsonAsync<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<T> QueryList<T>(QueryDescriptor query = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> QueryListAsync<T>(QueryDescriptor query = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> QueryMapperAsync<T>(Action<T> mapperAction, QueryDescriptor query = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> QueryMapperAsync<T>(Action<T> mapperAction, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QueryMuch<T, T2, TResult>(Expression<Func<T, T2, object[]>> joinExpression, Expression<Func<T, T2, TResult>> selectExpression, Expression<Func<T, T2, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QueryMuch<T, T2, T3, T4, TResult>(Expression<Func<T, T2, T3, T4, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QueryMuch<T, T2, T3, T4, T5, TResult>(Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QueryMuch<T, T2, T3, T4, T5, T6, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QueryMuch<T, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QueryMuch<T, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QueryMuch<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryMuchAsync<T, T2, TResult>(Expression<Func<T, T2, object[]>> joinExpression, Expression<Func<T, T2, TResult>> selectExpression, Expression<Func<T, T2, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryMuchAsync<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, TResult>(Expression<Func<T, T2, T3, T4, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, TResult>(Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, T6, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, TResult>(Expression<Func<T, T2, object[]>> joinExpression, Expression<Func<T, T2, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, TResult>(Expression<Func<T, T2, T3, T4, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, TResult>(Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, T6, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, TResult>(Expression<Func<T, T2, object[]>> joinExpression, Expression<Func<T, T2, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, TResult>(Expression<Func<T, T2, T3, T4, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, TResult>(Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, T6, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression, Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression, QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<T>, int> QueryPageList<T>(QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<List<T>, int>> QueryPageListAsync<T>(QueryDescriptor query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public object QueryProcedureScalar(string procedureName, List<SqlParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public TResult QuerySelect<T, TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TResult> QuerySelectAsync<T, TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QuerySelectList<T, TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QuerySelectListAsync<T, TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> QuerySql<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public ISugarQueryable<T> QuerySql<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> QuerySqlAsync<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<ISugarQueryable<T>> QuerySqlAsync<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<T> QuerySqlList<T>(string sql) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> QuerySqlListAsync<T>(string sql) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public object QuerySqlScalar(string sql)
        {
            throw new NotImplementedException();
        }

        public T QuerySqlScalar<T>(string sql, params SugarParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<T> QuerySqlScalarAsync<T>(string sql, params SugarParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public List<T> QueryWhereList<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public List<TResult> QueryWhereList<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> QueryWhereListAsync<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryWhereListAsync<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public bool RenameColumn(string tableName, string oldColumnName, string newColumnName)
        {
            throw new NotImplementedException();
        }

        public Task<int> SumAsync<T>(string field) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> TakeAsync<T>(Expression<Func<T, bool>> whereLambda, int num) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public bool TruncateTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T entity, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Update<T>(List<T> entitys, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T entity, Expression<Func<T, bool>> where, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Update<T>(Expression<Func<T, T>> update, Expression<Func<T, bool>> where = null, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Update<T>(Dictionary<string, object> keyValues, Expression<Func<T, bool>> where = null, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(T entity, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(List<T> entitys, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(T entity, Expression<Func<T, bool>> where, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(Expression<Func<T, T>> update, Expression<Func<T, bool>> where = null, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(Dictionary<string, object> keyValues, Expression<Func<T, bool>> where = null, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public bool UpdateColumn(string tableName, DbColumnInfo column)
        {
            throw new NotImplementedException();
        }

        public int UpdateColumns<T>(List<T> entitys, Expression<Func<T, object>> updateColumns, Expression<Func<T, object>> wherecolumns = null, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int UpdateColumns<T>(T entity, Expression<Func<T, object>> updateColumns, Expression<Func<T, object>> wherecolumns = null, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int UpdateRowVer<T>(T entity, List<string> lstIgnoreColumns = null, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int UpdateRowVer<T>(Expression<Func<T, T>> update, Dictionary<string, object> where, bool isLock = true) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
