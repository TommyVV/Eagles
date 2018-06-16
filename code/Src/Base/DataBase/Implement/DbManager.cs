using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eagles.Base.Config;
using Eagles.Base.Configuration;
using Eagles.Base.Logger;
using MySql.Data.MySqlClient;

namespace Eagles.Base.DataBase.Implement
{
    public class DbManager : IDbManager
    {
        private readonly IConfigurationManager configuration;

        private readonly ILogger logger;

        public DbManager(IConfigurationManager configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        private DataBaseConfig DbConfig => configuration.GetConfiguration<DataBaseConfig>();

        public List<T> Query<T>(string command, object parameter)
        {
            var conn = new MySqlConnection(DbConfig.DataBaseConnectString);
            var result = new List<T>();
            try
            {
                result = conn.Query<T>(command, parameter).ToList();
            }
            catch (Exception e)
            {
                logger.LoggerError(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int Excuted(string command, object paramster)
        {
            var conn = new MySqlConnection(DbConfig.DataBaseConnectString);
            var result = 0;
            try
            {
                result = conn.Execute(command, paramster);
            }
            catch (Exception e)
            {
                logger.LoggerError(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
