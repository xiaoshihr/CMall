using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace BrnMall.Core.DBUtility
{
    public class T_SQL
    {
        public static string connstr = PubConstant.ConnectionString; 
     
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataAdapter adapter = null;
        SqlDataReader reader = null;
        DataSet ds = null;
         //本段代码的缺点是：没执行一个SQL语句，都会创建一次连接对象。因此效率低下。
        /// <summary>
        /// 用提供的函数，执行SQL命令，返回一个从指定连接的数据库记录集
        /// </summary>
        /// <remarks>
        /// 例如：
        /// SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">SqlConnection有效的SQL连接字符串</param>
        /// <param name="commandType">CommandType：CommandType.Text、CommandType.StoredProcedure</param>
        /// <param name="commandText">SQL语句或存储过程</param>
        /// <param name="commandParameters">SqlParameter[]参数数组</param>
        /// <returns>SqlDataReader：执行结果的记录集</returns>
        public SqlDataReader GetSqlReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connstr);

            // 我们在这里用 try/catch 是因为如果这个方法抛出异常，我们目的是关闭数据库连接，再抛出异常，
            // 因为这时不会有DataReader存在，此后commandBehaviour.CloseConnection将不会工作。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }


        /// <summary>
        /// 为执行命令做好准备：打开数据库连接，命令语句，设置命令类型（SQL语句或存储过程），函数语取。
        /// </summary>
        /// <param name="cmd">SqlCommand 组件</param>
        /// <param name="conn">SqlConnection 组件</param>
        /// <param name="trans">SqlTransaction 组件，可以为null</param>
        /// <param name="cmdType">语句类型：CommandType.Text、CommandType.StoredProcedure</param>
        /// <param name="cmdText">SQL语句，可以为存储过程</param>
        /// <param name="cmdParms">SQL参数数组</param>
        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
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
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        private void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, CommandType cmdType, string cmdText, OleDbParameter[] cmdParms)
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
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public OleDbDataReader GetOleReader(CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connstr);

            // 我们在这里用 try/catch 是因为如果这个方法抛出异常，我们目的是关闭数据库连接，再抛出异常，
            // 因为这时不会有DataReader存在，此后commandBehaviour.CloseConnection将不会工作。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 用提供的函数，执行SQL命令，返回一个从指定连接的数据库记录集
        /// </summary>
        /// <remarks>
        /// 例如：
        /// int count = cmd.ExecuteNonQuery();
        /// </remarks>
        /// <param name="connectionString">SqlConnection有效的SQL连接字符串</param>
        /// <param name="commandType">CommandType：CommandType.Text、CommandType.StoredProcedure</param>
        /// <param name="commandText">SQL语句或存储过程</param>
        /// <param name="commandParameters">SqlParameter[]参数数组</param>
        /// <returns>SqlDataReader：执行结果的记录集</returns>
        public int GetSqlCount(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connstr);

            // 我们在这里用 try/catch 是因为如果这个方法抛出异常，我们目的是关闭数据库连接，再抛出异常，
            // 因为这时不会有DataReader存在，此后commandBehaviour.CloseConnection将不会工作。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return count;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public int GetOleCount(CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connstr);

            // 我们在这里用 try/catch 是因为如果这个方法抛出异常，我们目的是关闭数据库连接，再抛出异常，
            // 因为这时不会有DataReader存在，此后commandBehaviour.CloseConnection将不会工作。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return count;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public DataSet GetOleDataSet(CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connstr);

            // 我们在这里用 try/catch 是因为如果这个方法抛出异常，我们目的是关闭数据库连接，再抛出异常，
            // 因为这时不会有DataReader存在，此后commandBehaviour.CloseConnection将不会工作。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds, "tablename");
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataSet GetSqlDataSet(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connstr);

            // 我们在这里用 try/catch 是因为如果这个方法抛出异常，我们目的是关闭数据库连接，再抛出异常，
            // 因为这时不会有DataReader存在，此后commandBehaviour.CloseConnection将不会工作。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds, "tablename");
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public object GetOleOne(CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connstr);

            // 我们在这里用 try/catch 是因为如果这个方法抛出异常，我们目的是关闭数据库连接，再抛出异常，
            // 因为这时不会有DataReader存在，此后commandBehaviour.CloseConnection将不会工作。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object one = cmd.ExecuteScalar();
                return one;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public object GetSqlOne(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connstr);

            // 我们在这里用 try/catch 是因为如果这个方法抛出异常，我们目的是关闭数据库连接，再抛出异常，
            // 因为这时不会有DataReader存在，此后commandBehaviour.CloseConnection将不会工作。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object one = cmd.ExecuteScalar();
                return one;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public object SelectOne(string sql)//返回第一行第一列<即第一个单元格>的值
        {
            try
            {
                //1
                conn = new SqlConnection();
                conn.ConnectionString = connstr;
                //2 
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                object obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception e)
            {
                conn.Close();
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<object> SelectOneColumn(string sql)//返回一列
        {
            try
            {
                //1
                conn = new SqlConnection();
                conn.ConnectionString = connstr;
                //2  
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                reader=cmd.ExecuteReader();
                List<object> list = new List<object>();
                while (reader.Read())
                {
                    list.Add(reader.GetValue(0));
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {
                conn.Close();
            }
        }

       public List<object> SelectOneColumn(string sql,int n)//返回指定的列  是上个方法的重载，同名，同返回值，不同参数 
        {
            try
            {
                //1
                conn = new SqlConnection();
                conn.ConnectionString = connstr;
                //2  
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                reader = cmd.ExecuteReader();
                List<object> list = new List<object>();
                while (reader.Read())
                {
                    list.Add(reader.GetValue(n));
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {
                conn.Close();
            }
        }

         public List<object> SelectOneRow(string sql)//返回第一行的值
        {
            List<object> list = new List<object>();
            try
            {
                //1
                conn = new SqlConnection();
                conn.ConnectionString = connstr;
                //2  
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        list.Add(reader.GetValue(i));
                    }
                }
            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {
                conn.Close();
            }
            return list;
        }

       public List<object> SelectOneRow(string sql,int n)//返回指定行的值  是上一个方法的重载
        {
            List<object> list = new List<object>();
            try
            {
                //1
                conn = new SqlConnection();
                conn.ConnectionString = connstr;
                //2  
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                reader = cmd.ExecuteReader();
                int k = 1;//代表默认状态下获取的是第一行的值
                while (reader.Read())
                {
                    if (k == n)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            list.Add(reader.GetValue(i));
                        }
                    }
                    k++;
                }

            }
            catch (Exception e)
            {
                conn.Close();
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return list;
        }      

        public DataSet SelectDs(string sql)
        {
            ds = new DataSet();
            try
            {
                //1
                conn = new SqlConnection();
                conn.ConnectionString = connstr;
                //2
                adapter = new SqlDataAdapter();

                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "ado");
            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataTable SelectDt(string sql)
        {
            DataTable ds = new DataTable();
            try
            {
                //1
                conn = new SqlConnection();
                conn.ConnectionString = connstr;
                //2
                adapter = new SqlDataAdapter();

                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {
                conn.Close();
            }
            return ds;
        } 

         public bool Update(string sql)
        {
            bool b = false;
            try
            {
                //1
                conn = new SqlConnection();
                conn.ConnectionString = connstr;
                conn.Open();
                //2 
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();
                b = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if(conn!=null)
                {
                  conn.Close();
                }
            }
            return b;
        }

         public bool NewUpdate(string sql)
        {
            bool b = false;
            try
            {
                using (conn = new SqlConnection(connstr))
                { 
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    { 
                        cmd.ExecuteNonQuery();
                        b = true;
                    }
                }                
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }        
            return b;
        }

         public bool Update(string sql, SqlParameter[] p)//在调用本方法之前，必须确定参数的数目！参数SqlParameter的长度是固定的！
        {
            bool b = false;
            try
            {
                using (conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        foreach (SqlParameter a in p)
                        {
                            cmd.Parameters.Add(a);
                        }
                        cmd.ExecuteNonQuery();
                        b = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return b;
        }

        public object SelectOne(string sql, params SqlParameter[] p)
        {
            object o = new object();
            try
            {
                //1
                conn = new SqlConnection();
                conn.ConnectionString = connstr;
                //2 
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                foreach (SqlParameter a in p)
                {
                    cmd.Parameters.Add(a);
                }
                conn.Open();
                o = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                conn.Close();
                throw e;

            }
            finally
            {
                conn.Close();
            }
            return o;
        }

        public bool Update(string sql, List<SqlParameter> p)//参数SqlParameter的长度是可变的
        {
            bool b = false;
            try
            {
                using (conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        foreach (SqlParameter a in p)
                        {
                            cmd.Parameters.Add(a);
                        }
                        cmd.ExecuteNonQuery();
                        b = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return b;
        }

        public  int sqlExecuteNonquery(SqlCommand cmd)
        {
            SqlConnection conn = new SqlConnection(connstr);
            cmd.Connection = conn;
            try
            {
                //打开连接
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //执行SQL语句并返回 
                return cmd.ExecuteNonQuery();
            }
            //异常处理
            catch (Exception e)
            {
                //抛出异常
                throw e;
            }
            finally
            {
                //关闭连接
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public  SqlDataReader sqlExecuteder(SqlCommand cmd)
        {
            SqlConnection conn = new SqlConnection(connstr);
            cmd.Connection = conn;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }
            //finally
            //{
            //    if (conn.State == ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //}
        }

        public  DataSet GetDataSet(SqlCommand cmd, string tablename)
        {
            SqlConnection conn = new SqlConnection(connstr);
            cmd.Connection = conn;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds, tablename);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public  object GetFrist(SqlCommand cmd)
        {
            SqlConnection conn = new SqlConnection(connstr);
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.ExecuteScalar();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
        }

        public  int GetAdd(string sql)
        {
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand(sql, conn);
            int count;
            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public  object[] GetDataReader(SqlCommand cmd,int J)//int J表示要返回的字段的数目  适用于查找一行！！！！
        {
            SqlConnection conn = new SqlConnection(connstr);
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            reader = cmd.ExecuteReader();
            try
            {
                object[] obj = new object[J];
                if (reader.Read())
                {
                    for (int i = 0; i < J; i++)
                    {
                        obj[i] = reader[i].ToString();
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
        }

        public  object[,] GetDataReaderSec(SqlCommand cmd, int J, int K)//先列数后行数//int J表示要返回的字段的数目！！！！即：列数
        {                                                           //K代表要返回的行数
            SqlConnection conn = new SqlConnection(connstr);
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            reader = cmd.ExecuteReader();
            try
            {
                object[,] obj = new object[J,K];
                    for (int i = 0; i < K; i++)
                    {
                        for (int z = 0; z < J; z++)
                        {
                            if (reader.Read())
                            {
                                obj[z, i] = reader[z].ToString();
                            }
                        }
                    }

                
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
        }

        public  DataSet GetSelProcDs(string ProcName)//没有参数的存储过程@
        {
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand(ProcName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                adapter.Fill(ds, "tablename");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public DataSet GetSelProcDs(string ProcName, params SqlParameter[] p)//只有输入参数的存储过程@也可以没有任何参数！
        {
            DataSet ds = new DataSet();
            try
            {
                conn = new SqlConnection(connstr);
                cmd = new SqlCommand(ProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter a in p)
                {
                    cmd.Parameters.Add(a);
                    a.Direction = ParameterDirection.Input;
                }
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                adapter.Fill(ds, "tablename");
                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public DataSet GetSelProcDs(string ProcName, List<SqlParameter> p,List<SqlParameter> q)//带有输入参数和输出参数的存储过程@
        {
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand(ProcName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter a in p)
            {
                cmd.Parameters.Add(a);
                a.Direction = ParameterDirection.Input;
            }
            foreach (SqlParameter a in q)
            {
                cmd.Parameters.Add(a);
                a.Direction = ParameterDirection.Output;
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataSet ds = new DataSet();
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "tablename");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public DataSet GetSelProcDs(string ProcName, List<SqlParameter> p)//只带有输出参数的存储过程@
        {
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand(ProcName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter a in p)
            {
                cmd.Parameters.Add(a);
                a.Direction = ParameterDirection.Output;
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataSet ds = new DataSet();
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "tablename");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public int GetUpdateProc(string ProcName)//没有任何参数的存储过程，用于更新！@
        {
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand(ProcName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                int count = cmd.ExecuteNonQuery();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public int GetUpdateProc(string ProcName, List<SqlParameter> p)//带有输入参数的存储过程3
        {
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand(ProcName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter a in p)
            {
                cmd.Parameters.Add(a);
                a.Direction = ParameterDirection.Input;
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                int count = cmd.ExecuteNonQuery();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void GetTransactions(string sql1,string sql2)
        {
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlTransaction dt = conn.BeginTransaction();
            cmd.Transaction = dt;
            try
            {
                cmd.CommandText = sql1;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                dt.Commit();

            }
            catch
            {
                dt.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }


        public bool GetTransaction(string sql1, string sql2)
        {

            bool b = false;
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlTransaction dt = conn.BeginTransaction();
            cmd.Transaction = dt;
            try
            {
                cmd.CommandText = sql1;
                int c1 = cmd.ExecuteNonQuery();

                cmd.CommandText = sql2;
                int c2 = cmd.ExecuteNonQuery();

                dt.Commit();
                b = true;

            }

            catch
            {
                dt.Rollback();
            }
            finally
            {
                conn.Close();

            }
            return b;
        }

        public bool GetTransaction(string sql1, string sql2, string sql3)
        {

            bool b = false;
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlTransaction dt = conn.BeginTransaction();
            cmd.Transaction = dt;
            try
            {
                cmd.CommandText = sql1;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql3;
                cmd.ExecuteNonQuery();
                dt.Commit();
                b = true;

            }

            catch
            {
                dt.Rollback();
            }
            finally
            {
                conn.Close();

            }
            return b;
        }

        public bool GetTransaction(string sql1, string sql2, string sql3, string sql4)
        {
            bool B = false;
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlTransaction dt = conn.BeginTransaction();
            cmd.Transaction = dt;
            try
            {
                cmd.CommandText = sql1;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql3;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql4;
                cmd.ExecuteNonQuery();
                dt.Commit();
                B = true;

            }
            catch
            {
                dt.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return B;
        }

        public bool GetTransaction(string sql1, string sql2, string sql3, string sql4, string sql5)
        {
            bool B = false;
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlTransaction dt = conn.BeginTransaction();
            cmd.Transaction = dt;
            try
            {
                cmd.CommandText = sql1;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql3;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql4;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql5;
                cmd.ExecuteNonQuery();
                dt.Commit();
                B = true;

            }
            catch
            {
                dt.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return B;
        }


        public bool GetTransaction(string sql1, string sql2, string sql3, string sql4, string sql5,string sql6)
        {
            bool B = false;
            conn = new SqlConnection(connstr);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlTransaction dt = conn.BeginTransaction();
            cmd.Transaction = dt;
            try
            {
                cmd.CommandText = sql1;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql3;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql4;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql5;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql6;
                cmd.ExecuteNonQuery();
                dt.Commit();
                B = true;

            }
            catch
            {
                dt.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return B;
      
        }
    }
      
}

