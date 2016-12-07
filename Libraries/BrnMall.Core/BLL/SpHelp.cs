using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace BrnMall.Core.BLL
{

    public class SpHelp
    {
        public static string connectionString = DBUtility.PubConstant.ConnectionString;

        #region 插入微信支付订单
        public static object SP_CheckPayOrder(string orderno,string openid,int pice,int type,int status)
        {
            DbParameter[] parms =  {
                             GenerateInParam("@orderno", SqlDbType.VarChar ,100,orderno),
                             GenerateInParam("@openid", SqlDbType.VarChar ,100,openid),
                              GenerateInParam("@type", SqlDbType.Int ,4,type),
                             GenerateInParam("@pice", SqlDbType.Int ,4,pice),
                             GenerateInParam("@status", SqlDbType.Int ,4,status)
            };
            return ExecuteScalar(CommandType.StoredProcedure, "[SP_CheckPayOrder]", parms);
        }

        #endregion
        #region 更新推荐人
        public static object SP_UpdateMyParent(int upuid,int pid,int layernum)
        {
            DbParameter[] parms =  {
                             GenerateInParam("@upuid", SqlDbType.Int ,4,upuid),
                              GenerateInParam("@pid", SqlDbType.Int ,4,pid),
                               GenerateInParam("@layernum", SqlDbType.Int ,4,layernum)
            };
            return ExecuteScalar(CommandType.StoredProcedure, "[SP_UpdateMyParent]", parms);
        }
        #endregion

            #region  辅助方法
        public static DbProviderFactory _factory;//抽象数据工厂
        /// <summary>
        /// 生成输入参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static DbParameter GenerateInParam(string paramName, SqlDbType sqlDbType, int size, object value)
        {
            _factory = GetDbProviderFactory();
            SqlParameter param = new SqlParameter(paramName, sqlDbType, size);
            param.Direction = ParameterDirection.Input;
            if (value != null)
                param.Value = value;
            return param;
        }

        public static object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {


            DbCommand cmd = _factory.CreateCommand();

            using (DbConnection connection = _factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                DateTime startTime = DateTime.Now;

                object val = cmd.ExecuteScalar();

                //DateTime endTime = DateTime.Now;
                //_executedetail += GetExecuteDetail(cmd.CommandText, startTime, endTime, commandParameters);

                cmd.Parameters.Clear();
                return val;
            }
        }
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                {
                    if (parm != null)
                    {
                        if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Input) &&
                            (parm.Value == null))
                        {
                            parm.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parm);
                    }
                }
            }
        }
        /// <summary>
        /// 获得数据库提供程序工厂
        /// </summary>
        public static DbProviderFactory GetDbProviderFactory()
        {
            return SqlClientFactory.Instance;
        }
        #endregion
    }
}
