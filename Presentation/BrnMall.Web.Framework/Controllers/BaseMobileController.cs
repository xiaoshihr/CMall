using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using System.Linq;
using System.Collections.Generic;

using BrnMall.Core;
using BrnMall.Services;

namespace BrnMall.Web.Framework
{
    /// <summary>
    /// 移动前台基础控制器类
    /// </summary>
    public class BaseMobileController : BaseController
    {
        //工作上下文
        public MobileWorkContext WorkContext = new MobileWorkContext();

        protected override void Initialize(RequestContext requestContext)
        {
           
                base.Initialize(requestContext);
                this.ValidateRequest = false;

                WorkContext.IsHttpAjax = WebHelper.IsAjax();
                WorkContext.IP = WebHelper.GetIP();
                WorkContext.RegionInfo = Regions.GetRegionByIP(WorkContext.IP);
                WorkContext.RegionId = WorkContext.RegionInfo.RegionId;
                WorkContext.Url = WebHelper.GetUrl();
                WorkContext.UrlReferrer = WebHelper.GetUrlReferrer();

                //获得用户唯一标示符sid
                WorkContext.Sid = MallUtils.GetSidCookie();
                WorkContext.Openid = "";
                if (WorkContext.Sid.Length == 0)
                {
                    //生成sid
                    WorkContext.Sid = Sessions.GenerateSid();
                    //将sid保存到cookie中
                    MallUtils.SetSidCookie(WorkContext.Sid);
                }
            
            PartUserInfo partUserInfo;

            //获得用户id
            int uid = MallUtils.GetUidCookie();
            if (uid < 1)//当用户为游客时
            {
                //创建游客
                partUserInfo = Users.CreatePartGuest();
                BrnMall.Core.WeiXinConfig wxconfig = BrnMall.Core.BMAConfig.WeiXinConfig;
                #region 获取用户openid
                //if (Request.QueryString["code"] == null)
                //{
                //    string host = Request.Url.Host;
                //    string path = Request.Path;
                //    string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect", wxconfig.AppID, System.Web.HttpUtility.UrlEncode("http://" + host + path));

                //    Response.Redirect(url);
                //}
                //else
                //{

                //    //BrnMall.Core.WeiXinConfig wxconfig = BrnMall.Core.BMAConfig.WeiXinConfig;
                //    string code = Request.QueryString["code"];//获取授权code
                //                                              // string openIdUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + wxconfig.AppID + "&secret=" + wxconfig.AppSecret + "&code=" + code + "&grant_type=authorization_code";
                //    string openIdUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + wxconfig.AppID + "&secret=" + wxconfig.AppSecret + "&code=" + code + "&grant_type=authorization_code";
                //    string content = "";
                //    try
                //    {
                //        content = BrnMall.Core.WeiXinHelp.GetPage(openIdUrl, "");

                //    }
                //    catch
                //    {
                //        Response.Write("code:" + code + "这边错了");
                //    }

                //    string openid = "";//根据授权  获取当前人的openid
                //    try
                //    {
                //        openid = BrnMall.Core.WeiXinHelp.GetJsonValue(content, "openid");

                //    }
                //    catch
                //    {
                //        Response.Write("code:" + code + "||||content" + content);
                //    }
                //    Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson dic = null;

                //    dic = Senparc.Weixin.MP.AdvancedAPIs.UserApi.Info(WeiXinHelp.IsExistAccess_Token2(), openid);

                //    if (dic.subscribe == 1)
                //    {
                //        //获取用户
                //        //try
                //        //{
                //        //BMALog.Instance.Write("openid:" + openid);
                //        partUserInfo = Users.GetPartUserByOpenid(openid);

                //        if (partUserInfo != null)
                //        {


                //            MallUtils.SetUserCookie(partUserInfo, 30);
                //            //WorkContext.EncryptPwd= MallUtils.GetCookiePassword();
                //        }
                //        else//不存在
                //        {
                //            //partUserInfo = Users.CreatePartGuest();
                //            //WorkContext.EncryptPwd = string.Empty;
                //            //MallUtils.SetUidCookie(-1);
                //            //MallUtils.SetCookiePassword("");
                //            UserInfo userinfo = new UserInfo();

                //            userinfo.Pid = 3;
                //            userinfo.Openid = dic.openid;
                //            userinfo.NickName = dic.nickname;
                //            userinfo.UserName = dic.nickname;
                //            userinfo.Password = Users.CreateUserPassword("Migewan123", "1");
                //            userinfo.Avatar = dic.headimgurl;
                //            userinfo.UserLevel = 0;
                //            userinfo.PayCredits = 0;
                //            userinfo.RankCredits = 0;
                //            userinfo.VerifyEmail = 0;
                //            userinfo.VerifyMobile = 0;
                //            userinfo.Salt = Randoms.CreateRandomValue(6);
                //            userinfo.LastVisitIP = WebHelper.GetIP();
                //            userinfo.MallAGid = 1;//非管理员组
                //                                  //userinfo.LastVisitRgId = Regions.GetRegionByIP(userinfo.LastVisitIP);
                //            userinfo.LastVisitTime = DateTime.Now;
                //            userinfo.RegisterIP = WebHelper.GetIP();

                //            userinfo.RegisterTime = DateTime.Now;
                //            Users.CreateUser(userinfo);
                //            partUserInfo = Users.GetPartUserByOpenid(openid);
                //            MallUtils.SetUserCookie(partUserInfo, 30);
                //        }
                //    }
                //}


                #endregion
                #region 测试
                ////string openid = "oD-R9wWHGhJ3rcRgX7sbU5W0s9sU";
                string openid = "oD-R9wbVoaX-B-7kmk7sz1nz_-bc";
                partUserInfo = Users.GetPartUserByOpenid(openid);
                if (partUserInfo != null)
                {


                    MallUtils.SetUserCookie(partUserInfo, 30);
                    //WorkContext.EncryptPwd= MallUtils.GetCookiePassword();
                }
                else//不存在
                {
                    partUserInfo = Users.CreatePartGuest();
                    WorkContext.EncryptPwd = string.Empty;
                    MallUtils.SetUidCookie(-1);
                    MallUtils.SetCookiePassword("");
                }
                #endregion
            }
            else//当用户为会员时
            {
                //获得保存在cookie中的密码
                string encryptPwd = MallUtils.GetCookiePassword();
                //防止用户密码被篡改为危险字符
                if (encryptPwd.Length == 0 || !SecureHelper.IsBase64String(encryptPwd))
                {
                    //创建游客
                    partUserInfo = Users.CreatePartGuest();
                    encryptPwd = string.Empty;
                    MallUtils.SetUidCookie(-1);
                    MallUtils.SetCookiePassword("");
                }
                else
                {
                    partUserInfo = Users.GetPartUserByUidAndPwd(uid, MallUtils.DecryptCookiePassword(encryptPwd));
                    if (partUserInfo != null)
                    {
                        //发放登陆积分
                       // Credits.SendLoginCredits(ref partUserInfo, DateTime.Now);
                    }
                    else//当会员的账号或密码不正确时，将用户置为游客
                    {
                        partUserInfo = Users.CreatePartGuest();
                        encryptPwd = string.Empty;
                        MallUtils.SetUidCookie(-1);
                        MallUtils.SetCookiePassword("");
                    }
                }
                WorkContext.EncryptPwd = encryptPwd;
            }
            //try
            //{
                //设置用户等级
                if (UserRanks.IsBanUserRank(partUserInfo.UserRid) && partUserInfo.LiftBanTime <= DateTime.Now)
                {
                    UserRankInfo userRankInfo = UserRanks.GetUserRankByCredits(partUserInfo.PayCredits);
                    Users.UpdateUserRankByUid(partUserInfo.Uid, userRankInfo.UserRid);
                    partUserInfo.UserRid = userRankInfo.UserRid;
                }

                //当用户被禁止访问时重置用户为游客
                if (partUserInfo.UserRid == 1)
                {
                    partUserInfo = Users.CreatePartGuest();
                    WorkContext.EncryptPwd = string.Empty;
                    MallUtils.SetUidCookie(-1);
                    MallUtils.SetCookiePassword("");
                }
            //}
            //catch
            //{
            //    Response.Write("我的错");
            //}
            //try
            //{
                WorkContext.PartUserInfo = partUserInfo;
                WorkContext.Pid = partUserInfo.Pid;
                WorkContext.Userno = partUserInfo.Userno;
                WorkContext.UserLevel = partUserInfo.UserLevel;
                WorkContext.Openid = partUserInfo.Openid;
                WorkContext.Addtime = partUserInfo.Addtime;
                WorkContext.IsReal = partUserInfo.IsReal;


                WorkContext.Uid = partUserInfo.Uid;
                WorkContext.UserName = partUserInfo.UserName;
                WorkContext.UserEmail = partUserInfo.Email;
                WorkContext.UserMobile = partUserInfo.Mobile;
                WorkContext.Password = partUserInfo.Password;
                WorkContext.NickName = partUserInfo.NickName;
                WorkContext.Avatar = partUserInfo.Avatar;
                WorkContext.PayCreditName = Credits.PayCreditName;
                WorkContext.PayCreditCount = partUserInfo.PayCredits;
                WorkContext.RankCreditName = Credits.RankCreditName;
                WorkContext.RankCreditCount = partUserInfo.RankCredits;
            Core.BLL.SendBag bllsendbag = new Core.BLL.SendBag();
            //全部红包
            //WorkContext.CollarBag = bllsendbag.GetRecordSum(" Receiverid="+ partUserInfo.Uid );
            

            //已领红包
            WorkContext.NoCollarBag= bllsendbag.GetRecordSum(" Receiverid=" + partUserInfo.Uid+ " and Status=0"); ;
            //未领红包
            WorkContext.HaCollarBag=0;
                WorkContext.UserRid = partUserInfo.UserRid;
                WorkContext.UserRankInfo = UserRanks.GetUserRankById(partUserInfo.UserRid);
                //WorkContext.UserRTitle = WorkContext.UserRankInfo.Title;
                switch (WorkContext.UserLevel)
                    {
                    case 1:
                        WorkContext.UserRTitle = "银卡会员";
                        break;
                    case 2:
                        WorkContext.UserRTitle = "金卡会员";
                        break;
                    case 3:
                        WorkContext.UserRTitle = "钻石会员";
                        break;
                    default:
                        WorkContext.UserRTitle = "普通会员";
                        break;
                }
                //设置用户商城管理员组
                WorkContext.MallAGid = partUserInfo.MallAGid;
                WorkContext.MallAdminGroupInfo = MallAdminGroups.GetMallAdminGroupById(partUserInfo.MallAGid);
                WorkContext.MallAGTitle = WorkContext.MallAdminGroupInfo.Title;

                //设置当前控制器类名
                WorkContext.Controller = RouteData.Values["controller"].ToString().ToLower();
                //设置当前动作方法名
                WorkContext.Action = RouteData.Values["action"].ToString().ToLower();
                WorkContext.PageKey = string.Format("/{0}/{1}", WorkContext.Controller, WorkContext.Action);

                WorkContext.ImageCDN = WorkContext.MallConfig.ImageCDN;
                WorkContext.CSSCDN = WorkContext.MallConfig.CSSCDN;
                WorkContext.ScriptCDN = WorkContext.MallConfig.ScriptCDN;

                //在线总人数
                WorkContext.OnlineUserCount = OnlineUsers.GetOnlineUserCount();
                //在线游客数
                WorkContext.OnlineGuestCount = OnlineUsers.GetOnlineGuestCount();
                //在线会员数
                WorkContext.OnlineMemberCount = WorkContext.OnlineUserCount - WorkContext.OnlineGuestCount;
                //搜索词
                WorkContext.SearchWord = string.Empty;
                //购物车中商品数量
                WorkContext.CartProductCount = Carts.GetCartProductCountCookie();
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("赋值时:"+ex.ToString());
            //}
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            //商城已经关闭
            if (WorkContext.MallConfig.IsClosed == 1 && WorkContext.MallAGid == 1 && WorkContext.PageKey != Url.Action("login", "account") && WorkContext.PageKey != Url.Action("logout", "account"))
            {
                filterContext.Result = PromptView(WorkContext.MallConfig.CloseReason);
                return;
            }
            //判断是否阅读了用户协议
            if (WorkContext.IsReal==0)
            {
                filterContext.Result = IsRealView();
                return;
            }
            //当前时间为禁止访问时间
            if (ValidateHelper.BetweenPeriod(WorkContext.MallConfig.BanAccessTime) && WorkContext.MallAGid == 1 && WorkContext.PageKey != Url.Action("login", "account") && WorkContext.PageKey != Url.Action("logout", "account"))
            {
                filterContext.Result = PromptView("当前时间不能访问本商城");
                return;
            }

            //当用户ip在被禁止的ip列表时
            if (ValidateHelper.InIPList(WorkContext.IP, WorkContext.MallConfig.BanAccessIP))
            {
                filterContext.Result = PromptView("您的IP被禁止访问本商城");
                return;
            }

            //当用户ip不在允许的ip列表时
            if (!string.IsNullOrEmpty(WorkContext.MallConfig.AllowAccessIP) && !ValidateHelper.InIPList(WorkContext.IP, WorkContext.MallConfig.AllowAccessIP))
            {
                filterContext.Result = PromptView("您的IP被禁止访问本商城");
                return;
            }

            //当用户IP被禁止时
            if (BannedIPs.CheckIP(WorkContext.IP))
            {
                filterContext.Result = PromptView("您的IP被禁止访问本商城");
                return;
            }
            
            //判断目前访问人数是否达到允许的最大人数
            if (WorkContext.OnlineUserCount > WorkContext.MallConfig.MaxOnlineCount && WorkContext.MallAGid == 1 && (WorkContext.Controller != "account" && (WorkContext.Action != "login" || WorkContext.Action != "logout")))
            {
                filterContext.Result = PromptView("商城人数达到访问上限, 请稍等一会再访问！");
                return;
            }

            //判断是否关注了公众号
            if (string.IsNullOrEmpty(WorkContext.Openid))
            {
                filterContext.Result = PromptView("网页错误,请联系管理员");
                return;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            //当用户为会员时,更新用户的在线时间
            if (WorkContext.Uid > 0)
                Users.UpdateUserOnlineTime(WorkContext.Uid);

            //更新在线用户
            Asyn.UpdateOnlineUser(WorkContext.Uid, WorkContext.Sid, WorkContext.NickName, WorkContext.IP, WorkContext.RegionId);
            //更新PV统计
            Asyn.UpdatePVStat(WorkContext.StoreId, WorkContext.Uid, WorkContext.RegionId, WebHelper.GetBrowserType(), WebHelper.GetOSType());
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string message)
        {
            return View("prompt", new PromptModel(message));
        }
        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult IsRealView()
        {
            return View("isreal");
        }
    }
}
