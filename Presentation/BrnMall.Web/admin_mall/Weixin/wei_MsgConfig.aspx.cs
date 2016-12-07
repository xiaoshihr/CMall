using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace qiaojiaren.Manger
{
    public partial class wei_MsgConfig : basePage
    {
        public DataTable dt = new DataTable();
        BrnMall.Core.DBUtility.T_SQL imp = new BrnMall.Core.DBUtility.T_SQL();
        BrnMall.Core.Model.YX_Event mol = new BrnMall.Core.Model.YX_Event();
        BrnMall.Core.BLL.YX_Event bll = new BrnMall.Core.BLL.YX_Event();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] == "del")
            {
                weixinEventDel(Request.QueryString["id"]); return;
            }
            BindInfo("");
        }

        #region 绑定信息  void BindInfo(string where)
        /// <summary>
        /// 绑定信息
        /// </summary>
        /// <param name="where"></param>
        protected void BindInfo(string where)
        {
            int totalPage;
            int recordCount;
            int pageIndex = AspNetPager1.CurrentPageIndex;
            int pageSize = AspNetPager1.PageSize;//页容量 
            if (string.IsNullOrEmpty(where))
            {
                where = " 1=1";
            }
            else
            {
                where += " and 1=1";
            }
            dt = BrnMall.Core.DBUtility.ProcUtility.GetPageProBll("YX_Event", "*", " Id desc", where, pageSize, pageIndex, out totalPage, out recordCount);
            this.AspNetPager1.RecordCount = recordCount;
            this.AspNetPager1.RecordCount = recordCount;
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
        }
        #endregion

        #region//用于数据分页时，切换页面 void AspNetPager1_PageChanged
        /// <summary>
        /// 用于数据分页时，切换页面 void AspNetPager1_PageChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindInfo("");
        }
        #endregion

        #region 获取微信/消息事件的汉语名称
        /// <summary>
        /// 获取微信事件的汉语名称
        /// </summary>
        /// <param name="EventName">xx</param>
        /// <returns></returns>
        public string GetEventType(string EventName)
        {
            string vle = "";
            switch (EventName)
            {
                case "subscribe": vle = "关注事件"; break;
                case "SCAN": vle = "已关注事件"; break;
                case "keyword": vle = "关键词事件"; break;
                case "image": vle = "图片事件"; break;
                case "voice": vle = "语音事件"; break;
                case "video": vle = "视频事件"; break;
                case "shortvideo": vle = "小视频事件"; break;
                case "location": vle = "地理位置事件"; break;
                case "link": vle = "链接事件"; break;
                case "0": vle = "未定义"; break;
            }
            return vle;
        }

        /// <summary>
        /// 获取微信消息的汉语名称
        /// </summary>
        /// <param name="msgtype"></param>
        /// <returns></returns>
        public string GetMsgType(string msgtype)
        {
            string vle = "";
            switch (msgtype)
            {
                case "1": vle = "发送文本消息"; break;
                case "2": vle = "发送图文消息"; break;
                case "3": vle = "发送图片消息"; break;
                case "4": vle = "发送语音消息"; break;
                case "5": vle = "发送视频消息"; break;
                case "6": vle = "发送音乐消息"; break;
                case "0": vle = "未定义"; break;
            }
            return vle;
        }
        #endregion

        #region 删除事件 并删除对应的消息
        /// <summary>
        /// 删除事件 并删除对应的消息
        /// </summary>
        protected void weixinEventDel(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                mol = bll.GetModel(Convert.ToInt32(id));
                string enentname = mol.EventName;
                int msgType = Convert.ToInt32(mol.msgType);
                string sql = "";
                switch (msgType)//删除对应的消息
                {
                    case 1: sql = "delete from YX_text where EventId=" + id + " and EventCate='" + enentname + "'"; break;
                    case 2: sql = "delete from YX_news where EventId=" + id + " and EventCate='" + enentname + "'"; break;
                    case 3: sql = "delete from YX_image where EventId=" + id + " and EventCate='" + enentname + "'"; break;
                    case 4: sql = "delete from YX_voice where EventId=" + id + " and EventCate='" + enentname + "'"; break;
                    case 5: sql = "delete from YX_vedio where EventId=" + id + " and EventCate='" + enentname + "'"; break;
                    case 6: sql = "delete from YX_music where EventId=" + id + " and EventCate='" + enentname + "'"; break;
                }
                imp.GetSqlCount(CommandType.Text, sql);
                if (bll.Delete(Convert.ToInt32(id)))
                {
                    Response.Write("OK");
                }
                else
                {
                    Response.Write("NO");
                }
            }
            else
            {
                Response.Write("NO");
            }
            Response.End();
        }
        #endregion

        #region 获取事件类型
        /// <summary>
        /// 获取菜单类型
        /// </summary>
        /// <param name="WX_MenuType">菜单类型数值</param>
        /// <returns></returns>
        public object GetMsgType(object flat)
        {
            int type = Convert.ToInt32(flat);
            if (Convert.ToInt32((MsgType)(type - 1)) == -1)
            {
                return "暂未配置";
            }
            else
            {
                return (MsgType)(type - 1);
            }
        }
        #endregion
    }
}