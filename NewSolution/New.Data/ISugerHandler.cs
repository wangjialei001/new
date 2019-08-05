using New.Common.Sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace New.Data
{
    /// <summary>
    /// SqlSugar操作接口类
    /// </summary>
    public interface ISugerHandler : IDisposable
    {
        SqlSugarClient DbContext { get; set; }

        #region 事务

        /// <summary>
        /// 事务操作
        /// 注意:代码段里面如果调用本身类其它方法或其它类方法必须带着var db = SugerHandler.Instance()这个db走，不带着走事务回滚会不成功
        /// </summary> 
        /// <param name="level">事务级别</param>
        /// /// <param name="serviceAction">代码段</param> 
        bool InvokeTransactionScope(IsolationLevel level = IsolationLevel.ReadCommitted, string message = "添加成功", params Func<bool>[] serviceActions);
        #endregion

        #region 数据库管理
        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column">列信息</param>
        /// <returns></returns>
        bool AddColumn(string tableName, DbColumnInfo column);
        /// <summary>
        /// 添加主键
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        bool AddPrimaryKey(string tableName, string columnName);
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        /// <param name="fullFileName">文件名</param>
        /// <returns></returns>
        bool BackupDataBase(string databaseName, string fullFileName);
        /// <summary>
        /// 备份表
        /// </summary>
        /// <param name="oldTableName">旧表名</param>
        /// <param name="newTableName">行表名</param>
        /// <param name="maxBackupDataRows">行数</param>
        /// <returns></returns>
        bool BackupTable(string oldTableName, string newTableName, int maxBackupDataRows = int.MaxValue);
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列集合</param>
        /// <param name="isCreatePrimaryKey">是否创建主键</param>
        /// <returns></returns>
        bool CreateTable(string tableName, List<DbColumnInfo> columns, bool isCreatePrimaryKey = true);
        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        bool DropColumn(string tableName, string columnName);
        /// <summary>
        /// 删除约束
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="constraintName">约束名</param>
        /// <returns></returns>
        bool DropConstraint(string tableName, string constraintName);
        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool DropTable(string tableName);
        /// <summary>
        /// 获取列信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="isCache">是否缓存</param>
        /// <returns></returns>
        List<DbColumnInfo> GetColumnInfosByTableName(string tableName, bool isCache = true);
        /// <summary>
        /// 获取自增列
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        List<string> GetIsIdentities(string tableName);
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        List<string> GetPrimaries(string tableName);
        /// <summary>
        /// 获取表集合
        /// </summary>
        /// <param name="isCache">是否缓存</param>
        /// <returns></returns>
        List<DbTableInfo> GetTableInfoList(bool isCache = true);
        /// <summary>
        /// 获取视图集合
        /// </summary>
        /// <param name="isCache">是否缓存</param>
        /// <returns></returns>
        List<DbTableInfo> GetViewInfoList(bool isCache = true);
        /// <summary>
        /// 检测列是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column">列名</param>
        /// <returns></returns>
        bool IsAnyColumn(string tableName, string column);
        /// <summary>
        /// 检测约束
        /// </summary>
        /// <param name="constraintName">约束名称</param>
        /// <returns></returns>
        bool IsAnyConstraint(string constraintName);
        /// <summary>
        /// 检测是否有任何系统表权限 
        /// </summary>
        /// <returns></returns>
        bool IsAnySystemTablePermissions();
        /// <summary>
        /// 检测表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="isCache">是否缓存</param>
        /// <returns></returns>
        bool IsAnyTable(string tableName, bool isCache = true);
        /// <summary>
        /// 检测列是否自增列
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column">列名</param>
        /// <returns></returns>
        bool IsIdentity(string tableName, string column);
        /// <summary>
        /// 检测列是否主键
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column">列名</param>
        /// <returns></returns>
        bool IsPrimaryKey(string tableName, string column);
        /// <summary>
        /// 重置列名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="oldColumnName">旧列名</param>
        /// <param name="newColumnName">新列名</param>
        /// <returns></returns>
        bool RenameColumn(string tableName, string oldColumnName, string newColumnName);
        /// <summary>
        /// 重置表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        bool TruncateTable(string tableName);
        /// <summary>
        /// 修改列信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column">列信息</param>
        /// <returns></returns>
        bool UpdateColumn(string tableName, DbColumnInfo column);

        /// <summary>
        /// 获取数据库时间
        /// </summary>
        /// <returns>返回值</returns>
        DateTime GetDataBaseTime();
        #endregion

        #region 新增(异步执行) 
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param>  
        /// <returns>操作影响的行数</returns>
        Task<int> AddAsync<T>(T entity) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entitys">泛型集合</param>
        /// <returns>操作影响的行数</returns>
        Task<int> AddAsync<T>(List<T> entitys) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="keyValues">字典集合（Key:列名 Value:值）</param> 
        /// <returns>操作影响的行数</returns>
        Task<int> AddAsync<T>(Dictionary<string, object> keyValues) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param> 
        /// <returns>返回实体</returns>
        Task<T> AddReturnEntityAsync<T>(T entity) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param> 
        /// <returns>返回自增列</returns>
        Task<int> AddReturnIdentityAsync<T>(T entity) where T : class, new();
        /// <summary>
        /// 新增
        /// </summary> 
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param> 
        /// <returns>返回bool</returns>
        Task<bool> AddReturnBoolAsync<T>(T entity) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entitys">泛型集合</param>
        /// <returns>返回bool</returns>
        Task<bool> AddReturnBoolAsync<T>(List<T> entitys) where T : class, new();
        #endregion

        #region 新增(同步执行) 
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param>  
        /// <returns>操作影响的行数</returns>
        int Add<T>(T entity) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entitys">泛型集合</param>
        /// <returns>操作影响的行数</returns>
        int Add<T>(List<T> entitys) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="keyValues">字典集合（Key:列名 Value:值）</param> 
        /// <returns>操作影响的行数</returns>
        int Add<T>(Dictionary<string, object> keyValues) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param> 
        /// <returns>返回实体</returns>
        T AddReturnEntity<T>(T entity) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param> 
        /// <returns>返回自增列</returns>
        int AddReturnIdentity<T>(T entity) where T : class, new();
        /// <summary>
        /// 新增
        /// </summary> 
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param> 
        /// <returns>返回bool</returns>
        bool AddReturnBool<T>(T entity) where T : class, new();

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entitys">泛型集合</param>
        /// <returns>返回bool</returns>
        bool AddReturnBool<T>(List<T> entitys) where T : class, new();
        #endregion

        #region 修改

        /// <summary>
        /// 修改（主键是更新条件）
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件 </param> 
        /// <param name="lstIgnoreColumns">不更新的列</param>
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateAsync<T>(T entity, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new();

        /// <summary>
        /// 修改（主键是更新条件）
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entitys"> 实体对象集合（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件 </param> 
        /// <param name="lstIgnoreColumns">不更新的列</param>
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateAsync<T>(List<T> entitys, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new();

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param> 
        /// <param name="where"> 条件 </param>  
        /// <param name="lstIgnoreColumns">不更新的列</param>
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateAsync<T>(T entity, Expression<Func<T, bool>> where, List<string> lstIgnoreColumns = null,
            bool isLock = true, bool isIgnoreColumns = true) where T : class, new();

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="update"> 实体对象 </param> 
        /// <param name="where"> 条件 </param>  
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateAsync<T>(Expression<Func<T, T>> update, Expression<Func<T, bool>> where = null, bool isLock = true) where T : class, new();

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="keyValues">字典集合（Key:列名 Value:值）</param> 
        /// <param name="where"> 条件 </param>  
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateAsync<T>(Dictionary<string, object> keyValues, Expression<Func<T, bool>> where = null, bool isLock = true)
            where T : class, new();


        /// <summary>
        /// 修改（主键是更新条件）
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件 </param> 
        /// <param name="lstIgnoreColumns">不更新的列</param>
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int Update<T>(T entity, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new();

        /// <summary>
        /// 修改（主键是更新条件）
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entitys"> 实体对象集合（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件 </param> 
        /// <param name="lstIgnoreColumns">不更新的列</param>
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int Update<T>(List<T> entitys, List<string> lstIgnoreColumns = null, bool isLock = true, bool isIgnoreColumns = true) where T : class, new();

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param> 
        /// <param name="where"> 条件 </param>  
        /// <param name="lstIgnoreColumns">不更新的列</param>
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int Update<T>(T entity, Expression<Func<T, bool>> where, List<string> lstIgnoreColumns = null,
            bool isLock = true, bool isIgnoreColumns = true) where T : class, new();

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="update"> 实体对象 </param> 
        /// <param name="where"> 条件 </param>  
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int Update<T>(Expression<Func<T, T>> update, Expression<Func<T, bool>> where = null, bool isLock = true) where T : class, new();

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="keyValues">字典集合（Key:列名 Value:值）</param> 
        /// <param name="where"> 条件 </param>  
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int Update<T>(Dictionary<string, object> keyValues, Expression<Func<T, bool>> where = null, bool isLock = true)
            where T : class, new();

        /// <summary>
        /// 批量修改需要更新的列
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entitys"> 实体对象（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件 </param> 
        /// <param name="updateColumns">更新指定列</param>
        /// <param name="wherecolumns">条件(为空则以主键更新,反之需要把wherecolumns中的列加到UpdateColumns中)</param>
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int UpdateColumns<T>(List<T> entitys, Expression<Func<T, object>> updateColumns,
            Expression<Func<T, object>> wherecolumns = null, bool isLock = true) where T : class, new();

        /// <summary>
        /// 批量修改需要更新的列
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entitys"> 实体对象（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件 </param> 
        /// <param name="updateColumns">更新指定列</param>
        /// <param name="wherecolumns">条件(为空则以主键更新,反之需要把wherecolumns中的列加到UpdateColumns中)</param>
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int UpdateColumns<T>(T entity, Expression<Func<T, object>> updateColumns,
            Expression<Func<T, object>> wherecolumns = null, bool isLock = true) where T : class, new();


        /// <summary>
        /// 修改 通过RowVer及主键Code 更新
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 </param> 
        /// <param name="lstIgnoreColumns">不更新的列</param>
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int UpdateRowVer<T>(T entity, List<string> lstIgnoreColumns = null, bool isLock = true) where T : class, new();

        /// <summary>
        /// 修改 通过RowVer及主键Code 更新
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="update"> 实体对象 </param>  
        /// <param name="where"> 更新条件 </param>  
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int UpdateRowVer<T>(Expression<Func<T, T>> update, Dictionary<string, object> where, bool isLock = true) where T : class, new();
        #endregion

        #region 删除（异步）

        /// <summary>
        /// 删除 通过主键数据
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="primaryKeyValues">主键值</param> 
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteByPrimaryAsync<T>(List<object> primaryKeyValues, bool isLock = true) where T : class, new();

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件 </param> 
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync<T>(T entity, bool isLock = true) where T : class, new();

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 （必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param> 
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync<T>(List<T> entity, bool isLock = true) where T : class, new();

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="where"> 条件 </param> 
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where, bool isLock = true) where T : class, new();

        /// <summary>
        /// 通过多值(主键)删除数据集
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam> 
        /// <param name="inValues">数据集合</param>
        /// <returns></returns>
        Task<int> DeleteInAsync<T>(List<dynamic> inValues) where T : class, new();

        #endregion

        #region 删除（同步）

        /// <summary>
        /// 删除 通过主键数据
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="primaryKeyValues">主键值</param> 
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int DeleteByPrimary<T>(List<object> primaryKeyValues, bool isLock = true) where T : class, new();

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件 </param> 
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int Delete<T>(T entity, bool isLock = true) where T : class, new();

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="entity"> 实体对象 （必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param> 
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int Delete<T>(List<T> entity, bool isLock = true) where T : class, new();

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="where"> 条件 </param> 
        /// <param name="isLock"> 是否加锁 </param> 
        /// <returns>操作影响的行数</returns>
        int Delete<T>(Expression<Func<T, bool>> where, bool isLock = true) where T : class, new();

        /// <summary>
        /// 通过多值(主键)删除数据集
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam> 
        /// <param name="inValues">数据集合</param>
        /// <returns></returns>
        int DeleteIn<T>(List<dynamic> inValues) where T : class, new();

        #endregion

        #region 查询

        #region 数据源

        ///// <summary>
        ///// 查询数据源
        ///// </summary>
        ///// <typeparam name="T">泛型参数(集合成员的类型</typeparam>
        ///// <returns>数据源</returns>
        //ISugarQueryable<T> Queryable<T>() where T : class, new();

        #endregion

        #region 多表查询（异步）

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (t1,t2) => new object[] {JoinType.Left,t1.UserNo==t2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (t1, t2) => new { Id =t1.UserNo, Id1 = t2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (t1, t2) =>t1.UserNo == "")</param>   
        /// <returns></returns>
        Task<List<TResult>> QueryMuchAsync<T, T2, TResult>(
            Expression<Func<T, T2, object[]>> joinExpression,
            Expression<Func<T, T2, TResult>> selectExpression,
            Expression<Func<T, T2, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式</param>
        /// <param name="selectExpression">返回表达式</param>
        /// <param name="whereLambda">查询表达式</param>  
        /// <returns></returns>
        Task<List<TResult>> QueryMuchAsync<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new();


        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式</param>
        /// <param name="selectExpression">返回表达式</param>
        /// <param name="whereLambda">查询表达式</param>  
        /// <returns></returns>
        Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, TResult>(
            Expression<Func<T, T2, T3, T4, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式</param>
        /// <param name="selectExpression">返回表达式</param>
        /// <param name="whereLambda">查询表达式</param>  
        /// <returns></returns>
        Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, TResult>(
           Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression,
           Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression,
           Expression<Func<T, T2, T3, T4, T5, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>  
        /// <returns>值</returns>
        Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, T6, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>  
        /// <returns>值</returns>
        Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, T6, T7, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>  
        /// <returns>值</returns>
        Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="T9">实体9</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>  
        /// <returns>值</returns>
        Task<List<TResult>> QueryMuchAsync<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (t1,t2) => new object[] {JoinType.Left,t1.UserNo==t2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (t1, t2) => new { Id =t1.UserNo, Id1 = t2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <param name="totalCount">总行数</param>  
        /// <returns>值</returns>
        Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, TResult>(
            Expression<Func<T, T2, object[]>> joinExpression,
            Expression<Func<T, T2, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();


        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>   
        /// <returns>列表和总行数</returns>
        Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();


        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, TResult>(
            Expression<Func<T, T2, T3, T4, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>   
        /// <returns>列表和总行数</returns>
        Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, TResult>(
            Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, T6, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, T6, T7, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="T9">实体9</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Task<Tuple<List<TResult>, int>> QueryMuchDescriptorAsync<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();
        #endregion

        #region 多表查询（同步）

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (t1,t2) => new object[] {JoinType.Left,t1.UserNo==t2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (t1, t2) => new { Id =t1.UserNo, Id1 = t2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (t1, t2) =>t1.UserNo == "")</param>   
        /// <returns></returns>
        List<TResult> QueryMuch<T, T2, TResult>(
            Expression<Func<T, T2, object[]>> joinExpression,
            Expression<Func<T, T2, TResult>> selectExpression,
            Expression<Func<T, T2, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式</param>
        /// <param name="selectExpression">返回表达式</param>
        /// <param name="whereLambda">查询表达式</param>  
        /// <returns></returns>
        List<TResult> QueryMuch<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new();


        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式</param>
        /// <param name="selectExpression">返回表达式</param>
        /// <param name="whereLambda">查询表达式</param>  
        /// <returns></returns>
        List<TResult> QueryMuch<T, T2, T3, T4, TResult>(
            Expression<Func<T, T2, T3, T4, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式</param>
        /// <param name="selectExpression">返回表达式</param>
        /// <param name="whereLambda">查询表达式</param>  
        /// <returns></returns>
        List<TResult> QueryMuch<T, T2, T3, T4, T5, TResult>(
           Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression,
           Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression,
           Expression<Func<T, T2, T3, T4, T5, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>  
        /// <returns>值</returns>
        List<TResult> QueryMuch<T, T2, T3, T4, T5, T6, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>  
        /// <returns>值</returns>
        List<TResult> QueryMuch<T, T2, T3, T4, T5, T6, T7, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>  
        /// <returns>值</returns>
        List<TResult> QueryMuch<T, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="T9">实体9</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param>  
        /// <returns>值</returns>
        List<TResult> QueryMuch<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (t1,t2) => new object[] {JoinType.Left,t1.UserNo==t2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (t1, t2) => new { Id =t1.UserNo, Id1 = t2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <param name="totalCount">总行数</param>  
        /// <returns>值</returns>
        Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, TResult>(
            Expression<Func<T, T2, object[]>> joinExpression,
            Expression<Func<T, T2, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();


        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>   
        /// <returns>列表和总行数</returns>
        Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();


        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, TResult>(
            Expression<Func<T, T2, T3, T4, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>   
        /// <returns>列表和总行数</returns>
        Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, TResult>(
            Expression<Func<T, T2, T3, T4, T5, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, T6, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, T6, T7, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();

        /// <summary>
        ///查询-多表查询 
        /// </summary>
        /// <typeparam name="T">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="T4">实体4</typeparam>
        /// <typeparam name="T5">实体5</typeparam>
        /// <typeparam name="T6">实体6</typeparam>
        /// <typeparam name="T7">实体7</typeparam>
        /// <typeparam name="T8">实体8</typeparam>
        /// <typeparam name="T9">实体9</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="query">查询条件</param>  
        /// <returns>列表和总行数</returns>
        Tuple<List<TResult>, int> QueryMuchDescriptor<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object[]>> joinExpression,
            Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selectExpression,
            QueryDescriptor query) where T : class, new();
        #endregion

        #region  查询（异步）

        /// <summary>
        /// 查询-返回自定义数据
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="expression">返回值表达式</param> 
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns>值</returns>
        Task<TResult> QuerySelectAsync<T, TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 查询-返回自定义数据
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="expression">返回值表达式</param> 
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns>值</returns>
        Task<List<TResult>> QuerySelectListAsync<T, TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns></returns>
        Task<T> QueryAsync<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="whereLambda">过滤条件</param> 
        /// <returns>实体</returns>
        Task<List<T>> QueryWhereListAsync<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new();
        Task<List<TResult>> QueryWhereListAsync<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new();
        Task<TResult> QueryAsync<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new();
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="query">过滤条件</param> 
        /// <returns>实体</returns>
        Task<List<T>> QueryListAsync<T>(QueryDescriptor query = null) where T : class, new();

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="sql">sql</param> 
        /// <returns>实体</returns>
        Task<List<T>> QuerySqlListAsync<T>(string sql) where T : class, new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="query">过滤条件</param>
        /// <param name="totalCount">总行数</param>
        /// <returns>实体</returns>
        Task<Tuple<List<T>, int>> QueryPageListAsync<T>(QueryDescriptor query) where T : class, new();

        /// <summary>
        /// 通过多值查询数据集
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="inFieldName">字段名</param>
        /// <param name="inValues">数据集合</param>
        /// <returns></returns>
        Task<List<T>> FieldNameInAsync<T>(string inFieldName, List<dynamic> inValues, Expression<Func<T, bool>> whereLambda = null) where T : class, new();
        /// <summary>
        /// 通过多值查询数据集
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="expression">返回值表达式</param>
        /// <param name="inValues">数据集合</param>
        /// <param name="whereLambda">查询表达式</param>  
        /// <returns></returns>
        Task<List<T>> InAsync<T>(Expression<Func<T, object>> expression, object[] inValues, Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 通过多值(主键)查询数据集
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="values">主键数据集合</param>
        /// <returns></returns>
        Task<List<T>> InAsync<T>(List<dynamic> values) where T : class, new();

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns>实体</returns>
        Task<DataTable> QueryDataTableAsync<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        Task<T> QuerySqlScalarAsync<T>(string sql, params SugarParameter[] parameters);
        Task<int> QueryCountAsync<T>(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 查询列表(注意：未解密，如有需要需自己进行解密的操作)
        /// </summary>
        /// <typeparam name="T">返回的实体类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>可查询的对象</returns>
        Task<IQueryable<T>> QuerySqlAsync<T>(string sql, params SugarParameter[] parameters) where T : class, new();
        /// <summary>
        /// 查询列表(注意：未解密，如有需要需自己进行解密的操作)
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <returns></returns>
        Task<ISugarQueryable<T>> QuerySqlAsync<T>() where T : class, new();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="query">过滤条件</param>
        /// <param name="totalCount">总行数</param>
        /// <returns>DataTable</returns>
        Task<Tuple<DataTable, int>> QueryDataTablePageListAsync<T>(QueryDescriptor query) where T : class, new();

        #endregion

        #region  查询（同步）

        /// <summary>
        /// 查询-返回自定义数据
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="expression">返回值表达式</param> 
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns>值</returns>
        TResult QuerySelect<T, TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 查询-返回自定义数据
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="expression">返回值表达式</param> 
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns>值</returns>
        List<TResult> QuerySelectList<T, TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns></returns>
        T Query<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="whereLambda">过滤条件</param> 
        /// <returns>实体</returns>
        List<T> QueryWhereList<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new();
        List<TResult> QueryWhereList<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new();
        TResult Query<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new();
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="query">过滤条件</param> 
        /// <returns>实体</returns>
        List<T> QueryList<T>(QueryDescriptor query = null) where T : class, new();

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="sql">sql</param> 
        /// <returns>实体</returns>
        List<T> QuerySqlList<T>(string sql) where T : class, new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="query">过滤条件</param>
        /// <param name="totalCount">总行数</param>
        /// <returns>实体</returns>
        Tuple<List<T>, int> QueryPageList<T>(QueryDescriptor query) where T : class, new();

        /// <summary>
        /// 通过多值查询数据集
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="inFieldName">字段名</param>
        /// <param name="inValues">数据集合</param>
        /// <returns></returns>
        List<T> FieldNameIn<T>(string inFieldName, List<dynamic> inValues, Expression<Func<T, bool>> whereLambda = null) where T : class, new();
        /// <summary>
        /// 通过多值查询数据集
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="expression">返回值表达式</param>
        /// <param name="inValues">数据集合</param>
        /// <param name="whereLambda">查询表达式</param>  
        /// <returns></returns>
        List<T> In<T>(Expression<Func<T, object>> expression, object[] inValues, Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 通过多值(主键)查询数据集
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="values">主键数据集合</param>
        /// <returns></returns>
        List<T> In<T>(List<dynamic> values) where T : class, new();

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns>实体</returns>
        DataTable QueryDataTable<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 查询集合
        /// </summary> 
        /// <param name="sql">sql</param> 
        /// <returns>实体</returns>
        DataTable QueryDataTable(string sql);

        /// <summary>
        /// 查询单个值
        /// </summary> 
        /// <param name="sql">sql</param> 
        /// <returns>单个值</returns>
        object QuerySqlScalar(string sql);
        T QuerySqlScalar<T>(string sql, params SugarParameter[] parameters);
        int QueryCount<T>(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T">返回的实体类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>可查询的对象</returns>
        IQueryable<T> QuerySql<T>(string sql, params SugarParameter[] parameters) where T : class, new();
        ISugarQueryable<T> QuerySql<T>() where T : class, new();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="query">过滤条件</param>
        /// <param name="totalCount">总行数</param>
        /// <returns>DataTable</returns>
        Tuple<DataTable, int> QueryDataTablePageList<T>(QueryDescriptor query) where T : class, new();

        #endregion

        #region Mapper

        /// <summary>
        /// Mapper查询
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="mapperAction">操作(it, cache) =>
        /// { 
        ///     var all = cache.GetListByPrimaryKeys(vmodel => vmodel.Id);  //一次性查询出所要的外键引用数据 
        ///     it.School = all.FirstOrDefault(i => i.Id == it.Id); //一对一 
        ///     it.Schools = all.Where(i => i.Id == it.Id).ToList(); //一对多
        ///     /*用C#处理你想要的结果*/
        ///     it.Name = it.Name == null ? "null" : it.Name;
        ///  }
        /// </param>
        /// <param name="query">过滤条件</param>
        /// <returns></returns>
        Task<List<T>> QueryMapperAsync<T>(Action<T> mapperAction, QueryDescriptor query = null) where T : class, new();

        /// <summary>
        /// Mapper查询
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="mapperAction">操作(it, cache) =>
        /// { 
        ///     var all = cache.GetListByPrimaryKeys(vmodel => vmodel.Id);  //一次性查询出所要的外键引用数据 
        ///     it.School = all.FirstOrDefault(i => i.Id == it.Id); //一对一 
        ///     it.Schools = all.Where(i => i.Id == it.Id).ToList(); //一对多
        ///     /*用C#处理你想要的结果*/
        ///     it.Name = it.Name == null ? "null" : it.Name;
        ///  }
        /// </param>
        /// <param name="whereLambda">过滤条件</param>
        /// <returns></returns>
        Task<List<T>> QueryMapperAsync<T>(Action<T> mapperAction, Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        #endregion

        #region 存储过程
        Task<TResult> FirstAsync<T, TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selectExpression) where T : class, new();
        /// <summary>
        /// 查询存储过程
        /// </summary> 
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandType">执行数据库命令的类型</param>
        /// <returns>DataSet</returns>
        DataSet QueryDataSet(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.Text);
        /// <summary>
        /// 查询存储过程
        /// </summary> 
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandType">执行数据库命令的类型</param>
        /// <returns>DataTable</returns>
        DataTable QueryDataTable(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 查询存储过程
        /// </summary> 
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>单个值</returns>
        object QueryProcedureScalar(string procedureName, List<SqlParameter> parameters);

        #endregion

        #region 分组

        /// <summary>
        /// 分组
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam> 
        /// <param name="groupByLambda">分组表达式</param> 
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns>值</returns>
        Task<List<T>> GroupByAsync<T>(Expression<Func<T, object>> groupByLambda, Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 分组-返回自定义数据
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="expression">返回值表达式</param> 
        /// <param name="groupByLambda">分组表达式</param> 
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns>值</returns>
        Task<List<TResult>> GroupByAsync<T, TResult>(Expression<Func<T, TResult>> expression,
            Expression<Func<T, object>> groupByLambda, Expression<Func<T, bool>> whereLambda = null)
            where T : class, new();

        #endregion

        #region Json

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns>Json</returns>
        Task<string> QueryJsonAsync<T>(Expression<Func<T, bool>> whereLambda = null) where T : class, new();

        #endregion

        #region 其它

        /// <summary>
        /// 查询前多少条数据
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型</typeparam>
        /// <param name="whereLambda">查询表达式</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        Task<List<T>> TakeAsync<T>(Expression<Func<T, bool>> whereLambda, int num) where T : class, new();

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型</typeparam>
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns></returns>
        Task<T> FirstAsync<T>(Expression<Func<T, bool>> whereLambda) where T : class, new();

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型</typeparam>
        /// <param name="whereLambda">查询表达式</param> 
        /// <returns></returns>
        Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> whereLambda) where T : class, new();

        /// <summary>
        /// 合计
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型</typeparam>
        /// <param name="field">字段</param> 
        /// <returns></returns>
        Task<int> SumAsync<T>(string field) where T : class, new();

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型</typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="field">字段</param> 
        /// <returns></returns>
        Task<TResult> MaxAsync<T, TResult>(string field) where T : class, new();

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型</typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="field">字段</param> 
        /// <returns></returns>
        Task<TResult> MinAsync<T, TResult>(string field) where T : class, new();
        /// <summary>
        /// 平均值
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型</typeparam>
        /// <param name="field">字段</param> 
        /// <returns></returns>
        Task<int> AvgAsync<T>(string field) where T : class, new();

        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <param name="key">列名</param>
        /// <param name="prefix">前缀</param>
        /// <param name="fixedLength">流水号长度</param>
        /// <param name="dateFomart">日期格式(yyyyMMdd) 为空前缀后不加日期,反之加</param>
        /// <returns></returns>
        string CustomNumber<T>(string key, string prefix = "", int fixedLength = 4, string dateFomart = "") where T : class, new();

        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <param name="key">列名</param>
        /// <param name="num">数量</param>
        /// <param name="prefix">前缀</param>
        /// <param name="fixedLength">流水号长度</param>
        /// <param name="dateFomart">日期格式(yyyyMMdd) 为空前缀后不加日期,反之加</param>
        /// <returns></returns>
        List<string> CustomNumber<T>(string key, int num, string prefix = "", int fixedLength = 4, string dateFomart = "") where T : class, new();

        #endregion

        /// <summary>
        /// 异步执行sql语句（使用事务）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<int> ExecuteSqlTransAsync(string sql, params SugarParameter[] parameters);

        /// <summary>
        /// 异步执行sql语句(未使用事务)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<int> ExecuteSqlAsync(string sql, params SugarParameter[] parameters);

        /// <summary>
        /// 异步执行sql语句（使用事务）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int ExecuteSqlTrans(string sql, params SugarParameter[] parameters);

        /// <summary>
        /// 执行sql语句(未使用事务)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int ExecuteSql(string sql, params SugarParameter[] parameters);

        #endregion
    }
}
