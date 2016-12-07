/**  版本信息模板在安装目录下，可自行修改。
* SendBag.cs
*
* 功 能： N/A
* 类 名： SendBag
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/4/7 星期四 14:13:25   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BrnMall.Core.DBUtility;//Please add references
namespace BrnMall.Core.DAL
{
	/// <summary>
	/// 数据访问类:SendBag
	/// </summary>
	public partial class SendBag
	{
		public SendBag()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "SendBag"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SendBag");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(BrnMall.Core.Model.SendBag model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SendBag(");
			strSql.Append("Pice,SenderID,ReceiverID,SendType,Status,Addtime,SendTime)");
			strSql.Append(" values (");
			strSql.Append("@Pice,@SenderID,@ReceiverID,@SendType,@Status,@Addtime,@SendTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Pice", SqlDbType.Int,4),
					new SqlParameter("@SenderID", SqlDbType.Int,4),
					new SqlParameter("@ReceiverID", SqlDbType.Int,4),
					new SqlParameter("@SendType", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Addtime", SqlDbType.DateTime),
					new SqlParameter("@SendTime", SqlDbType.DateTime)};
			parameters[0].Value = model.Pice;
			parameters[1].Value = model.SenderID;
			parameters[2].Value = model.ReceiverID;
			parameters[3].Value = model.SendType;
			parameters[4].Value = model.Status;
			parameters[5].Value = model.Addtime;
			parameters[6].Value = model.SendTime;

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
		public bool Update(BrnMall.Core.Model.SendBag model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SendBag set ");
			strSql.Append("Pice=@Pice,");
			strSql.Append("SenderID=@SenderID,");
			strSql.Append("ReceiverID=@ReceiverID,");
			strSql.Append("SendType=@SendType,");
			strSql.Append("Status=@Status,");
			strSql.Append("Addtime=@Addtime,");
			strSql.Append("SendTime=@SendTime");
            strSql.Append("IsNotSend=@IsNotSend,");

            strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@Pice", SqlDbType.Int,4),
					new SqlParameter("@SenderID", SqlDbType.Int,4),
					new SqlParameter("@ReceiverID", SqlDbType.Int,4),
					new SqlParameter("@SendType", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Addtime", SqlDbType.DateTime),
                    
                    new SqlParameter("@SendTime", SqlDbType.DateTime),
                    new SqlParameter("@IsNotSend", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.Pice;
			parameters[1].Value = model.SenderID;
			parameters[2].Value = model.ReceiverID;
			parameters[3].Value = model.SendType;
			parameters[4].Value = model.Status;
			parameters[5].Value = model.Addtime;
			parameters[6].Value = model.SendTime;
            parameters[7].Value = model.IsNotSend;
            parameters[8].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SendBag ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SendBag ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public BrnMall.Core.Model.SendBag GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,Mchbillno,Pice,SenderID,ReceiverID,SendType,Status,Addtime,SendTime from SendBag ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			BrnMall.Core.Model.SendBag model=new BrnMall.Core.Model.SendBag();
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
		public BrnMall.Core.Model.SendBag DataRowToModel(DataRow row)
		{
			BrnMall.Core.Model.SendBag model=new BrnMall.Core.Model.SendBag();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
                if (row["Mchbillno"] != null)
                {
                    model.Mchbillno = row["Mchbillno"].ToString();
                }
                if (row["Pice"]!=null && row["Pice"].ToString()!="")
				{
					model.Pice=int.Parse(row["Pice"].ToString());
				}
				if(row["SenderID"]!=null && row["SenderID"].ToString()!="")
				{
					model.SenderID=int.Parse(row["SenderID"].ToString());
				}
				if(row["ReceiverID"]!=null && row["ReceiverID"].ToString()!="")
				{
					model.ReceiverID=int.Parse(row["ReceiverID"].ToString());
				}
				if(row["SendType"]!=null && row["SendType"].ToString()!="")
				{
					model.SendType=int.Parse(row["SendType"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["Addtime"]!=null && row["Addtime"].ToString()!="")
				{
					model.Addtime=DateTime.Parse(row["Addtime"].ToString());
				}
				if(row["SendTime"]!=null && row["SendTime"].ToString()!="")
				{
					model.SendTime=DateTime.Parse(row["SendTime"].ToString());
				}
                if (row["OID"] != null && row["OID"].ToString() != "")
                {
                    model.OID = int.Parse(row["OID"].ToString());
                }
                if (row["IsNotSend"] != null && row["IsNotSend"].ToString() != "")
                {
                    model.IsNotSend = int.Parse(row["IsNotSend"].ToString());
                }
                //if (row["avatar"] != null && row["avatar"].ToString() != "")
                //{
                //    model.ModelUser.avatar = row["avatar"].ToString();
                //}
                //if (row["Userno"] != null && row["Userno"].ToString() != "")
                //{
                //    model.ModelUser.Userno =int.Parse( row["Userno"].ToString());
                //}
                //if (row["UserLevel"] != null && row["UserLevel"].ToString() != "")
                //{
                //    model.ModelUser.Userno = int.Parse(row["UserLevel"].ToString());
                //}
            }
			return model;
		}

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BrnMall.Core.Model.SendBag DataRowToModel1(DataRow row)
        {
            BrnMall.Core.Model.SendBag model = new BrnMall.Core.Model.SendBag();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Mchbillno"] != null)
                {
                    model.Mchbillno = row["Mchbillno"].ToString();
                }
                if (row["Pice"] != null && row["Pice"].ToString() != "")
                {
                    model.Pice = int.Parse(row["Pice"].ToString());
                }
                if (row["SenderID"] != null && row["SenderID"].ToString() != "")
                {
                    model.SenderID = int.Parse(row["SenderID"].ToString());
                }
                if (row["ReceiverID"] != null && row["ReceiverID"].ToString() != "")
                {
                    model.ReceiverID = int.Parse(row["ReceiverID"].ToString());
                }
                if (row["SendType"] != null && row["SendType"].ToString() != "")
                {
                    model.SendType = int.Parse(row["SendType"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Addtime"] != null && row["Addtime"].ToString() != "")
                {
                    model.Addtime = DateTime.Parse(row["Addtime"].ToString());
                }
                if (row["SendTime"] != null && row["SendTime"].ToString() != "")
                {
                    model.SendTime = DateTime.Parse(row["SendTime"].ToString());
                }
                if (row["OID"] != null && row["OID"].ToString() != "")
                {
                    model.OID = int.Parse(row["OID"].ToString());
                }

                if (row["Noncestr"] != null)
                {
                    model.Noncestr = row["Noncestr"].ToString();
                }


                if (row["PaySign"] != null)
                {
                    model.PaySign = row["PaySign"].ToString();
                }

                if (row["IsNotSend"] != null && row["IsNotSend"].ToString() != "")
                {
                    model.IsNotSend = int.Parse(row["IsNotSend"].ToString());
                }
                if (row["username"] != null && row["username"].ToString() != "")
                {
                    model.ModelUser = new BrnMall.Core.Model.bma_users
                    {
                        username = row["username"].ToString(),
                        avatar = row["avatar"].ToString(),
                        Userno = int.Parse(row["Userno"].ToString()),
                        UserLevel = int.Parse(row["UserLevel"].ToString())
                    };
                }
                //if (row["avatar"] != null && row["avatar"].ToString() != "")
                //{
                //    model.ModelUser.avatar = row["avatar"].ToString();
                //}
                //if (row["Userno"] != null && row["Userno"].ToString() != "")
                //{
                //    model.ModelUser.Userno =int.Parse( row["Userno"].ToString());
                //}
                //if (row["UserLevel"] != null && row["UserLevel"].ToString() != "")
                //{
                //    model.ModelUser.Userno = int.Parse(row["UserLevel"].ToString());
                //}
            }
            return model;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,Mchbillno,Pice,SenderID,ReceiverID,SendType,Status,Addtime,SendTime,Noncestr,PaySign,IsNotSend,OID ");
			strSql.Append(" FROM SendBag ");
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
			strSql.Append(" ID,Mchbillno,Pice,SenderID,ReceiverID,SendType,Status,Addtime,SendTime,Noncestr,PaySign,IsNotSend,OID ");
			strSql.Append(" FROM SendBag ");
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
			strSql.Append("select count(1) FROM SendBag ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from SendBag T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获得关健字查询分页数据(搜索用到)
        /// </summary>
        public DataSet GetSearch(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Mchbillno,Pice,SenderID,ReceiverID,SendType,Status,Addtime,SendTime,Noncestr,PaySign,IsNotSend,OID,username,Userno,UserLevel,avatar from  view_SendBagByR");
            strSql.Append(" where ID>0");
            //if (!string.IsNullOrEmpty(channel_name))
            //{
            //    strSql.Append(" and channel_id=(select id from  channel where [name]='" + channel_name + "')");
            //}
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(BrnMall.Core.Common.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(BrnMall.Core.Common.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        /// <summary>
        /// 获得关健字查询分页数据(搜索用到)
        /// </summary>
        public DataSet GetSearchByS(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Mchbillno,Pice,SenderID,ReceiverID,SendType,Status,Addtime,SendTime,Noncestr,PaySign,IsNotSend,OID,username,Userno,UserLevel,avatar from  view_SendBagByS");
            strSql.Append(" where ID>0");
           
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(BrnMall.Core.Common.PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(BrnMall.Core.Common.PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        /// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordSum(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(Pice) FROM SendBag ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			parameters[0].Value = "SendBag";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

