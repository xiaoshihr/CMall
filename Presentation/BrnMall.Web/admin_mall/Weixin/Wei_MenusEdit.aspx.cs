using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace qiaojiaren.Manger
{
    public partial class Wei_MenusEdit : basePage
    {
        BrnMall.Core.BLL.YX_weiXinMenus menuBll = new BrnMall.Core.BLL.YX_weiXinMenus();//微信菜单
        public BrnMall.Core.Model.YX_weiXinMenus menuMol = new BrnMall.Core.Model.YX_weiXinMenus();

        public string MenuType = "";//菜单类型
        public string fatherMenu = "";//父菜单
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("childmenu") as Label).Text = "微菜单编辑";
            if (!Page.IsPostBack)
            {
                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    menuMol = menuBll.GetModel(Convert.ToInt32(id));
                    if (menuMol.WX_MenuType == "0")
                    {
                        MenuType = "主菜单";
                    }
                    else
                    {
                        int type = Convert.ToInt32(menuMol.WX_MenuType);
                        MenuType = ((ButtonType)(type - 1)).ToString();
                    }

                    int Fid = Convert.ToInt32(menuMol.WX_Fid);
                    if (Fid != 0)
                    {
                        BrnMall.Core.Model.YX_weiXinMenus menuMol2 = new BrnMall.Core.Model.YX_weiXinMenus();
                        menuMol2 = menuBll.GetModel(Fid);
                        fatherMenu = menuMol2.WX_menuName;
                    }
                }
            }

            //编辑微菜单
            if (Request.Form["action"] == "action")
            {
                WeiMenusEdits();
            }
        }

        /// <summary>
        /// 编辑微菜单
        /// </summary>
        protected void WeiMenusEdits()
        {
            string id = Request.Form["menuId"];
            if (!string.IsNullOrEmpty(id))
            {
                menuMol = menuBll.GetModel(Convert.ToInt32(id));
                menuMol.WX_menuName = Request.Form["WX_menuName"];
                menuMol.WX_MenusKey_URL = Request.Form["WX_MenusKey_URL"];
                menuMol.flat2 = Convert.ToInt32(Request.Form["flat2"]);
                if (menuBll.Update(menuMol))
                {
                    CommonMethod.Alert("编辑微菜单成功！", "wei_MenusList.aspx?menuFid="+Request.Form["fid"]);
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