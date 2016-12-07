using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace qiaojiaren.Manger
{
    public class basePage : System.Web.UI.Page
    {
        //BrnShop.Core.BLL.YX_Menus Mbll = new BrnShop.Core.BLL.YX_Menus();//导航栏对象
        //BrnShop.Core.Model.YX_Menus Mmol = new BrnShop.Core.Model.YX_Menus();
        //
        //BrnShop.Core.BLL.YX_sysLog logBll = new BrnShop.Core.BLL.YX_sysLog();
        //public BrnShop.Core.Model.YX_sysLog logMol = new BrnShop.Core.Model.YX_sysLog();//日志表
        //
        //BrnShop.Core.BLL.YX_UserInfo cusBll = new BrnShop.Core.BLL.YX_UserInfo();//用户表
        //public BrnShop.Core.Model.YX_UserInfo cusMol = new BrnShop.Core.Model.YX_UserInfo();

        //BrnShop.Core.BLL.YX_sysNews newsBll = new BrnShop.Core.BLL.YX_sysNews();//站内信
        //BrnShop.Core.Model.YX_sysNews newsMol = new BrnShop.Core.Model.YX_sysNews();//
        public int NewsCount = 0;//未读的站内信条数

        protected override void OnInit(EventArgs e)
        {
            //if (Session["userid"] == null)
            //{
            //    Response.Redirect("../Login.aspx");
            //}
            //else
            //{
            //    cusMol = cusBll.GetModel(Convert.ToInt32(Session["userid"]));
            //}
        }

        #region 获取模板CSS及JS
        public string GetCSS()
        {
            object CSS = BrnShop.Core.Common.CacheHelper.GetCache("CSS");//设置CSS缓存，防止多次创建StringBuilder对象
            if (CSS != null)
            {
                return CSS.ToString();
            }
            else
            {
                StringBuilder sb = new StringBuilder(); sb.Append("");
                sb.Append(@"<link href='media/css/bootstrap.min.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/bootstrap-responsive.min.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/font-awesome.min.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/style-metro.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/style.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/style-responsive.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/default.css' rel='stylesheet' type='text/css' id='style_color' />

                        <link href='media/css/uniform.default.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/jquery.gritter.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/daterangepicker.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/fullcalendar.css' rel='stylesheet' type='text/css' />

                        <link href='media/css/jqvmap.css' rel='stylesheet' type='text/css' media='screen' />

                        <link href='media/css/jquery.easy-pie-chart.css' rel='stylesheet' type='text/css' media='screen' />

                        <link rel='shortcut icon' href='media/image/favicon.ico' />
                        <link href='media/sweetalert/sweetalert.css' rel='stylesheet' />
                        <script src='media/sweetalert/sweetalert-dev.js'></script>
                        <script src='media/sweetalert/sweetalert.min.js'></script>");
                BrnShop.Core.Common.CacheHelper.SetCache("CSS", sb.ToString(), TimeSpan.FromMinutes(60));
                return sb.ToString();
            }


        }

        public string GetJS()
        {
            object JS = BrnShop.Core.Common.CacheHelper.GetCache("JS");//设置JS缓存，防止多次创建StringBuilder对象
            if (JS != null)
            {
                return JS.ToString();
            }
            else
            {
                StringBuilder sb = new StringBuilder(); sb.Append("");
                sb.Append(@" <!-- END FOOTER -->

            <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->

            <!-- BEGIN CORE PLUGINS -->

            <script src='media/js/jquery-1.10.1.min.js' type='text/javascript'></script>

            <script src='media/js/jquery-migrate-1.2.1.min.js' type='text/javascript'></script>

            <!-- IMPORTANT! Load jquery-ui-1.10.1.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->

            <script src='media/js/jquery-ui-1.10.1.custom.min.js' type='text/javascript'></script>

            <script src='media/js/bootstrap.min.js' type='text/javascript'></script>

            <!--[if lt IE 9]>

	        <script src='media/js/excanvas.min.js'></script>

	        <script src='media/js/respond.min.js'></script>  

	        <![endif]-->

            <script src='media/js/jquery.slimscroll.min.js' type='text/javascript'></script>

            <script src='media/js/jquery.blockui.min.js' type='text/javascript'></script>

            <script src='media/js/jquery.cookie.min.js' type='text/javascript'></script>

            <script src='media/js/jquery.uniform.min.js' type='text/javascript'></script>

            <!-- END CORE PLUGINS -->

            <!-- BEGIN PAGE LEVEL PLUGINS -->

            <script src='media/js/jquery.vmap.js' type='text/javascript'></script>

            <script src='media/js/jquery.vmap.russia.js' type='text/javascript'></script>

            <script src='media/js/jquery.vmap.world.js' type='text/javascript'></script>

            <script src='media/js/jquery.vmap.europe.js' type='text/javascript'></script>

            <script src='media/js/jquery.vmap.germany.js' type='text/javascript'></script>

            <script src='media/js/jquery.vmap.usa.js' type='text/javascript'></script>

            <script src='media/js/jquery.vmap.sampledata.js' type='text/javascript'></script>

            <script src='media/js/jquery.flot.js' type='text/javascript'></script>

            <script src='media/js/jquery.flot.resize.js' type='text/javascript'></script>

            <script src='media/js/jquery.pulsate.min.js' type='text/javascript'></script>

            <script src='media/js/date.js' type='text/javascript'></script>

            <script src='media/js/daterangepicker.js' type='text/javascript'></script>

            <script src='media/js/jquery.gritter.js' type='text/javascript'></script>

            <script src='media/js/fullcalendar.min.js' type='text/javascript'></script>

            <script src='media/js/jquery.easy-pie-chart.js' type='text/javascript'></script>

            <script src='media/js/jquery.sparkline.min.js' type='text/javascript'></script>

            <!-- END PAGE LEVEL PLUGINS -->

            <!-- BEGIN PAGE LEVEL SCRIPTS -->

            <script src='media/js/app.js' type='text/javascript'></script>

            <script src='media/js/index.js' type='text/javascript'></script>

            <!-- END PAGE LEVEL SCRIPTS -->

            <script>

        jQuery(document).ready(function () {

            App.init(); // initlayout and core plugins

            Index.init();

            Index.initJQVMAP(); // init index page's custom scripts

            Index.initCalendar(); // init index page's custom scripts

            Index.initCharts(); // init index page's custom scripts

            Index.initChat();

            Index.initMiniCharts();

            Index.initDashboardDaterange();

           

        });

        </script>

        <!-- END JAVASCRIPTS -->

        <script type='text/javascript'>  var _gaq = _gaq || []; _gaq.push(['_setAccount', 'UA-37564768-1']); _gaq.push(['_setDomainName', 'keenthemes.com']); _gaq.push(['_setAllowLinker', true]); _gaq.push(['_trackPageview']); (function () { var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true; ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s); })();</script>
        <script src='js/jquery-1.7.2.js'></script>
            <script type='text/javascript'>
       
                jQuery(document).ready(function () {
                    var menuFid = document.getElementById('menuFid').value;
                    var sa = $('#li' + menuFid + '').html();
                    $('.page-sidebar-menu li').attr('class', '');
                    $('#li' + menuFid + '').attr('class', 'start active');
                    $('#span' + menuFid + '').attr('class', 'selected');
                });
            </script>");
                BrnShop.Core.Common.CacheHelper.SetCache("JS", sb.ToString(), TimeSpan.FromMinutes(60));
                return sb.ToString();
            }
        }
        #endregion

        #region 获取左侧导航栏
        //当前支持22个图标 22个图标对应22个一级栏目，意味着最多支持22个一级栏目
        //private string[] iconAry = new string[] { "icon-cogs", "icon-bookmark-empty", "icon-table", "icon-briefcase", "icon-gift", "icon-sitemap", "icon-folder-open", "icon-globe", "icon-user", "icon-external-link", "icon-bell", "icon-th", "icon-file-text", "icon-map-marker", "icon-bar-chart", "icon-group", "icon-coffee", "icon-envelope-alt", "icon-calendar", "icon-remove", "icon-pencil", "icon-edit" };
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
//           // SqlDataReader dr = Mbll.getDr(" rightID=" + userRight + " and Fid=0 order by flat1 desc");//加载一级目录
//            DataTable dr = Mbll.GetList(" rightID like '%" + userRight + "%' and Fid=0 order by flat1 desc").Tables[0];
//            if (dr!=null)
//            {
//                for (int i = 0; i < dr.Rows.Count;i++ )
//                    //while (dr.Read())
//                    {
//                        k++;//用于加载不同图标
//                        if (k > 21)
//                        {
//                            k = 21;
//                        }
//                        sb.Append(@"<li id='li" + dr.Rows[i]["Id"] + "' class=''><a href='javascript:;'><i class='" + iconAry[k] + "'></i><span class='title'>" + dr.Rows[i]["menuName"] + "</span><span id='li" + dr.Rows[i]["Id"] + "' class='arrow'></span></a>");//此后判断是否有子目录
//                        //SqlDataReader dr2 = Mbll.getDr(" rightID=" + userRight + " and Fid=" + dr.Rows[i]["ID"] + " order by flat1 desc");
//                        DataTable dr2 = Mbll.GetList(" rightID like '%" + userRight + "%' and Fid=" + dr.Rows[i]["ID"] + " order by flat1 desc").Tables[0];
//                        if (dr2!=null)
//                        {
//                            sb.Append(@"<ul class='sub-menu'>");//如果存在子目录 则加载.Rows[j]
//                            for (int j = 0; j < dr2.Rows.Count; j++)
//                            //while (dr2.Read())
//                            {
//                                sb.Append(@"<li> <a href='" + dr2.Rows[j]["menuPth"] + "?menuFid=" + dr.Rows[i]["Id"] + "&mid=" + dr2.Rows[j]["Id"] + "'>" + dr2.Rows[j]["menuName"] + "</a></li>");
//                            }
//                            sb.Append("</ul>");
//                        }
//                        sb.Append("</li>");
//                    }
//            }
//            sb.Append("</ul>");
//            //

//            return sb.ToString();

//        }
        #endregion

        #region 写入日志
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logcate">操作类型</param>
        /// <param name="logContent">操作内容</param>
        //public void writeLog(string logcate, string logContent)
        //{
        //    logMol.userNum = Session["username"] != null ? Session["username"].ToString() : "";
        //    logMol.userId = Session["userid"] != null ? Convert.ToInt32(Session["userid"]) : 0;
        //    logMol.userRight =  0;
        //    logMol.logcate = logcate;
        //    logMol.logContent = logContent;
        //    logMol.logIp = CommonMethod.IPAddress;
        //    logMol.logTime = DateTime.Now;
        //    logBll.Add(logMol);
        //}
        #endregion

        #region 获取最新10条站内信
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
        #endregion
    }
}
