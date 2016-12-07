using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace qiaojiaren.Manger
{
    public partial class wei_MenusAdd : basePage
    {
        public string SelCon = "";
        BrnShop.Core.BLL.YX_weiXinMenus bll = new BrnShop.Core.BLL.YX_weiXinMenus();
        BrnShop.Core.Model.YX_weiXinMenus mol = new BrnShop.Core.Model.YX_weiXinMenus();
        BrnShop.Core.DBUtility.T_SQL imp = new BrnShop.Core.DBUtility.T_SQL();
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("childmenu") as Label).Text = "添加微菜单";
            //添加操作
            if (Request.QueryString["action"] == "action")
            {
                AddMenus();
                return;
            }
            //
            if (!Page.IsPostBack)
            {
                loadMenus();
            }




        }

        #region 加载一级菜单
        /// <summary>
        /// 加载一级栏目
        /// </summary>
        public void loadMenus()
        {
            string sql = "select * from YX_weiXinMenus where WX_Fid=0 and WX_MenusKey_URL=''";
            DataTable dt = imp.GetSqlDataSet(CommandType.Text, sql).Tables[0];
            //
            //                            

            //            dt.Rows[0]           
            StringBuilder sb = new StringBuilder();
            sb.Append("<select id='WX_Fid' name='WX_Fid' class='txb2'>");
            sb.Append("<option value='0'>--请选择--</option>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<option value='" + dt.Rows[i]["Id"] + "'>" + dt.Rows[i]["WX_menuName"] + "</option>");
            }
            sb.Append("</select>");
            SelCon = sb.ToString();
        }
        #endregion

        #region 添加菜单操作
        /// <summary>
        /// 添加菜单操作
        /// </summary>
        protected void AddMenus()
        {
            string WX_menuName = CommonMethod.CheckParamThrow(Request.Form["WX_menuName"]);//菜单名称
            string WX_MenuType = CommonMethod.CheckParamThrow(Request.Form["WX_MenuType"]);//菜单类型
            string WX_MenusKey_URL = CommonMethod.CheckParamThrow(Request.Form["WX_MenusKey_URL"]);//菜单取值
            string WX_Fid = Request.Form["WX_Fid"];

            mol.WX_menuName = WX_menuName;
            mol.WX_MenuType = WX_MenuType;
            mol.WX_MenusKey_URL = WX_MenusKey_URL;
            mol.WX_Fid = Convert.ToInt32(WX_Fid);
            mol.WX_AddTime = DateTime.Now;
            try
            {
                mol.flat2 = Convert.ToInt32(Request.Form["flat2"]);
            }
            catch
            {
                mol.flat2 = 0;
            }
            string sql = "";
            int onum = 0;
            if (mol.WX_Fid.ToString().Trim() == "0")
            {
                sql = "select count(*) from YX_weiXinMenus where WX_Fid=0";
                onum = Convert.ToInt32(imp.GetSqlOne(CommandType.Text, sql));
                if (onum >= 3)
                {
                    Response.Write("NO");//一级菜单大于三
                }
                else
                {
                    if (bll.Add(mol) != 0)
                    {
                        Response.Write("OK");//添加成功
                    }
                    else
                    {
                        Response.Write("error");//系统错误  添加失败
                    }
                }
            }
            else
            {
                sql = "select count(*) from YX_weiXinMenus where WX_Fid=" + mol.WX_Fid + "";
                onum = Convert.ToInt32(imp.GetSqlOne(CommandType.Text, sql));
                if (onum >= 5)
                {
                    Response.Write("NOO");//二级菜单大于5
                }
                else
                {
                    if (bll.Add(mol) != 0)
                    {
                        Response.Write("OK");//添加成功
                    }
                    else
                    {
                        Response.Write("error");//系统错误  添加失败
                    }
                }

            }
            Response.End();
        }
        #endregion
    }
}