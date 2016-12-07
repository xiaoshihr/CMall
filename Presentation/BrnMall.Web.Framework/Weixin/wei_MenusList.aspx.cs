using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace qiaojiaren.Manger
{
    public partial class wei_MenusList : basePage
    {
        public DataTable dt = new DataTable();
        BrnShop.Core.DBUtility.T_SQL imp = new BrnShop.Core.DBUtility.T_SQL();

        BrnShop.Core.BLL.YX_weiXinMenus bll = new BrnShop.Core.BLL.YX_weiXinMenus();
        BrnShop.Core.Model.YX_weiXinMenus mol = new BrnShop.Core.Model.YX_weiXinMenus();
        protected void Page_Load(object sender, EventArgs e)
        {
            //删除
            if (Request.QueryString["action"] == "del")
            {
                delMenus(Request.QueryString["id"]);
                return;
            }

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
            BindInfo("");
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
            if (type == 0)
            {
                return "主菜单";
            }
            else
            {
                return (ButtonType)(type - 1);
            }
          
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
            if (type == 0)
            {
                return "";
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

        #region 删除栏目  递归算法
        /// <summary>
        /// 删除栏目  递归算法  思想：从子节点着手，依次删除！
        /// </summary>
        /// <param name="id">根节点</param>
        /// <returns></returns>
        public void delMenus(object id)
        {
            if (true) // 检测是否有删除的权限！
            {
                object rootID = id; //根节点
                //依次查找子节点，若没有以该子节点为父节点的节点存在时，则将该子节点删除。依次类推。直至删除所有以ROOTID为父节点的所有节点
                while (id != null)
                {
                    string sql2 = "select Id from [YX_weiXinMenus] where WX_Fid=" + id + "";
                    object o = imp.GetSqlOne(CommandType.Text, sql2);
                    if (o != null)
                    {
                        id = o;
                    }
                    else
                    {
                        string sql3 = "delete from [YX_weiXinMenus] where id=" + id + ""; // 删除微信菜单

                        string sql4 = "delete from YX_text where EventId=" + id + " and EventCate='menu'";//删除菜单对应的事件  
                        string sql5 = "delete from YX_news where EventId=" + id + " and EventCate='menu'";//删除菜单对应的事件
                        string sql6 = "delete from YX_image where EventId=" + id + " and EventCate='menu'";//删除菜单对应的事件
                        string sql7 = "delete from YX_voice where EventId=" + id + " and EventCate='menu'";//删除菜单对应的事件
                        string sql8 = "delete from YX_video where EventId=" + id + " and EventCate='menu'";//删除菜单对应的事件
                        string sql9 = "delete from YX_music where EventId=" + id + " and EventCate='menu'";//删除菜单对应的事件
                        imp.GetSqlCount(CommandType.Text, sql3);
                        imp.GetSqlCount(CommandType.Text, sql4);
                        imp.GetSqlCount(CommandType.Text, sql5);
                        imp.GetSqlCount(CommandType.Text, sql6);
                        imp.GetSqlCount(CommandType.Text, sql7);
                        imp.GetSqlCount(CommandType.Text, sql8);
                        imp.GetSqlCount(CommandType.Text, sql9);
                        id = rootID;
                        if (imp.GetSqlOne(CommandType.Text, "select id from YX_weiXinMenus where id=" + rootID + "") == null)//根节点已被删除，循环结束！
                        {
                            id = null;
                            Response.Write("OK");
                        }
                    }
                }
            }
            Response.End();
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
                if (mol.WX_Fid == 0)//如果是一级菜单  则不可以
                {
                    Response.Write("NOO");
                }
                else
                {
                    if (mol.WX_MenuType != "1")//菜单事件不是click
                    {
                        Response.Write("NO");
                    }
                    else
                    {
                        Response.Write("OK");
                    }
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

        #region 检测是否拥有事件，已拥有，则可编辑事件！
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

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            BrnShop.Core.Common. CacheHelper.RemoveAllCache();
            CommonMethod.Alert("系统缓存清空成功！");
        }
    }
}

#region 菜单按钮类型
/// <summary>
/// 菜单按钮类型
/// </summary>
public enum ButtonType
{
    /// <summary>
    /// 点击
    /// </summary>
    click,

    /// <summary>
    /// Url
    /// </summary>
    view,

    /// <summary>
    /// 扫码推事件的事件推送
    /// </summary>
    scancode_push,

    /// <summary>
    /// 扫码推事件且弹出“消息接收中”提示框的事件推送
    /// </summary>
    scancode_waitmsg,

    /// <summary>
    /// 弹出系统拍照发图的事件推送
    /// </summary>
    pic_sysphoto,

    /// <summary>
    /// 弹出拍照或者相册发图的事件推送
    /// </summary>
    pic_photo_or_album,

    /// <summary>
    /// 弹出微信相册发图器的事件推送
    /// </summary>
    pic_weixin,

    /// <summary>
    /// 弹出地理位置选择器的事件推送
    /// </summary>
    location_select
}
#endregion

#region 菜单事件类型
/// <summary>
/// 菜单事件类型
/// </summary>
public enum MsgType
{
    /// <summary>
    /// 文本
    /// </summary>
    text,

    /// <summary>
    /// 图文
    /// </summary>
    news,

    /// <summary>
    /// 图片
    /// </summary>
    image,

    /// <summary>
    /// 语音
    /// </summary>
    voice,

    /// <summary>
    /// 视频
    /// </summary>
    video,

    /// <summary>
    /// 音乐
    /// </summary>
    music

}
#endregion