using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BrnMall.Core.DBUtility;//Please add references
namespace BrnMall.Core.DAL
{
	/// <summary>
	/// 数据访问类:YX_weiXinMenus
	/// </summary>
	public partial class YX_weiXinMenus
	{
		public YX_weiXinMenus()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "YX_weiXinMenus"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YX_weiXinMenus");
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
		public int Add(BrnMall.Core.Model.YX_weiXinMenus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YX_weiXinMenus(");
			strSql.Append("WX_menuName,WX_MenuType,WX_MenusKey_URL,WX_Fid,WX_AddTime,remark1,remark2,remark3,flat1,flat2,remark4,remark5,remark6,flat7,flat8,RegTim1,RegTim2)");
			strSql.Append(" values (");
			strSql.Append("@WX_menuName,@WX_MenuType,@WX_MenusKey_URL,@WX_Fid,@WX_AddTime,@remark1,@remark2,@remark3,@flat1,@flat2,@remark4,@remark5,@remark6,@flat7,@flat8,@RegTim1,@RegTim2)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@WX_menuName", SqlDbType.NVarChar,20),
					new SqlParameter("@WX_MenuType", SqlDbType.VarChar,20),
					new SqlParameter("@WX_MenusKey_URL", SqlDbType.VarChar,1000),
					new SqlParameter("@WX_Fid", SqlDbType.Int,4),
					new SqlParameter("@WX_AddTime", SqlDbType.DateTime),
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
			parameters[0].Value = model.WX_menuName;
			parameters[1].Value = model.WX_MenuType;
			parameters[2].Value = model.WX_MenusKey_URL;
			parameters[3].Value = model.WX_Fid;
			parameters[4].Value = model.WX_AddTime;
			parameters[5].Value = model.remark1;
			parameters[6].Value = model.remark2;
			parameters[7].Value = model.remark3;
			parameters[8].Value = model.flat1;
			parameters[9].Value = model.flat2;
			parameters[10].Value = model.remark4;
			parameters[11].Value = model.remark5;
			parameters[12].Value = model.remark6;
			parameters[13].Value = model.flat7;
			parameters[14].Value = model.flat8;
			parameters[15].Value = model.RegTim1;
			parameters[16].Value = model.RegTim2;

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
		public bool Update(BrnMall.Core.Model.YX_weiXinMenus model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YX_weiXinMenus set ");
			strSql.Append("WX_menuName=@WX_menuName,");
			strSql.Append("WX_MenuType=@WX_MenuType,");
			strSql.Append("WX_MenusKey_URL=@WX_MenusKey_URL,");
			strSql.Append("WX_Fid=@WX_Fid,");
			strSql.Append("WX_AddTime=@WX_AddTime,");
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
					new SqlParameter("@WX_menuName", SqlDbType.NVarChar,20),
					new SqlParameter("@WX_MenuType", SqlDbType.VarChar,20),
					new SqlParameter("@WX_MenusKey_URL", SqlDbType.VarChar,1000),
					new SqlParameter("@WX_Fid", SqlDbType.Int,4),
					new SqlParameter("@WX_AddTime", SqlDbType.DateTime),
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
			parameters[0].Value = model.WX_menuName;
			parameters[1].Value = model.WX_MenuType;
			parameters[2].Value = model.WX_MenusKey_URL;
			parameters[3].Value = model.WX_Fid;
			parameters[4].Value = model.WX_AddTime;
			parameters[5].Value = model.remark1;
			parameters[6].Value = model.remark2;
			parameters[7].Value = model.remark3;
			parameters[8].Value = model.flat1;
			parameters[9].Value = model.flat2;
			parameters[10].Value = model.remark4;
			parameters[11].Value = model.remark5;
			parameters[12].Value = model.remark6;
			parameters[13].Value = model.flat7;
			parameters[14].Value = model.flat8;
			parameters[15].Value = model.RegTim1;
			parameters[16].Value = model.RegTim2;
			parameters[17].Value = model.Id;

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
			strSql.Append("delete from YX_weiXinMenus ");
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
			strSql.Append("delete from YX_weiXinMenus ");
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
		public BrnMall.Core.Model.YX_weiXinMenus GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,WX_menuName,WX_MenuType,WX_MenusKey_URL,WX_Fid,WX_AddTime,remark1,remark2,remark3,flat1,flat2,remark4,remark5,remark6,flat7,flat8,RegTim1,RegTim2 from YX_weiXinMenus ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			BrnMall.Core.Model.YX_weiXinMenus model=new BrnMall.Core.Model.YX_weiXinMenus();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WX_menuName"]!=null && ds.Tables[0].Rows[0]["WX_menuName"].ToString()!="")
				{
					model.WX_menuName=ds.Tables[0].Rows[0]["WX_menuName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["WX_MenuType"]!=null && ds.Tables[0].Rows[0]["WX_MenuType"].ToString()!="")
				{
					model.WX_MenuType=ds.Tables[0].Rows[0]["WX_MenuType"].ToString();
				}
				if(ds.Tables[0].Rows[0]["WX_MenusKey_URL"]!=null && ds.Tables[0].Rows[0]["WX_MenusKey_URL"].ToString()!="")
				{
					model.WX_MenusKey_URL=ds.Tables[0].Rows[0]["WX_MenusKey_URL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["WX_Fid"]!=null && ds.Tables[0].Rows[0]["WX_Fid"].ToString()!="")
				{
					model.WX_Fid=int.Parse(ds.Tables[0].Rows[0]["WX_Fid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WX_AddTime"]!=null && ds.Tables[0].Rows[0]["WX_AddTime"].ToString()!="")
				{
					model.WX_AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["WX_AddTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["remark1"]!=null && ds.Tables[0].Rows[0]["remark1"].ToString()!="")
				{
					model.remark1=ds.Tables[0].Rows[0]["remark1"].ToString();
				}
				if(ds.Tables[0].Rows[0]["remark2"]!=null && ds.Tables[0].Rows[0]["remark2"].ToString()!="")
				{
					model.remark2=ds.Tables[0].Rows[0]["remark2"].ToString();
				}
				if(ds.Tables[0].Rows[0]["remark3"]!=null && ds.Tables[0].Rows[0]["remark3"].ToString()!="")
				{
					model.remark3=ds.Tables[0].Rows[0]["remark3"].ToString();
				}
				if(ds.Tables[0].Rows[0]["flat1"]!=null && ds.Tables[0].Rows[0]["flat1"].ToString()!="")
				{
					model.flat1=int.Parse(ds.Tables[0].Rows[0]["flat1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["flat2"]!=null && ds.Tables[0].Rows[0]["flat2"].ToString()!="")
				{
					model.flat2=int.Parse(ds.Tables[0].Rows[0]["flat2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["remark4"]!=null && ds.Tables[0].Rows[0]["remark4"].ToString()!="")
				{
					model.remark4=ds.Tables[0].Rows[0]["remark4"].ToString();
				}
				if(ds.Tables[0].Rows[0]["remark5"]!=null && ds.Tables[0].Rows[0]["remark5"].ToString()!="")
				{
					model.remark5=ds.Tables[0].Rows[0]["remark5"].ToString();
				}
				if(ds.Tables[0].Rows[0]["remark6"]!=null && ds.Tables[0].Rows[0]["remark6"].ToString()!="")
				{
					model.remark6=ds.Tables[0].Rows[0]["remark6"].ToString();
				}
				if(ds.Tables[0].Rows[0]["flat7"]!=null && ds.Tables[0].Rows[0]["flat7"].ToString()!="")
				{
					model.flat7=int.Parse(ds.Tables[0].Rows[0]["flat7"].ToString());
				}
				if(ds.Tables[0].Rows[0]["flat8"]!=null && ds.Tables[0].Rows[0]["flat8"].ToString()!="")
				{
					model.flat8=int.Parse(ds.Tables[0].Rows[0]["flat8"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegTim1"]!=null && ds.Tables[0].Rows[0]["RegTim1"].ToString()!="")
				{
					model.RegTim1=DateTime.Parse(ds.Tables[0].Rows[0]["RegTim1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegTim2"]!=null && ds.Tables[0].Rows[0]["RegTim2"].ToString()!="")
				{
					model.RegTim2=DateTime.Parse(ds.Tables[0].Rows[0]["RegTim2"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,WX_menuName,WX_MenuType,WX_MenusKey_URL,WX_Fid,WX_AddTime,remark1,remark2,remark3,flat1,flat2,remark4,remark5,remark6,flat7,flat8,RegTim1,RegTim2 ");
			strSql.Append(" FROM YX_weiXinMenus ");
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
			strSql.Append(" Id,WX_menuName,WX_MenuType,WX_MenusKey_URL,WX_Fid,WX_AddTime,remark1,remark2,remark3,flat1,flat2,remark4,remark5,remark6,flat7,flat8,RegTim1,RegTim2 ");
			strSql.Append(" FROM YX_weiXinMenus ");
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
			strSql.Append("select count(1) FROM YX_weiXinMenus ");
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
			strSql.Append(")AS Row, T.*  from YX_weiXinMenus T ");
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
			parameters[0].Value = "YX_weiXinMenus";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

