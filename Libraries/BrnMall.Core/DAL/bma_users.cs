using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BrnMall.Core.DBUtility;//Please add references
namespace BrnMall.Core.DAL
{
    /// <summary>
    /// 数据访问类:bma_users
    /// </summary>
    public partial class bma_users
    {
        public bma_users()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int uid, string username, string email, string mobile, int userrid, int storeid, int mallagid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bma_users");
            strSql.Append(" where uid=@uid and username=@username and email=@email and mobile=@mobile and userrid=@userrid and storeid=@storeid and mallagid=@mallagid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@uid", SqlDbType.Int,4),
                    new SqlParameter("@username", SqlDbType.NChar,20),
                    new SqlParameter("@email", SqlDbType.Char,50),
                    new SqlParameter("@mobile", SqlDbType.Char,15),
                    new SqlParameter("@userrid", SqlDbType.SmallInt,2),
                    new SqlParameter("@storeid", SqlDbType.Int,4),
                    new SqlParameter("@mallagid", SqlDbType.SmallInt,2)         };
            parameters[0].Value = uid;
            parameters[1].Value = username;
            parameters[2].Value = email;
            parameters[3].Value = mobile;
            parameters[4].Value = userrid;
            parameters[5].Value = storeid;
            parameters[6].Value = mallagid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BrnMall.Core.Model.bma_users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into bma_users(");
            strSql.Append("username,email,mobile,password,userrid,storeid,mallagid,nickname,avatar,paycredits,rankcredits,verifyemail,verifymobile,liftbantime,salt,Openid,Pid,Userno,UserLevel,Layernum,IsReal)");
            strSql.Append(" values (");
            strSql.Append("@username,@email,@mobile,@password,@userrid,@storeid,@mallagid,@nickname,@avatar,@paycredits,@rankcredits,@verifyemail,@verifymobile,@liftbantime,@salt,@Openid,@Pid,@Userno,@UserLevel,@Layernum,@IsReal)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@username", SqlDbType.NChar,20),
                    new SqlParameter("@email", SqlDbType.Char,50),
                    new SqlParameter("@mobile", SqlDbType.Char,15),
                    new SqlParameter("@password", SqlDbType.Char,32),
                    new SqlParameter("@userrid", SqlDbType.SmallInt,2),
                    new SqlParameter("@storeid", SqlDbType.Int,4),
                    new SqlParameter("@mallagid", SqlDbType.SmallInt,2),
                    new SqlParameter("@nickname", SqlDbType.NChar,20),
                    new SqlParameter("@avatar", SqlDbType.Char,40),
                    new SqlParameter("@paycredits", SqlDbType.Int,4),
                    new SqlParameter("@rankcredits", SqlDbType.Int,4),
                    new SqlParameter("@verifyemail", SqlDbType.TinyInt,1),
                    new SqlParameter("@verifymobile", SqlDbType.TinyInt,1),
                    new SqlParameter("@liftbantime", SqlDbType.DateTime),
                    new SqlParameter("@salt", SqlDbType.NChar,6),
                    new SqlParameter("@Openid", SqlDbType.VarChar,150),
                    new SqlParameter("@Pid", SqlDbType.Int,4),
                    new SqlParameter("@Userno", SqlDbType.Int,4),
                    new SqlParameter("@UserLevel", SqlDbType.Int,4),
                    new SqlParameter("@Layernum", SqlDbType.Int,4),
                    new SqlParameter("@IsReal", SqlDbType.Int,4)};
            parameters[0].Value = model.username;
            parameters[1].Value = model.email;
            parameters[2].Value = model.mobile;
            parameters[3].Value = model.password;
            parameters[4].Value = model.userrid;
            parameters[5].Value = model.storeid;
            parameters[6].Value = model.mallagid;
            parameters[7].Value = model.nickname;
            parameters[8].Value = model.avatar;
            parameters[9].Value = model.paycredits;
            parameters[10].Value = model.rankcredits;
            parameters[11].Value = model.verifyemail;
            parameters[12].Value = model.verifymobile;
            parameters[13].Value = model.liftbantime;
            parameters[14].Value = model.salt;
            parameters[15].Value = model.Openid;
            parameters[16].Value = model.Pid;
            parameters[17].Value = model.Userno;
            parameters[18].Value = model.UserLevel;
            parameters[19].Value = model.Layernum;
            parameters[20].Value = model.IsReal;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(BrnMall.Core.Model.bma_users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update bma_users set ");
            strSql.Append("password=@password,");
            strSql.Append("nickname=@nickname,");
            strSql.Append("avatar=@avatar,");
            strSql.Append("paycredits=@paycredits,");
            strSql.Append("rankcredits=@rankcredits,");
            strSql.Append("verifyemail=@verifyemail,");
            strSql.Append("verifymobile=@verifymobile,");
            strSql.Append("liftbantime=@liftbantime,");
            strSql.Append("salt=@salt,");
            strSql.Append("Openid=@Openid,");
            strSql.Append("Pid=@Pid,");
            strSql.Append("Userno=@Userno,");
            strSql.Append("UserLevel=@UserLevel,");
            strSql.Append("Layernum=@Layernum,");
            strSql.Append("IsReal=@IsReal");
            strSql.Append(" where uid=@uid");
            SqlParameter[] parameters = {
                    new SqlParameter("@password", SqlDbType.Char,32),
                    new SqlParameter("@nickname", SqlDbType.NChar,20),
                    new SqlParameter("@avatar", SqlDbType.Char,40),
                    new SqlParameter("@paycredits", SqlDbType.Int,4),
                    new SqlParameter("@rankcredits", SqlDbType.Int,4),
                    new SqlParameter("@verifyemail", SqlDbType.TinyInt,1),
                    new SqlParameter("@verifymobile", SqlDbType.TinyInt,1),
                    new SqlParameter("@liftbantime", SqlDbType.DateTime),
                    new SqlParameter("@salt", SqlDbType.NChar,6),
                    new SqlParameter("@Openid", SqlDbType.VarChar,150),
                    new SqlParameter("@Pid", SqlDbType.Int,4),
                    new SqlParameter("@Userno", SqlDbType.Int,4),
                    new SqlParameter("@UserLevel", SqlDbType.Int,4),
                    new SqlParameter("@uid", SqlDbType.Int,4),
                    new SqlParameter("@username", SqlDbType.NChar,20),
                    new SqlParameter("@email", SqlDbType.Char,50),
                    new SqlParameter("@mobile", SqlDbType.Char,15),
                    new SqlParameter("@userrid", SqlDbType.SmallInt,2),
                    new SqlParameter("@storeid", SqlDbType.Int,4),
                    new SqlParameter("@mallagid", SqlDbType.SmallInt,2),
                    new SqlParameter("@Layernum", SqlDbType.Int,4),
                    new SqlParameter("@IsReal", SqlDbType.Int,4)};
            parameters[0].Value = model.password;
            parameters[1].Value = model.nickname;
            parameters[2].Value = model.avatar;
            parameters[3].Value = model.paycredits;
            parameters[4].Value = model.rankcredits;
            parameters[5].Value = model.verifyemail;
            parameters[6].Value = model.verifymobile;
            parameters[7].Value = model.liftbantime;
            parameters[8].Value = model.salt;
            parameters[9].Value = model.Openid;
            parameters[10].Value = model.Pid;
            parameters[11].Value = model.Userno;
            parameters[12].Value = model.UserLevel;
            parameters[13].Value = model.uid;
            parameters[14].Value = model.username;
            parameters[15].Value = model.email;
            parameters[16].Value = model.mobile;
            parameters[17].Value = model.userrid;
            parameters[18].Value = model.storeid;
            parameters[19].Value = model.mallagid;
            parameters[20].Value = model.Layernum;
            parameters[21].Value = model.IsReal;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateIsReal(int uid)
        {
            string strsql = " update bma_users set IsReal=1 where uid="+uid;
            int rows = DbHelperSQL.ExecuteSql(strsql);
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
        public bool Delete(int uid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from bma_users ");
            strSql.Append(" where uid=@uid");
            SqlParameter[] parameters = {
                    new SqlParameter("@uid", SqlDbType.Int,4)
            };
            parameters[0].Value = uid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(int uid, string username, string email, string mobile, int userrid, int storeid, int mallagid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from bma_users ");
            strSql.Append(" where uid=@uid and username=@username and email=@email and mobile=@mobile and userrid=@userrid and storeid=@storeid and mallagid=@mallagid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@uid", SqlDbType.Int,4),
                    new SqlParameter("@username", SqlDbType.NChar,20),
                    new SqlParameter("@email", SqlDbType.Char,50),
                    new SqlParameter("@mobile", SqlDbType.Char,15),
                    new SqlParameter("@userrid", SqlDbType.SmallInt,2),
                    new SqlParameter("@storeid", SqlDbType.Int,4),
                    new SqlParameter("@mallagid", SqlDbType.SmallInt,2)         };
            parameters[0].Value = uid;
            parameters[1].Value = username;
            parameters[2].Value = email;
            parameters[3].Value = mobile;
            parameters[4].Value = userrid;
            parameters[5].Value = storeid;
            parameters[6].Value = mallagid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string uidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from bma_users ");
            strSql.Append(" where uid in (" + uidlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public BrnMall.Core.Model.bma_users GetModel(int uid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 uid,username,email,mobile,password,userrid,storeid,mallagid,nickname,avatar,paycredits,rankcredits,verifyemail,verifymobile,liftbantime,salt,Openid,Pid,Userno,UserLevel,Layernum,IsReal from bma_users ");
            strSql.Append(" where uid=@uid");
            SqlParameter[] parameters = {
                    new SqlParameter("@uid", SqlDbType.Int,4)
            };
            parameters[0].Value = uid;

            BrnMall.Core.Model.bma_users model = new BrnMall.Core.Model.bma_users();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public BrnMall.Core.Model.bma_users DataRowToModel(DataRow row)
        {
            BrnMall.Core.Model.bma_users model = new BrnMall.Core.Model.bma_users();
            if (row != null)
            {
                if (row["uid"] != null && row["uid"].ToString() != "")
                {
                    model.uid = int.Parse(row["uid"].ToString());
                }
                if (row["username"] != null)
                {
                    model.username = row["username"].ToString();
                }
                if (row["email"] != null)
                {
                    model.email = row["email"].ToString();
                }
                if (row["mobile"] != null)
                {
                    model.mobile = row["mobile"].ToString();
                }
                if (row["password"] != null)
                {
                    model.password = row["password"].ToString();
                }
                if (row["userrid"] != null && row["userrid"].ToString() != "")
                {
                    model.userrid = int.Parse(row["userrid"].ToString());
                }
                if (row["storeid"] != null && row["storeid"].ToString() != "")
                {
                    model.storeid = int.Parse(row["storeid"].ToString());
                }
                if (row["mallagid"] != null && row["mallagid"].ToString() != "")
                {
                    model.mallagid = int.Parse(row["mallagid"].ToString());
                }
                if (row["nickname"] != null)
                {
                    model.nickname = row["nickname"].ToString();
                }
                if (row["avatar"] != null)
                {
                    model.avatar = row["avatar"].ToString();
                }
                if (row["paycredits"] != null && row["paycredits"].ToString() != "")
                {
                    model.paycredits = int.Parse(row["paycredits"].ToString());
                }
                if (row["rankcredits"] != null && row["rankcredits"].ToString() != "")
                {
                    model.rankcredits = int.Parse(row["rankcredits"].ToString());
                }
                if (row["verifyemail"] != null && row["verifyemail"].ToString() != "")
                {
                    model.verifyemail = int.Parse(row["verifyemail"].ToString());
                }
                if (row["verifymobile"] != null && row["verifymobile"].ToString() != "")
                {
                    model.verifymobile = int.Parse(row["verifymobile"].ToString());
                }
                if (row["liftbantime"] != null && row["liftbantime"].ToString() != "")
                {
                    model.liftbantime = DateTime.Parse(row["liftbantime"].ToString());
                }
                if (row["salt"] != null)
                {
                    model.salt = row["salt"].ToString();
                }
                if (row["Openid"] != null)
                {
                    model.Openid = row["Openid"].ToString();
                }
                if (row["Pid"] != null && row["Pid"].ToString() != "")
                {
                    model.Pid = int.Parse(row["Pid"].ToString());
                }
                if (row["Userno"] != null && row["Userno"].ToString() != "")
                {
                    model.Userno = int.Parse(row["Userno"].ToString());
                }
                if (row["UserLevel"] != null && row["UserLevel"].ToString() != "")
                {
                    model.UserLevel = int.Parse(row["UserLevel"].ToString());
                }
                if (row["Layernum"] != null && row["Layernum"].ToString() != "")
                {
                    model.Layernum = int.Parse(row["Layernum"].ToString());
                }
                if (row["IsReal"] != null && row["IsReal"].ToString() != "")
                {
                    model.IsReal = int.Parse(row["IsReal"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select uid,username,email,mobile,password,userrid,storeid,mallagid,nickname,avatar,paycredits,rankcredits,verifyemail,verifymobile,liftbantime,salt,Openid,Pid,Userno,UserLevel,Layernum,IsReal ");
            strSql.Append(" FROM bma_users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" uid,username,email,mobile,password,userrid,storeid,mallagid,nickname,avatar,paycredits,rankcredits,verifyemail,verifymobile,liftbantime,salt,Openid,Pid,Userno,UserLevel,Layernum,IsReal ");
            strSql.Append(" FROM bma_users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM bma_users ");
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
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.uid desc");
            }
            strSql.Append(")AS Row, T.*  from bma_users T ");
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
			parameters[0].Value = "bma_users";
			parameters[1].Value = "uid";
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

