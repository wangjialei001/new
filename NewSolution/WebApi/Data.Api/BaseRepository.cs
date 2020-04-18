using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using System.Data;

namespace Data.Api
{
    public class BaseRepository
    {
        private readonly IConfiguration Configuration;
        private readonly string connectionString;
        public BaseRepository(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            connectionString = Configuration["ConnectionString:MySql"];
        }
        public async Task<bool> UpdateAsync(string sql, IDbTransaction dbTransaction = null)
        {
            int n = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                n = await connection.ExecuteAsync(sql);
            }
            return n > 0;
        }
        public bool Update(string sql, IDbTransaction dbTransaction = null)
        {
            int n = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                n = connection.Execute(sql);
            }
            return n > 0;
        }
        public List<T> Query<T>(string sql,IDbTransaction dbTransaction=null)
        {
            List<T> data = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
               var items = connection.Query<T>(sql);
                data = items as List<T>;
            }
            return data;
        }
        public async Task<List<T>> QueryAsync<T>(string sql, IDbTransaction dbTransaction = null)
        {
            List<T> data = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var items = await connection.QueryAsync<T>(sql);
                data = items as List<T>;
            }
            return data;
        }
    }
}
