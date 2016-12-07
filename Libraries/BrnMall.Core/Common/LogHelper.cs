using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace BrnMall.Core.Common
{
    /// <summary>
    /// LogHelper的摘要说明。
    /// </summary>
    public class LogHelper
    {
        private LogHelper()
        {
        }

        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        public static void SetConfig()
        {
            log4net.Config.DOMConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.DOMConfigurator.Configure(configFile); 
        }

        public static void WriteLog(string info)
        {
            if(loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public static void WriteLog(string info,Exception se)
        {
            if(logerror.IsErrorEnabled)
            {
                logerror.Error(info,se);
            }
        }
    }
}