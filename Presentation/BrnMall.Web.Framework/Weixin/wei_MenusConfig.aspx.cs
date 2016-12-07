using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wuqi.Webdiyer;
namespace qiaojiaren.Manger
{
    public partial class wei_MenusConfig : basePage
    {
        public DataTable dt = new DataTable();
        BrnShop.Core.DBUtility.T_SQL imp = new BrnShop.Core.DBUtility.T_SQL();

        BrnShop.Core.BLL.YX_weiXinMenus bll = new BrnShop.Core.BLL.YX_weiXinMenus();
        BrnShop.Core.Model.YX_weiXinMenus mol = new BrnShop.Core.Model.YX_weiXinMenus();
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (Request.QueryString["action"] == "checkMenu")//检测菜单是否可以加事件 例如view类型的菜单  不需要添加事件等
            {
                checkMenu(Request.QueryString["id"]);
                return;
            }

            if (Request.QueryString["action"] == "cekevent")//检测是否已添加了事件  如果已添加  不可重复添加
            {
                cekevent(Request.QueryString["id"]);
                return;
            }
            if (Request.QueryString["action"] == "cekevent2")//检测是否已添加了事件  如果没添加  则不可编辑事件
            {
                cekevent2(Request.QueryString["id"]);
                return;
            }
            BindInfo(" (WX_MenuType='0' or WX_MenuType='1') and WX_MenusKey_URL!=''");
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
            dt = BrnShop.Core.DBUtility.ProcUtility.GetPageProBll("YX_weiXinMenus", "*", " Id desc", where, pageSize, pageIndex, out totalPage, out recordCount);
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
            BindInfo(" (WX_MenuType='0' or WX_MenuType='1') and WX_MenusKey_URL!=''");
        }
        #endregion

        #region 获取父亲菜单
        /// <summary>
        /// 获取父亲菜单
        /// </summary>
        /// <param name="menusID">菜单Id</param>
        /// <returns></returns>
        public object GetFatherMenus(object menusID)
        {
            string sql = "select isNull(WX_menuName,'') from [YX_weiXinMenus] where Id=" + menusID + "";
            return imp.GetSqlOne(CommandType.Text, sql);
        }
        #endregion

        #region 获取菜单类型
        /// <summary>
        /// 获取菜单类型
        /// </summary>
        /// <param name="WX_MenuType">菜单类型数值</param>
        /// <returns></returns>
        public object GetMenusType(object WX_MenuType)
        {
            int type = Convert.ToInt32(WX_MenuType);
            return (ButtonType)(type - 1);
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

        #region 剪切字符串
        /// <summary>
        /// 剪切字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public string cutstr(string str, int len)
        {
            return CommonMethod.CutString(str, len);
        }
        #endregion


        #region 检测菜单是否可以加事件
        /// <summary>
        /// 检测菜单是否可以加事件
        /// </summary>
        /// <param name="Id">菜单Id</param>
        protected void checkMenu(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                mol = bll.GetModel(Convert.ToInt32(Id));

                if (mol.WX_MenuType != "1")//菜单事件不是click
                {
                    Response.Write("NO");
                }
                else
                {
                    Response.Write("OK");
                }

            }
            else
            {
                Response.Write("error");
            }
            Response.End();
        }
        #endregion

        #region 检测是否拥有事件，已拥有，则不可再添加！
        /// <summary>
        /// 检测是否拥有事件，已拥有，则不可再添加！
        /// </summary>
        /// <param name="id">菜单Id</param>
        protected void cekevent(string id)
        {
            mol = bll.GetModel(Convert.ToInt32(id));
            int flat = Convert.ToInt32(mol.flat1);
            if (flat != 0)//已添加事件
            {
                Response.Write("NO");
            }
            else
            {
                Response.Write("OK");
            }
            Response.End();
        }
        #endregion

        #region 检测是否拥有事件，已拥有，则编辑事件！
        /// <summary>
        ///检测是否拥有事件，已拥有，则可编辑事件！
        /// </summary>
        /// <param name="id">菜单Id</param>
        protected void cekevent2(string id)
        {
            mol = bll.GetModel(Convert.ToInt32(id));
            int flat = Convert.ToInt32(mol.flat1);
            if (flat != 0)//已添加事件
            {
                Response.Write("OK");
            }
            else
            {
                Response.Write("NO");
            }
            Response.End();
        }
        #endregion
    }
}
