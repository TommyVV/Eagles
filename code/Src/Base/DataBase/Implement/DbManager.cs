﻿using System;
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

        public int Excuted(string command, object parameter)
        {
            var conn = new MySqlConnection(DbConfig.DataBaseConnectString);
            var result = 0;
            try
            {
                result = conn.Execute(command, parameter);
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

        /// <summary>
        /// ExcutedByTransaction
        /// </summary>
        /// <param name="command">command string, object is parameters</param>
        /// <returns></returns>
        public bool ExcutedByTransaction(Dictionary<string,object> command)
        {
            var response = true;
            using (var conn = new MySqlConnection(DbConfig.DataBaseConnectString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {

                    foreach (var cmd in command)
                    {
                        var result = conn.Execute(cmd.Key, cmd.Value);
                        if (result <= 0)
                        {
                            trans.Rollback();
                            response = false;
                            break;
                        }
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    response = false;
                    trans.Rollback();
                }
                finally
                {
                   conn.Close();
                }
            }
            return response;
        }
    }
}
