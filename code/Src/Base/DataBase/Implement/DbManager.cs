using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eagles.Base.Logger;
using MySql.Data.MySqlClient;

namespace Eagles.Base.DataBase.Implement
{
    public class DbManager: IDbManager
    {
        private readonly ILogger logger;

        private const string connstr =
            "server=120.77.46.183;Port=3306;User Id=appuser;password=appuser@123;Database=Eagles";

        public DbManager(ILogger logger)
        {
            this.logger = logger;
        }

        public List<T> Query<T>(string command, object parameter)
        {
            var conn=new MySqlConnection(connstr);
            var result=new List<T>();
            try
            {
                result=conn.Query<T>(command, parameter).ToList();
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
            var conn = new MySqlConnection(connstr);
            var result = 0;
            try
            {
                 result=conn.Execute(command,paramster);
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
