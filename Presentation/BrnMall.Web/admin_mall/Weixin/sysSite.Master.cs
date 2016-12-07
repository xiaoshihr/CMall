using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace qiaojiaren.qiaojiaren
{
    public partial class sysSite : System.Web.UI.MasterPage
    {
        // .BLL.YX_Menus Mbll = new BrnMall.Core.BLL.YX_Menus();//导航栏对象
        //BrnMall.Core.Model.YX_Menus Mmol = new BrnMall.Core.Model.YX_Menus();
        //////


        //BrnMall.Core.BLL.YX_sysNews newsBll = new BrnMall.Core.BLL.YX_sysNews();//站内信
        //BrnMall.Core.Model.YX_sysNews newsMol = new BrnMall.Core.Model.YX_sysNews();//


        //BrnMall.Core.BLL.YX_sysLog logBll = new BrnMall.Core.BLL.YX_sysLog();
        //public BrnMall.Core.Model.YX_sysLog logMol = new BrnMall.Core.Model.YX_sysLog();//日志表
        ////
        //BrnMall.Core.BLL.YX_UserInfo cusBll = new BrnMall.Core.BLL.YX_UserInfo();//用户表
        //public BrnMall.Core.Model.YX_UserInfo cusMol = new BrnMall.Core.Model.YX_UserInfo();
        //public int NewsCount = 0;//未读的站内信条数

        //public string mainLeaveName = "";//一级栏目名称
        //public string childLeaveName = "";//二级栏目名称
        //public string mid = "0";
        //public string Fid = "0";
        protected void Page_Load(object sender, EventArgs e)
        {

            //mid = Request.QueryString["mid"];
            //Fid = Request.QueryString["menuFid"];

            //GetMenusName(Fid, mid);
        }

//        #region 获取左侧导航栏
//        //当前支持22个图标 22个图标对应22个一级栏目，意味着最多支持22个一级栏目
//        private string[] iconAry = new string[] { "icon-cogs", "icon-bookmark-empty", "icon-table", "icon-briefcase", "icon-gift", "icon-sitemap", "icon-folder-open", "icon-globe", "icon-user", "icon-external-link", "icon-bell", "icon-th", "icon-file-text", "icon-map-marker", "icon-bar-chart", "icon-group", "icon-coffee", "icon-envelope-alt", "icon-calendar", "icon-remove", "icon-pencil", "icon-edit" };
//        public string GetLeftMenus()
//        {

//            int k = -1;
//            string userRight = "";
//            if (Session["URight"] == null)
//            {
//                Response.Redirect("../Login.aspx");
//            }
//            else
//            {
//                userRight = Session["URight"].ToString();
//            }
//            StringBuilder sb = new StringBuilder();
//            sb.Append(@"<ul class='page-sidebar-menu'> 
//                    <li class='start active'>
//                    <a href='index.aspx'>
//                    <i class='icon-home'></i>
//                    <span class='title'>首页</span>
//                    <span class='selected'></span>
//                    </a>
//                    </li>");
//            // SqlDataReader dr = Mbll.getDr(" rightID=" + userRight + " and Fid=0 order by flat1 desc");//加载一级目录
//            DataTable dr = Mbll.GetList(" rightID like '%" + userRight + "%' and Fid=0 order by flat1 desc").Tables[0];
//            if (dr != null)
//            {
//                for (int i = 0; i < dr.Rows.Count; i++)
//                //while (dr.Read())
//                {
//                    k++;//用于加载不同图标
//                    if (k > 21)
//                    {
//                        k = 21;
//                    }
//                    sb.Append(@"<li id='li" + dr.Rows[i]["Id"] + "' class=''><a href='javascript:;'><i class='" + iconAry[k] + "'></i><span class='title'>" + dr.Rows[i]["menuName"] + "</span><span id='li" + dr.Rows[i]["Id"] + "' class='arrow'></span></a>");//此后判断是否有子目录
//                    //SqlDataReader dr2 = Mbll.getDr(" rightID=" + userRight + " and Fid=" + dr.Rows[i]["ID"] + " order by flat1 desc");
//                    DataTable dr2 = Mbll.GetList(" rightID like '%" + userRight + "%' and Fid=" + dr.Rows[i]["ID"] + " order by flat1 desc").Tables[0];
//                    if (dr2 != null)
//                    {
//                        sb.Append(@"<ul class='sub-menu'>");//如果存在子目录 则加载.Rows[j]
//                        for (int j = 0; j < dr2.Rows.Count; j++)
//                        //while (dr2.Read())
//                        {
//                            sb.Append(@"<li> <a href='" + dr2.Rows[j]["menuPth"] + "?menuFid=" + dr.Rows[i]["Id"] + "&mid=" + dr2.Rows[j]["Id"] + "'>" + dr2.Rows[j]["menuName"] + "</a></li>");
//                        }
//                        sb.Append("</ul>");
//                    }
//                    sb.Append("</li>");
//                }
//            }
//            sb.Append("</ul>");
//            //

//            return sb.ToString();

//        }
//        #endregion

//        #region 获取最新10条站内信
//        public string GetYX_sysNews()
//        {
//            string TimInfo = "";
//            StringBuilder sb = new StringBuilder(); sb.Append("");
//            DataTable dt = newsBll.GetList(10, " newsState=0", "Addtime Desc").Tables[0];
//            if (dt.Rows.Count > 0)
//            {
//                sb.Append(@"<li class='dropdown' id='header_notification_bar'>
//
//                        <a href='JavaScript:void(0)' class='dropdown-toggle' data-toggle='dropdown'>
//
//                            <i class='icon-envelope'></i>
//
//                            <span class='badge'>" + dt.Rows.Count + @"</span>
//
//                        </a>
//                        <ul class='dropdown-menu extended notification'>
//
//                            <li>
//
//                                <p>最新10条未读信息如下：</p>
//
//                            </li> ");

//                for (int i = 0; i < dt.Rows.Count; i++)
//                {
//                    //根据时间间隔  判断是显示年、月、天 还是小时小时  还是显示分钟
//                    string Timdiff = newsBll.GetDiffTime(dt.Rows[i]["Id"]);
//                    string[] timAry = Timdiff.Split('_');
//                    if (Timdiff.Contains("yy"))//年
//                    {
//                        TimInfo = timAry[1] + "年前";
//                    }
//                    if (Timdiff.Contains("mm"))//月
//                    {
//                        TimInfo = timAry[1] + "月前";
//                    }
//                    if (Timdiff.Contains("dd"))//日
//                    {
//                        TimInfo = timAry[1] + "天前";
//                    }
//                    if (Timdiff.Contains("hh"))//时
//                    {
//                        TimInfo = timAry[1] + "小时前";
//                    }
//                    if (Timdiff.Contains("mi"))//分
//                    {
//                        TimInfo = timAry[1] + "分钟前";
//                    }
//                    if (Timdiff.Contains("ss"))//秒
//                    {
//                        TimInfo = timAry[1] + "秒前";
//                    }
//                    sb.Append(@"<li>
//
//                                <a href='inlineNews.aspx'>
//
//                                    <span class='label label-success'><i class='icon-plus'></i></span>
//
//                                    " + CommonMethod.CutString(dt.Rows[i]["newsContent"].ToString(), 20) + @"
//
//								<span class='time'>" + TimInfo + @"</span>
//
//                                </a>
//
//                            </li>");
//                }
//                sb.Append(@" <li class='external'>
//
//                                <a href='inlineNews.aspx'>查看所有信息 <i class='m-icon-swapright'></i></a>
//
//                            </li>
//
//                        </ul></li>");
//            }
//            else
//            {
//                sb.Append(@"<li class='dropdown' id='header_notification_bar'>
//
//                        <a href='JavaScript:void(0)' class='dropdown-toggle' data-toggle='dropdown'>
//
//                            <i class='icon-envelope'></i>
//
//                            <span class='badge'>0</span>
//
//                        </a></li> ");

//            }
//            return sb.ToString();
//        }
//        #endregion

//        #region 获取导航目录
//        private void GetMenusName(string fid, string mid)
//        {
//            if (!string.IsNullOrEmpty(fid))
//            {
//                Mmol = Mbll.GetModel(Convert.ToInt32(fid));
//                if (Mmol != null)
//                {
//                    mainLeaveName = Mmol.menuName;
//                }
//                else
//                {
//                    mainLeaveName = "首页";
//                }
//            }
//            else
//            {
//                mainLeaveName = "首页";
//            }
//            if (!string.IsNullOrEmpty(mid))
//            {
//                Mmol = Mbll.GetModel(Convert.ToInt32(mid));
//                if (Mmol != null)
//                {
//                    childLeaveName = Mmol.menuName;
//                    childmenu.Text = childLeaveName;
//                }
//                else
//                {
//                    childLeaveName = "";
//                }
//            }
//            else
//            {
//                childLeaveName = "";
//            }
//        }
//        #endregion

//        #region 写入日志
//        /// <summary>
//        /// 写入日志
//        /// </summary>
//        /// <param name="logcate">操作类型</param>
//        /// <param name="logContent">操作内容</param>
//        public void writeLog(string logcate, string logContent)
//        {
//            logMol.userNum = Session["username"] != null ? Session["username"].ToString() : "";
//            logMol.userId = Session["userid"] != null ? Convert.ToInt32(Session["userid"]) : 0;
//            logMol.userRight = Session["URight"] != null ? Convert.ToInt32(Session["URight"]) : 0;
//            logMol.logcate = logcate;
//            logMol.logContent = logContent;
//            logMol.logIp = CommonMethod.IPAddress;
//            logMol.logTime = DateTime.Now;
//            logBll.Add(logMol);
//        }
//        #endregion

//        #region 获取最新10条站内信
//        public string GetsysNews()
//        {
//            string TimInfo = "";
//            StringBuilder sb = new StringBuilder(); sb.Append("");
//            DataTable dt = newsBll.GetList(10, " newsState=0", "Addtime Desc").Tables[0];
//            if (dt.Rows.Count > 0)
//            {
//                sb.Append(@"<li class='dropdown' id='header_notification_bar'>
//
//                        <a href='JavaScript:void(0)' class='dropdown-toggle' data-toggle='dropdown'>
//
//                            <i class='icon-envelope'></i>
//
//                            <span class='badge'>" + dt.Rows.Count + @"</span>
//
//                        </a>
//                        <ul class='dropdown-menu extended notification'>
//
//                            <li>
//
//                                <p>最新10条未读信息如下：</p>
//
//                            </li> ");

//                for (int i = 0; i < dt.Rows.Count; i++)
//                {
//                    //根据时间间隔  判断是显示年、月、天 还是小时小时  还是显示分钟
//                    string Timdiff = newsBll.GetDiffTime(dt.Rows[i]["Id"]);
//                    string[] timAry = Timdiff.Split('_');
//                    if (Timdiff.Contains("yy"))//年
//                    {
//                        TimInfo = timAry[1] + "年前";
//                    }
//                    if (Timdiff.Contains("mm"))//月
//                    {
//                        TimInfo = timAry[1] + "月前";
//                    }
//                    if (Timdiff.Contains("dd"))//日
//                    {
//                        TimInfo = timAry[1] + "天前";
//                    }
//                    if (Timdiff.Contains("hh"))//时
//                    {
//                        TimInfo = timAry[1] + "小时前";
//                    }
//                    if (Timdiff.Contains("mi"))//分
//                    {
//                        TimInfo = timAry[1] + "分钟前";
//                    }
//                    if (Timdiff.Contains("ss"))//秒
//                    {
//                        TimInfo = timAry[1] + "秒前";
//                    }
//                    sb.Append(@"<li>
//
//                                <a href='inlineNews.aspx'>
//
//                                    <span class='label label-success'><i class='icon-plus'></i></span>
//
//                                    " + CommonMethod.CutString(dt.Rows[i]["newsCate"].ToString(), 20) + @"
//
//								<span class='time'>" + TimInfo + @"</span>
//
//                                </a>
//
//                            </li>");
//                }
//                sb.Append(@" <li class='external'>
//
//                                <a href='inlineNews.aspx'>查看所有信息 <i class='m-icon-swapright'></i></a>
//
//                            </li>
//
//                        </ul></li>");
//            }
//            else
//            {
//                sb.Append(@"<li class='dropdown' id='header_notification_bar'>
//
//                        <a href='JavaScript:void(0)' class='dropdown-toggle' data-toggle='dropdown'>
//
//                            <i class='icon-envelope'></i>
//
//                            <span class='badge'>0</span>
//
//                        </a></li> ");

//            }
//            return sb.ToString();
//        }
//        #endregion

  
    }
}