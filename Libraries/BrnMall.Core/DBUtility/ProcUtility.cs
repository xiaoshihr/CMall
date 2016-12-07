using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BrnMall.Core.DBUtility;

namespace BrnMall.Core.DBUtility
{


    public class ProcUtility
    {

        #region 公用分页存储过程
        /// <summary>
        /// 公用分页存储过程 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">表中的字段，可以使用*代替</param>
        /// <param name="orderField">要排序的字段</param>
        /// <param name="sqlWhere">WHERE子句</param>
        /// <param name="pageSize">分页的大小</param>
        /// <param name="pageIndex">要显示的页的索引</param>
        /// <returns></returns>
        public DataSet GetPagePro(string tableName, string fields, string orderField, string sqlWhere, int pageSize, int pageIndex, out int total)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@TableName", SqlDbType.VarChar, 255),
                    new SqlParameter("@Fields", SqlDbType.VarChar, 3000),
                    new SqlParameter("@OrderField", SqlDbType.VarChar, 255),
                    new SqlParameter("@sqlWhere", SqlDbType.VarChar,1000),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@pageIndex", SqlDbType.Int),
                    new SqlParameter("@TotalPage", SqlDbType.Int),
                    new SqlParameter("@RecordCount", SqlDbType.Int)
                    };
            parameters[0].Value = tableName;
            parameters[1].Value = fields;
            parameters[2].Value = orderField;
            parameters[3].Value = sqlWhere;
            parameters[4].Value = pageSize;
            parameters[5].Value = pageIndex;
            parameters[6].Value = ParameterDirection.Output;
            parameters[7].Value = ParameterDirection.Output;
            total = Convert.ToInt32(parameters[6].Value);
            return DbHelperSQL.RunProcedure("ZXL_GetPageData", parameters, "ds");
        }

        #endregion

        #region 公用分页存储过程 DataSet GetPagePro()
        /// <summary>
        /// 公用分页存储过程 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">表中的字段，可以使用*代替</param>
        /// <param name="orderField">要排序的字段</param>
        /// <param name="sqlWhere">WHERE子句</param>
        /// <param name="pageSize">分页的大小</param>
        /// <param name="pageIndex">要显示的页的索引</param>
        /// <param name="pageIndex">返回的总页数</param>
        /// <param name="pageIndex">返回的总记录数</param>
        /// <returns>返回DataSet</returns>
        public DataSet GetPagePro(string tableName, string fields, string orderField, string sqlWhere, int pageSize, int pageIndex, out int totalPage, out int RecordCount)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@TableName", SqlDbType.NText),
                    new SqlParameter("@Fields", SqlDbType.NText),
                    new SqlParameter("@OrderField", SqlDbType.NText),
                    new SqlParameter("@sqlWhere", SqlDbType.NText),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@pageIndex", SqlDbType.Int),
                    new SqlParameter("@TotalPage", SqlDbType.Int),
                    new SqlParameter("@RecordCount", SqlDbType.Int)
                    };
            parameters[0].Value = tableName;
            parameters[1].Value = fields;
            parameters[2].Value = orderField;
            parameters[3].Value = sqlWhere;
            parameters[4].Value = pageSize;
            parameters[5].Value = pageIndex;
            parameters[6].Direction = ParameterDirection.Output;
            parameters[7].Direction = ParameterDirection.Output;

            DataSet dt = DbHelperSQL.RunProcedure("ZXL_GetPageData", parameters, "ds");
            string n6 = parameters[6].Value.ToString();
            string n7 = parameters[7].Value.ToString();
            //totalPage = int.Parse(n6);
            //RecordCount = int.Parse(n7);
            if (!string.IsNullOrEmpty(n6))
            {
                //totalPage = int.Parse(n6);
            }
            if (!string.IsNullOrEmpty(n7))
            {
                // RecordCount = int.Parse(n7);
            }
            totalPage = !string.IsNullOrEmpty(n6) ? int.Parse(n6) : 0;
            RecordCount = !string.IsNullOrEmpty(n7) ? int.Parse(n7) : 0;
            return dt;
        }


        /// <summary>
        /// 公用分页存储过程调用
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">表中的字段，可以使用*代替</param>
        /// <param name="orderField">要排序的字段</param>
        /// <param name="sqlWhere">WHERE子句</param>
        /// <param name="pageSize">分页的大小</param>
        /// <param name="pageIndex">要显示的页的索引</param>
        /// <param name="totalPage">返回的总页数</param>
        /// <param name="recordCount">返回的总记录数</param>
        /// <returns>返回DataSet</returns>

        public static DataTable GetPageProBll(string tableName, string fields, string orderField, string sqlWhere, int pageSize, int pageIndex, out int totalPage, out int recordCount)
        {
            totalPage = 0;
            recordCount = 0;
            DataSet ds = new ProcUtility().GetPagePro(tableName, fields, orderField, sqlWhere, pageSize, pageIndex, out totalPage, out recordCount);
            return ds.Tables[0];
            //return DataTableToList(ds.Tables[0]);
        }
        #endregion

        #region 公用更新存储过程 bool Pro_Modify
        /// <summary>
        /// 公用更新存储过程
        /// </summary>
        /// <param name="tableName">传入表名</param>
        /// <param name="fields">传入需要更新的字段</param>
        /// <param name="where">传入更新的条件</param>
        /// <returns></returns>
        public int Pro_Modify(string tableName, string fields, string where)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@talbe",SqlDbType.NVarChar,100),
                    new SqlParameter("@fields",SqlDbType.NVarChar,3000),
                    new SqlParameter("@where",SqlDbType.NVarChar,500),
                    new SqlParameter("@rows",SqlDbType.Int)
                    };
            parameters[0].Value = tableName;
            parameters[1].Value = fields;
            parameters[2].Value = where;
            parameters[3].Direction = ParameterDirection.Output;
            int rows;
            DbHelperSQL.RunProcedure("Pro_Modify", parameters, out rows);
            return rows;
        }

        /// <summary>
        /// 公用更新存储过程 调用
        /// </summary>
        /// <param name="tableName">传入表名</param>
        /// <param name="fields">传入需要更新的字段</param>
        /// <param name="where">传入更新的条件</param>
        /// <returns></returns>
        public static bool Pro_ModifyBll(string tableName, string fields, string where)
        {
            int n = new ProcUtility().Pro_Modify(tableName, fields, where);
            if (n > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }

}
