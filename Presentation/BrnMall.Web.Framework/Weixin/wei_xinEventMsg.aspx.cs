using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace qiaojiaren.Manger
{
    public partial class wei_xinEventMsg : System.Web.UI.Page
    {
        public BrnShop.Core.Model.YX_Event eventMol = new BrnShop.Core.Model.YX_Event();
        BrnShop.Core.BLL.YX_Event eventBll = new BrnShop.Core.BLL.YX_Event();
        BrnShop.Core.DBUtility.T_SQL imp = new BrnShop.Core.DBUtility.T_SQL();

        BrnShop.Core.BLL.YX_text textBll = new BrnShop.Core.BLL.YX_text();//发送文本事件
        public BrnShop.Core.Model.YX_text textMol = new BrnShop.Core.Model.YX_text();

        BrnShop.Core.BLL.YX_news newsBll = new BrnShop.Core.BLL.YX_news();//发送图文
        public BrnShop.Core.Model.YX_news newsMol = new BrnShop.Core.Model.YX_news();


        public DataTable dt_text = new DataTable();
        public DataTable dt_News = new DataTable();

        public string selCon = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("childmenu") as Label).Text = "配置微事件";
            if (Request.Form["action"] == "text")
            {
                AddText(); return;
            }
            if (Request.Form["action"] == "news")
            {
                AddNews(); return;
            }
            if (!Page.IsPostBack)
            {
                loadMenus(Request.QueryString["msgtype"]);
                string Id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(Id))
                {
                    eventMol = eventBll.GetModel(Convert.ToInt32(Id));
                    string eventType = eventMol.msgType.ToString();
                    switch (eventType)
                    {
                        case "1": dt_text = textBll.GetList(" EventId=" + Id + " and EventCate='" + eventMol.EventName + "'").Tables[0]; break;//文本
                        case "2": dt_News = newsBll.GetList(" EventId=" + Id + " and EventCate='" + eventMol.EventName + "'").Tables[0]; break;//图文
                    }
                    if (dt_text.Rows.Count != 0)
                    {
                        textMol = textBll.GetModel(Convert.ToInt32(dt_text.Rows[0]["Id"]));
                    }
                    if (dt_News.Rows.Count != 0)
                    {
                        newsMol = newsBll.GetModel(Convert.ToInt32(dt_News.Rows[0]["Id"]));
                    }
                  
                }
            }
        }

        /// <summary>
        /// 加载下拉框
        /// </summary>
        /// <param name="msgtype"></param>
        private void loadMenus(string msgtype)
        {
            if (!string.IsNullOrEmpty(msgtype))
            {
                if (msgtype == "1")
                {
                    selCon = @"<select id='newsType' name='newsType' onchange='coik(this)'>
            <option value='1'>文本消息</option>
            <option value='2'>图文消息</option>
        </select>";
                }
                else
                {
                    selCon = @"<select id='newsType' name='newsType' onchange='coik(this)'>
            <option value='1'>文本消息</option>
            <option value='2' selected='selected'>图文消息</option>
        </select>";
                }
            }
        }

        /// <summary>
        /// 添加文本事件
        /// </summary>
        protected void AddText()
        {
            textMol.EventCate = Request.Form["EventName"];//事件类型  菜单  关注等
            string EventId = Request.Form["EventId"];
            DelEvent();//先删除 后添加
            if (!string.IsNullOrEmpty(EventId))
            {
                eventMol = eventBll.GetModel(Convert.ToInt32(EventId));
                eventMol.msgType = 1;
                eventMol.flat1 = 1;//是否存在相应的消息事件 1代表已拥有
                eventBll.Update(eventMol);//更新事假的消息类型
                textMol.EventId = Convert.ToInt32(EventId);
                textMol.msgContent = CommonMethod.CheckParamThrow(Request.Form["msgContent"]);
                if (textBll.Add(textMol) != 0)
                {
                    CommonMethod.Alert("操作成功！", "wei_MsgConfig.aspx?menuFid=" + Request.Form["fid"]);
                }
                else
                {
                    CommonMethod.Alert("系统正忙，请稍后重试！");
                }
            }
            else
            {
                CommonMethod.Alert("系统正忙，请稍后重试！");
            }
        }

        /// <summary>
        /// 添加图文事件
        /// </summary>
        protected void AddNews()
        {
            newsMol.EventCate = Request.Form["EventName"];//事件类型  菜单  关注等
            string EventId = Request.Form["EventId"];
            DelEvent();//先删除 后添加
            if (!string.IsNullOrEmpty(EventId))
            {
                eventMol = eventBll.GetModel(Convert.ToInt32(EventId));
                eventMol.msgType = 2;
                eventMol.flat1 = 1;//是否存在相应的消息事件 1代表已拥有
                eventBll.Update(eventMol);//更新事假的消息类型

                newsMol.EventId = Convert.ToInt32(EventId);
                newsMol.newsTitle = CommonMethod.CheckParamThrow(Request.Form["newsTitle"]);
                newsMol.newsDescription = CommonMethod.CheckParamThrow(Request.Form["newsDescription"]);
                newsMol.newsPicUrl = CommonMethod.CheckParamThrow(Request.Form["newsPicUrl"]);
                newsMol.newsUrl = CommonMethod.CheckParamThrow(Request.Form["newsUrl"]);
                if (newsBll.Add(newsMol) != 0)
                {
                    CommonMethod.Alert("操作成功！", "wei_MsgConfig.aspx?menuFid=" + Request.Form["fid"]);
                }
                else
                {
                    CommonMethod.Alert("系统正忙，请稍后重试！");
                }
            }
            else
            {
                CommonMethod.Alert("系统正忙，请稍后重试！");
            }
        }

        #region 根据EventId和eventType删除相应的事件
        /// <summary>
        /// 根据EventId和eventType删除相应的事件
        /// </summary>
        protected void DelEvent()
        {
            string EventId = Request.Form["EventId"];
            string eventType = Request.Form["msgtype"];
            string EventName = Request.Form["EventName"];
            string sql = "";//SQL为空值的时候  会发生异常
            switch (eventType)
            {
                case "1": sql = "delete from YX_text where EventId=" + EventId + " and EventCate='" + EventName + "'"; break;//文本
                case "2": sql = "delete from YX_news where EventId=" + EventId + " and EventCate='" + EventName + "'"; break;//图文
                case "3": sql = "delete from YX_image where EventId=" + EventId + " and EventCate='" + EventName + "'"; break;
                case "4": sql = "delete from YX_voice where EventId=" + EventId + " and EventCate='" + EventName + "'"; break;
                case "5": sql = "delete from YX_video where EventId=" + EventId + " and EventCate='" + EventName + "'"; break;
                case "6": sql = "delete from YX_music where EventId=" + EventId + " and EventCate='" + EventName + "'"; break;
            }
            imp.GetSqlCount(CommandType.Text, sql);

        }
        #endregion
    }
}