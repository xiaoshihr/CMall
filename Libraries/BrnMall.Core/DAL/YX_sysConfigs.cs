using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BrnMall.Core.DBUtility;//Please add references
namespace BrnMall.Core.DAL
{
	/// <summary>
	/// 数据访问类:YX_sysConfigs
	/// </summary>
	public partial class YX_sysConfigs
	{
        T_SQL imp = new T_SQL();
		public YX_sysConfigs()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "YX_sysConfigs"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_sysConfigs");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(BrnMall.Core.Model.YX_sysConfigs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_sysConfigs(");
			strSql.Append("wxkey,wxkeyvalue,wxkeytime,remark1,remark2,remark3,flat1,flat2,remark4,remark5,remark6,flat7,flat8,RegTim1,RegTim2)");
			strSql.Append(" values (");
			strSql.Append("@wxkey,@wxkeyvalue,@wxkeytime,@remark1,@remark2,@remark3,@flat1,@flat2,@remark4,@remark5,@remark6,@flat7,@flat8,@RegTim1,@RegTim2)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@wxkey", SqlDbType.VarChar,100),
					new SqlParameter("@wxkeyvalue", SqlDbType.VarChar,200),
					new SqlParameter("@wxkeytime", SqlDbType.DateTime),
					new SqlParameter("@remark1", SqlDbType.NVarChar,500),
					new SqlParameter("@remark2", SqlDbType.NVarChar,500),
					new SqlParameter("@remark3", SqlDbType.NText),
					new SqlParameter("@flat1", SqlDbType.Int,4),
					new SqlParameter("@flat2", SqlDbType.Int,4),
					new SqlParameter("@remark4", SqlDbType.NVarChar,50),
					new SqlParameter("@remark5", SqlDbType.NVarChar,50),
					new SqlParameter("@remark6", SqlDbType.NText),
					new SqlParameter("@flat7", SqlDbType.Int,4),
					new SqlParameter("@flat8", SqlDbType.Int,4),
					new SqlParameter("@RegTim1", SqlDbType.DateTime),
					new SqlParameter("@RegTim2", SqlDbType.DateTime)};
			parameters[0].Value = model.wxkey;
			parameters[1].Value = model.wxkeyvalue;
			parameters[2].Value = model.wxkeytime;
			parameters[3].Value = model.remark1;
			parameters[4].Value = model.remark2;
			parameters[5].Value = model.remark3;
			parameters[6].Value = model.flat1;
			parameters[7].Value = model.flat2;
			parameters[8].Value = model.remark4;
			parameters[9].Value = model.remark5;
			parameters[10].Value = model.remark6;
			parameters[11].Value = model.flat7;
			parameters[12].Value = model.flat8;
			parameters[13].Value = model.RegTim1;
			parameters[14].Value = model.RegTim2;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(BrnMall.Core.Model.YX_sysConfigs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_sysConfigs set ");
			strSql.Append("wxkey=@wxkey,");
			strSql.Append("wxkeyvalue=@wxkeyvalue,");
			strSql.Append("wxkeytime=@wxkeytime,");
			strSql.Append("remark1=@remark1,");
			strSql.Append("remark2=@remark2,");
			strSql.Append("remark3=@remark3,");
			strSql.Append("flat1=@flat1,");
			strSql.Append("flat2=@flat2,");
			strSql.Append("remark4=@remark4,");
			strSql.Append("remark5=@remark5,");
			strSql.Append("remark6=@remark6,");
			strSql.Append("flat7=@flat7,");
			strSql.Append("flat8=@flat8,");
			strSql.Append("RegTim1=@RegTim1,");
			strSql.Append("RegTim2=@RegTim2");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@wxkey", SqlDbType.VarChar,100),
					new SqlParameter("@wxkeyvalue", SqlDbType.VarChar,200),
					new SqlParameter("@wxkeytime", SqlDbType.DateTime),
					new SqlParameter("@remark1", SqlDbType.NVarChar,500),
					new SqlParameter("@remark2", SqlDbType.NVarChar,500),
					new SqlParameter("@remark3", SqlDbType.NText),
					new SqlParameter("@flat1", SqlDbType.Int,4),
					new SqlParameter("@flat2", SqlDbType.Int,4),
					new SqlParameter("@remark4", SqlDbType.NVarChar,50),
					new SqlParameter("@remark5", SqlDbType.NVarChar,50),
					new SqlParameter("@remark6", SqlDbType.NText),
					new SqlParameter("@flat7", SqlDbType.Int,4),
					new SqlParameter("@flat8", SqlDbType.Int,4),
					new SqlParameter("@RegTim1", SqlDbType.DateTime),
					new SqlParameter("@RegTim2", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.wxkey;
			parameters[1].Value = model.wxkeyvalue;
			parameters[2].Value = model.wxkeytime;
			parameters[3].Value = model.remark1;
			parameters[4].Value = model.remark2;
			parameters[5].Value = model.remark3;
			parameters[6].Value = model.flat1;
			parameters[7].Value = model.flat2;
			parameters[8].Value = model.remark4;
			parameters[9].Value = model.remark5;
			parameters[10].Value = model.remark6;
			parameters[11].Value = model.flat7;
			parameters[12].Value = model.flat8;
			parameters[13].Value = model.RegTim1;
			parameters[14].Value = model.RegTim2;
			parameters[15].Value = model.Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_sysConfigs ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YX_sysConfigs ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BrnMall.Core.Model.YX_sysConfigs GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,wxkey,wxkeyvalue,wxkeytime,remark1,remark2,remark3,flat1,flat2,remark4,remark5,remark6,flat7,flat8,RegTim1,RegTim2 from YX_sysConfigs ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			BrnMall.Core.Model.YX_sysConfigs model=new BrnMall.Core.Model.YX_sysConfigs();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BrnMall.Core.Model.YX_sysConfigs DataRowToModel(DataRow row)
		{
			BrnMall.Core.Model.YX_sysConfigs model=new BrnMall.Core.Model.YX_sysConfigs();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["wxkey"]!=null)
				{
					model.wxkey=row["wxkey"].ToString();
				}
				if(row["wxkeyvalue"]!=null)
				{
					model.wxkeyvalue=row["wxkeyvalue"].ToString();
				}
				if(row["wxkeytime"]!=null && row["wxkeytime"].ToString()!="")
				{
					model.wxkeytime=DateTime.Parse(row["wxkeytime"].ToString());
				}
				if(row["remark1"]!=null)
				{
					model.remark1=row["remark1"].ToString();
				}
				if(row["remark2"]!=null)
				{
					model.remark2=row["remark2"].ToString();
				}
				if(row["remark3"]!=null)
				{
					model.remark3=row["remark3"].ToString();
				}
				if(row["flat1"]!=null && row["flat1"].ToString()!="")
				{
					model.flat1=int.Parse(row["flat1"].ToString());
				}
				if(row["flat2"]!=null && row["flat2"].ToString()!="")
				{
					model.flat2=int.Parse(row["flat2"].ToString());
				}
				if(row["remark4"]!=null)
				{
					model.remark4=row["remark4"].ToString();
				}
				if(row["remark5"]!=null)
				{
					model.remark5=row["remark5"].ToString();
				}
				if(row["remark6"]!=null)
				{
					model.remark6=row["remark6"].ToString();
				}
				if(row["flat7"]!=null && row["flat7"].ToString()!="")
				{
					model.flat7=int.Parse(row["flat7"].ToString());
				}
				if(row["flat8"]!=null && row["flat8"].ToString()!="")
				{
					model.flat8=int.Parse(row["flat8"].ToString());
				}
				if(row["RegTim1"]!=null && row["RegTim1"].ToString()!="")
				{
					model.RegTim1=DateTime.Parse(row["RegTim1"].ToString());
				}
				if(row["RegTim2"]!=null && row["RegTim2"].ToString()!="")
				{
					model.RegTim2=DateTime.Parse(row["RegTim2"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,wxkey,wxkeyvalue,wxkeytime,remark1,remark2,remark3,flat1,flat2,remark4,remark5,remark6,flat7,flat8,RegTim1,RegTim2 ");
			strSql.Append(" FROM YX_sysConfigs ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,wxkey,wxkeyvalue,wxkeytime,remark1,remark2,remark3,flat1,flat2,remark4,remark5,remark6,flat7,flat8,RegTim1,RegTim2 ");
			strSql.Append(" FROM YX_sysConfigs ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM YX_sysConfigs ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from YX_sysConfigs T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "YX_sysConfigs";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        public string GetKeyValue(string key)
        {
            string sql = "select wxkeyvalue from YX_sysConfigs where wxkey='" + key + "'";
            object o = imp.GetSqlOne(CommandType.Text, sql);
            if (o != null)
            {
                return o.ToString();
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 是否存在该记录 不存在  则插入配置   存在则更新配置
        /// </summary>
        public void isExists(string wxkey,string wxkeyvalue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from YX_sysConfigs");
            strSql.Append(" where wxkey=@wxkey");
            SqlParameter[] parameters = {
					new SqlParameter("@wxkey", SqlDbType.VarChar,50)
			};
            parameters[0].Value = wxkey;
            string sql = "";
            if (DbHelperSQL.Exists(strSql.ToString(), parameters))//如果存在 则更新
            {
                sql = "update YX_sysConfigs set wxkeyvalue='" + wxkeyvalue + "' where wxkey='" + wxkey + "'";
            }
            else//不存在 则插入相关记录
            {
                sql = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('"+wxkey+"','"+wxkeyvalue+"','" + DateTime.Now + "')";
            }
            imp.GetSqlCount(CommandType.Text, sql);
        }
		#endregion  ExtensionMethod
	}
}

