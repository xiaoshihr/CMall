using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BrnShop.Core;
namespace qiaojiaren.Manger
{
    public partial class wei_Event : basePage
    {
        BrnShop.Core.BLL.YX_weiXinMenus menuBll = new BrnShop.Core.BLL.YX_weiXinMenus();//微信菜单
        BrnShop.Core.Model.YX_weiXinMenus menuMol = new BrnShop.Core.Model.YX_weiXinMenus();

        BrnShop.Core.BLL.YX_text textBll = new BrnShop.Core.BLL.YX_text();//发送文本事件
        BrnShop.Core.Model.YX_text textMol = new BrnShop.Core.Model.YX_text();

        BrnShop.Core.BLL.YX_news newsBll = new BrnShop.Core.BLL.YX_news();//发送图文
        BrnShop.Core.Model.YX_news newsMol = new BrnShop.Core.Model.YX_news();
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("childmenu") as Label).Text = "微菜单配置";
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
                    CommonMethod.Alert("添加文本事件成功！", "wei_MenusConfig.aspx?menuFid=" + Request.Form["fid"]);
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
                newsMol.newsUrl = CommonMethod.CheckParamThrow(Request.Form["newsUrl"]);
                newsMol.EventCate = "menu";//事件类型菜单
                if (newsBll.Add(newsMol) != 0)
                {
                    CommonMethod.Alert("添加图文事件成功！", "wei_MenusConfig.aspx?menuFid="+Request.Form["fid"]);
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
    }
}