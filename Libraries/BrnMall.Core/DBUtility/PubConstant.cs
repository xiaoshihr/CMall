﻿using System;
using System.Configuration;
using System.Web;
namespace BrnMall.Core.DBUtility
{
    
    public class PubConstant
    {        
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {           
            get 
            {
                //string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];       
                //string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                //if (ConStringEncrypt == "true")
                //{
                //    _connectionString = DESEncrypt.Decrypt(_connectionString);
                //}
                string _connectionString = BMAConfig.RDBSConfig.RDBSConnectString;  
                return _connectionString; 
            }
        }

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }

        public static string GetConStr()
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            if (config.ConnectionStrings.ConnectionStrings["ConnectionStrings"] != null)
            {
                return "";
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;
            }
        }

    }
}
