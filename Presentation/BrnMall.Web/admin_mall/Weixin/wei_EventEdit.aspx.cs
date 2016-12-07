using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace qiaojiaren.Manger
{
    public partial class wei_EventEdit : System.Web.UI.Page
    {

        BrnMall.Core.BLL.YX_weiXinMenus menuBll = new BrnMall.Core.BLL.YX_weiXinMenus();//微信菜单
        BrnMall.Core.Model.YX_weiXinMenus menuMol = new BrnMall.Core.Model.YX_weiXinMenus();

        public BrnMall.Core.Model.YX_weiXinMenus mol = new BrnMall.Core.Model.YX_weiXinMenus();
        BrnMall.Core.BLL.YX_weiXinMenus bll = new BrnMall.Core.BLL.YX_weiXinMenus();
        public string eventType = "";//菜单事件的类型  为 text news等

        BrnMall.Core.BLL.YX_text textBll = new BrnMall.Core.BLL.YX_text();//发送文本事件
        public BrnMall.Core.Model.YX_text textMol = new BrnMall.Core.Model.YX_text();

        BrnMall.Core.BLL.YX_news newsBll = new BrnMall.Core.BLL.YX_news();//发送图文
        public BrnMall.Core.Model.YX_news newsMol = new BrnMall.Core.Model.YX_news();

        public DataTable dt_text = new DataTable();
        public DataTable dt_News = new DataTable(); BrnMall.Core.DBUtility.T_SQL imp = new BrnMall.Core.DBUtility.T_SQL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                (Master.FindControl("childmenu") as Label).Text = "微事件编辑";
                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    mol = bll.GetModel(Convert.ToInt32(id));
                    eventType = mol.flat1.ToString();

                    switch (eventType)
                    {
                        case "1": dt_text = textBll.GetList(" EventId=" + id + " and EventCate='menu'").Tables[0]; break;//文本
                        case "2": dt_News = newsBll.GetList(" EventId=" + id + " and EventCate='menu'").Tables[0]; break;//图文
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

            if (Request.Form["action"] == "text")
            {
                AddText();
            }
            if (Request.Form["action"] == "news")
            {
                AddNews();
            }
           
        }

        /// <summary>
        /// 添加文本事件
        /// </summary>
        protected void AddText()
        {
            DelEvent();//先删除原来的事件  再添加新事件
            //
            string EventId = Request.Form["EventId"];
            if (!string.IsNullOrEmpty(EventId))
            {
                menuMol = menuBll.GetModel(Convert.ToInt32(EventId));
                menuMol.flat1 = 1;//代表发送文本
                menuBll.Update(menuMol);//更新数据
                textMol.EventId = Convert.ToInt32(EventId);
                textMol.msgContent = CommonMethod.CheckParamThrow(Request.Form["msgContent"]);
                textMol.EventCate = "menu";//事件类型 菜单
                if (textBll.Add(textMol) != 0)
                {
                    CommonMethod.Alert("恭喜编辑菜单成功！", "wei_MenusConfig.aspx?menuFid=" + Request.Form["fid"]);
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
            DelEvent();//先删除原来的事件  再添加新事件
            //
            string EventId = Request.Form["EventId"];
            if (!string.IsNullOrEmpty(EventId))
            {
                menuMol = menuBll.GetModel(Convert.ToInt32(EventId));
                menuMol.flat1 = 2;//代表发送图文
                menuBll.Update(menuMol);//更新数据
                newsMol.EventId = Convert.ToInt32(EventId);
                newsMol.newsTitle = CommonMethod.CheckParamThrow(Request.Form["newsTitle"]);
                newsMol.newsDescription = CommonMethod.CheckParamThrow(Request.Form["newsDescription"]);
                newsMol.newsPicUrl = CommonMethod.CheckParamThrow(Request.Form["newsPicUrl"]);
                newsMol.newsUrl = CommonMethod.CheckParamThrow(Request.Form["newsUrl"]); newsMol.EventCate = "menu";//事件类型 菜单
                if (newsBll.Add(newsMol) != 0)
                {
                    CommonMethod.Alert("恭喜编辑菜单成功！", "wei_MenusConfig.aspx?menuFid=" + Request.Form["fid"]);
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
            string eventType = Request.Form["hideventType"];
            string sql = "";
            switch (eventType)
            {
                case "1": sql = "delete from YX_text where EventId=" + EventId + " and EventCate='menu'"; break;//文本
                case "2": sql = "delete from YX_news where EventId=" + EventId + " and EventCate='menu'"; break;//图文
                case "3": sql = "delete from YX_image where EventId=" + EventId + " and EventCate='menu'"; break;
                case "4": sql = "delete from YX_voice where EventId=" + EventId + " and EventCate='menu'"; break;
                case "5": sql = "delete from YX_video where EventId=" + EventId + " and EventCate='menu'"; break;
                case "6": sql = "delete from YX_music where EventId=" + EventId + " and EventCate='menu'"; break;
            }
            imp.GetSqlCount(CommandType.Text, sql);

        }
        #endregion
    }
}