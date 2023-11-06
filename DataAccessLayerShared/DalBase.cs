using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayerShared
{
    public abstract class DalBase
    {
        private readonly DbProviderFactory _factory;
        protected string _connectionString;
        protected string _currentUserId;
        protected ILogger _logger;


        protected DalBase(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService(typeof(ILogger).MakeGenericType(GetType())) as ILogger;
            _factory = GetDbProviderFactory(serviceProvider);
            _connectionString = GetConnectionString(serviceProvider);
        }

        private DbProviderFactory GetDbProviderFactory(IServiceProvider serviceProvider)
        {
            var providerName = GetSqlProviderName(serviceProvider);
            if (string.IsNullOrWhiteSpace(providerName))
            {
                return SqlClientFactory.Instance;
            }
            return DbProviderFactories.GetFactory(providerName);
        }

        protected abstract string GetConnectionString(IServiceProvider serviceProvider);
        protected abstract string GetSqlProviderName(IServiceProvider serviceProvider);

        public async Task<IDbTransaction> BeginTransaction()
        {
            var connection = await GetConnection();
            return connection.BeginTransaction();
        }

        protected async Task<IDbConnection> GetConnection()
        {
            var connection = _factory.CreateConnection();
            connection.ConnectionString = _connectionString;
            await connection.OpenAsync();
            return connection;
        }

        protected static IDbCommand CreateStoredProcCommand(IDbConnection connection, IDbTransaction transaction, string procName)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandTimeout = 60;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (transaction != null)
                cmd.Transaction = transaction as SqlTransaction;
            return cmd;
        }

        private async Task<R> ProcessExecution<R>(IDbConnection connection, IDbTransaction transaction, string storedProcedure, Dictionary<string, object> parameters, Func<IDbCommand, Task<R>> cmdAction)
        {
            using (var cmd = CreateStoredProcCommand(connection, transaction, storedProcedure))
            {
                if (parameters != null && parameters.Count > 0)
                    foreach (KeyValuePair<string, object> param in parameters)
                        if (param.Value is DataTable dt)
                        {
                            //because of mapping of Datatable
                            SqlParameter parameter = new SqlParameter($"@{param.Key}", dt)
                            {
                                TypeName = dt.TableName,
                                SqlDbType = SqlDbType.Structured
                            };
                            cmd.Parameters.Add(parameter);
                        }
                        else if (param.Value == null)
                            cmd.AddParameterWithValue($"@{param.Key}", DBNull.Value);
                        else
                            cmd.AddParameterWithValue($"@{param.Key}", param.Value);

                string stringParams = "";
                if (parameters != null)
                    stringParams = string.Join(", ", parameters.Select(p => $"@{p.Key}={(p.Value != null ? "'" + p.Value + "'" : "NULL")}"));
                _logger.LogDebug(storedProcedure + " " + stringParams);
                try
                {
                    var result = await cmdAction(cmd);
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
            }
        }

        protected async Task ExecuteWithoutReturn(IDbConnection connection, string storedProcedure,
            Dictionary<string, object> parameters = null, IDbTransaction transaction = null)
        {
            await ProcessExecution(connection, transaction, storedProcedure, parameters,
                dbCmd => { return Task.FromResult(dbCmd.ExecuteNonQuery()); });
        }

        private async Task<T> ExecuteWithReturnParameter<T>(IDbConnection connection, string storedProcedure,
            Dictionary<string, object> parameters, IDbTransaction transaction, IDbDataParameter returnValue)
        {
            return await ProcessExecution(connection, transaction, storedProcedure, parameters, dbCmd =>
            {
                dbCmd.Parameters.Add(returnValue);
                dbCmd.ExecuteNonQuery();
                return Task.FromResult((T)returnValue.Value);
            });
        }


        private DbType MapDBType<T>()
        {
            var underlingType = typeof(T);
            if (underlingType == typeof(Guid)) return DbType.Guid;
            if (underlingType == typeof(string)) return DbType.String;
            if (underlingType == typeof(int)) return DbType.Int32;
            if (underlingType == typeof(long)) return DbType.Int64;
            return DbType.Int32;
        }

        protected Task<T> ExecuteWithReturn<T>(IDbConnection connection, string storedProcedure,
            Dictionary<string, object> parameters = null, IDbTransaction transaction = null)
        {
            using (var cmd = connection.CreateCommand())
            {
                var returnParameter = cmd.CreateParameter();
                returnParameter.Direction = ParameterDirection.ReturnValue;
                returnParameter.DbType = MapDBType<T>();

                return ExecuteWithReturnParameter<T>(connection, storedProcedure, parameters, transaction, returnParameter);
            }
        }
        protected Task<T> ExecuteWithOut<T>(IDbConnection connection, string storedProcedure, string outParameterName,
            Dictionary<string, object> parameters = null, IDbTransaction transaction = null)
        {
            using (var cmd = connection.CreateCommand())
            {
                var returnParameter = cmd.CreateParameter();
                returnParameter.Direction = ParameterDirection.Output;
                returnParameter.DbType = MapDBType<T>();
                returnParameter.ParameterName = outParameterName;
                return ExecuteWithReturnParameter<T>(connection, storedProcedure, parameters, transaction, returnParameter);
            }
        }

        protected async Task<IDataReader> ExecuteForData(IDbConnection connection, string storedProcedure,
            Dictionary<string, object> parameters = null, IDbTransaction transaction = null)
        {
            return await ProcessExecution(connection, transaction, storedProcedure, parameters,
                dbCmd => { return Task.FromResult(dbCmd.ExecuteReader()); });
        }
    }
}
