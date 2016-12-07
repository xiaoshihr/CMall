using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BrnMall.Core;
namespace BrnMall.Web.Mobile.Services
{
    public class WeixinHelp
    {
        BrnMall.Core.Model.YX_weiXinMenus menusMol = new BrnMall.Core.Model.YX_weiXinMenus();//微信菜单
        BrnMall.Core.BLL.YX_weiXinMenus menusBll = new BrnMall.Core.BLL.YX_weiXinMenus();
        BrnMall.Core.Model.YX_Event entMol = new BrnMall.Core.Model.YX_Event();
        BrnMall.Core.BLL.YX_Event entBll = new BrnMall.Core.BLL.YX_Event();//微信事件  关注  关键词  等
        BrnMall.Core.DBUtility.T_SQL imp = new BrnMall.Core.DBUtility.T_SQL();
        /// <summary>
        /// 根据OpenId 获取用户的Id
        /// </summary>
        /// <param name="openid">用户的OpenId</param>
        /// <returns></returns>
        public int GetIdByOpenId(string openid)
        {
            int returnValue = 0;
            try
            {
                string sql = "select Id from YX_weiUser where openid='" + openid + "'";
                returnValue = imp.GetSqlOne(CommandType.Text, sql) == null ? 0 : Convert.ToInt32(imp.GetSqlOne(CommandType.Text, sql));
            }
            catch (Exception ex)
            {
                returnValue = 0;
               BrnMall.Core.Common.LogHelper.WriteLog("数据表YX_weiUser根据OpenId 获取用户的Id时发生异常。", ex);
                throw ex;
            }
            return returnValue;
        }

        public string GetCode(int codeLength)
        {

            string so = "1,2,3,4,5,6,7,8,9,0";
            string[] strArr = so.Split(',');
            string code = "";
            Random rand = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                code += strArr[rand.Next(0, strArr.Length)];
            }
            return code;
        }

        /// <summary>
        /// 验证是否重复，递归验证
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private string PayNumsXX(string nums)
        {
            string s = "";
            //DataTable dt = ""; //WeiUserBll.GetList(" [remark1]='" + nums + "'").Tables[0];
            //if (dt.Rows.Count != 0)
            //{
            //    s = PayNumsXX(GetCode(8));
            //}
            //else
            //{
            //    s = nums;
            //}
            return s;
        }
        #region 动态自定义菜单
        /// <summary>
        /// 获取一级菜单数目
        /// </summary>
        /// <returns></returns>
        public DataTable GetMainMenus()
        {
            DataTable dt = menusBll.GetList(" WX_Fid=0 order by flat2 desc").Tables[0];
            return dt;
        }

        public DataTable GetChildMenus()
        {
            DataTable dt = menusBll.GetList(" WX_Fid!=0  order by flat2 desc").Tables[0];
            return dt;
        }

        public DataTable GetChildMenus(object Fid)
        {
            DataTable dt = menusBll.GetList(" WX_Fid=" + Fid + "").Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取单击类型的按钮
        /// </summary>
        /// <returns></returns>
        public DataTable GetClickMenus()
        {
            DataTable dt = menusBll.GetList(" WX_MenuType=1 order by flat2").Tables[0];
            return dt;
        }

        /// <summary>
        /// 获取单击类型按钮对应的消息类型 即：发送文本 发送图文 发送图片 音乐等！
        /// </summary>
        /// <param name="eventId">菜单Id</param>
        /// <param name="tableName">表名  发送文本表：YX_text  发送图文表：YX_news等</param>
        /// <param name="EventCate">是菜单的发送消息 还是关注、关注等事件发送的消息，菜单：menu 关注：subscribe 等</param>
        /// <returns></returns>
        public DataTable GetClickMenusMsg(string tableName, string eventId, string EventCate = "menu")
        {
            string sql = "select * from " + tableName + " where EventId=" + eventId + " and EventCate='" + EventCate + "'";
            DataTable dt = imp.GetSqlDataSet(CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt;
            }

        }
        #endregion

        #region 获取自定义的事件：关注 带参数二维码已关注事件 文本关键词事件等
        /// <summary>
        /// 获取自定义的事件：关注 带参数二维码已关注事件 文本关键词事件等
        /// </summary>
        /// <param name="eventType">subscribe：关注 keyword：关键词</param>
        /// <returns></returns>
        public DataTable getEvnet(string eventType)
        {
            DataTable dt = entBll.GetList(" EventName='" + eventType + "'").Tables[0];
            if (dt.Rows.Count != 0)
            {
                return dt;
            }
            else
            {
                return null;
            }

        }
        #endregion
    }
}
