using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using BrnMall.Web.Mobile.Models;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs.QrCode;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace BrnMall.Web.Mobile.controllers
{
    public class WeiXinController : Controller
    {


       
        
        

        //public  string AppId = ""; //微信公众号 appId
        //public  string Appsecret = "";  //微信公众号密钥
        //public  string baiduAk = "O5R6ObuaqtaC1K9FUysGTbNx";//百度开发者：baiduAk
        //public  string PartnerKey = "";//微信支付秘钥
        //public  string URLToken = "";//自己填写的Token  和开启开发者模式时填写的一样。

        //public  string certPath =  Server.MapPath("/cert/apiclient_cert.p12");//微信商户平台证书路径
        #region MyRegion
        //public  string certPwd = "";//证书密码  及   商户平台商户号  初始密码  可以修改
        public void Get()
        {
           BrnMall.Core.WeiXinConfig wxconfig = BrnMall.Core.BMAConfig.WeiXinConfig;
              string AppId = ""; //微信公众号 appId
          string Appsecret = "";  //微信公众号密钥
          string certPwd = "";
          //string baiduAk = "O5R6ObuaqtaC1K9FUysGTbNx";//百度开发者：baiduAk
          string PartnerKey = "";//微信支付秘钥
          string URLToken = "";//自己填写的Token  和开启开发者模式时填写的一样。
            // context.Response.ContentType = "text/plain";
            Appsecret = wxconfig.AppSecret;
            AppId = wxconfig.AppSecret;
            PartnerKey = wxconfig.PartnerKey;
            URLToken = wxconfig.Token; //GetKeyValue("token");
            try
            {
                if (Request.HttpMethod.ToUpper() == "GET")
                {
                    
                    if (!AccessTokenContainer.CheckRegistered(AppId))
                    {
                        AccessTokenContainer.Register(AppId, Appsecret);
                    }

                    //Auth(); //微信接入的测试  成为开发者第一步
                    string token = URLToken;//自己填写的Token  和开启开发者模式时填写的一样。
                    if (string.IsNullOrEmpty(token))
                    {
                        //LogTextHelper.Error(string.Format("WeixinToken 配置项没有配置！"));
                    }

                    string echoString = Request.QueryString["echostr"];
                    string signature = Request.QueryString["signature"];
                    string timestamp = Request.QueryString["timestamp"];
                    string nonce = Request.QueryString["nonce"];

                    if (CheckSignature(token, signature, timestamp, nonce))
                    {
                        if (!string.IsNullOrEmpty(echoString))
                        {
                            Response.Write(echoString);
                            //  Response.End();
                        }
                    }
                }
                if (Request.HttpMethod.ToUpper() == "POST")
                {
                    if (!AccessTokenContainer.CheckRegistered(AppId))
                    {
                        AccessTokenContainer.Register(AppId, Appsecret);
                    }



                    responseMsg();
                }
            }
            catch (Exception ex)
            {
                BMALog.Instance.Write("1"+ ex);
            }
        }
        #endregion
        
        #region 签名
        
        
        /// <summary>
        /// 验证微信签名
        /// </summary>
        public bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();

            if (tmpStr == signature)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region responseMsg 事件消息回复
        /// <summary>
        /// 事件消息回复
        /// </summary>
        public void responseMsg()
        {
            string content = "";//发送的内容
            
            Services.WeixinHelp wxdb = new Services.WeixinHelp();
            XmlDocument EventOrNews_XML = GetMsgXML();
            string EventName = (object)EventOrNews_XML.SelectSingleNode("xml").SelectSingleNode("Event") == null ? "" : EventOrNews_XML.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;//事件类型
            string EventKey = (object)EventOrNews_XML.SelectSingleNode("xml").SelectSingleNode("EventKey") == null ? "" : EventOrNews_XML.SelectSingleNode("xml").SelectSingleNode("EventKey").InnerText;//事件KEY值，与自定义菜单接口中KEY值对应 
            string MsgType = (object)EventOrNews_XML.SelectSingleNode("xml").SelectSingleNode("MsgType") == null ? "" : EventOrNews_XML.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText;//消息类型  语音为voice  文本为 text
            string UserOpenId = EventOrNews_XML.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText;
            try
            {
                #region 关注
                if (!string.IsNullOrEmpty(EventName) && EventName.Trim().ToLower() == "subscribe")//关注 - 事件类型
                {
                    //发送图文
                    //string newsTitle2 = "爱真丝爱生活";
                    //string newsDescription2 = "“上有天堂，下有苏杭“，苏州丝绸有限公司是一家集设计、开发、生产、销售于一体的专营高档丝绸系列产品的企业。";
                    //string newsPicUrl2 = "http://mmbiz.qpic.cn/mmbiz/aCEJh8QUibicnBtrSyjzJM7zKH9T9huibEwUoFhS3hsqiaZSe1LKgjy4h6SG9xeo5hcWc0fuzo3RsmKZCpBA7NJVIg/640?wx_fmt=jpeg&tp=webp&wxfrom=5&wx_lazy=1";
                    //string newsUrl2 = "http://mp.weixin.qq.com/s?__biz=MzI1ODA4NTY1OA==&mid=400760347&idx=1&sn=2493e225c51cec19dc84f293be07ba72#rd";
                    //Senparc.Weixin.MP.Entities.Article arc2 = new Senparc.Weixin.MP.Entities.Article();
                    //arc2.Title = newsTitle2;
                    //arc2.Description = newsDescription2;
                    //arc2.PicUrl = newsPicUrl2;
                    //arc2.Url = newsUrl2;
                    //List<Senparc.Weixin.MP.Entities.Article> list2 = new List<Senparc.Weixin.MP.Entities.Article>();
                    //list2.Add(arc2);
                    //CustomApi.SendNews(IsExistAccess_Token2(), UserOpenId, list2);
                    //发送图文结束


                    //int isfenxiao = 0;
                    //
                    int FId = 0;
                    if (EventKey.Contains("qrscene_"))//、用户通过带有场景值的二维码进行的关注事件   添加分享表   上下级关系
                    {
                        //isfenxiao = 1;
                        string[] Uids = EventKey.Split('_');//获取情景值
                                                            //LogHelper.WriteLog("进去了" + EventKey + "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"+Uids.Length.ToString());
                        try
                        {
                            FId = Convert.ToInt32(Uids[1]);
                        }
                        catch
                        {
                            FId = 0;
                        }
                    }
                    //
                    Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson dic = UserApi.Info(IsExistAccess_Token2(), UserOpenId);
                    //获取上级用户
                    //Users.GetPartUserById();
                    BrnMall.Core.BLL.bma_users userbll = new Core.BLL.bma_users();
                    if (userbll.GetRecordCount(" openid='" + UserOpenId + "'") == 0)//UserBll.IsExitsWeiUser(UserOpenId))//如果不存在 则插入记录
                    {
                        int uuid = 0;
                        if (FId != 0)//扫描其他人的二维码进行的关注
                        {

                            // wxdb.AddWeiXinUserInfo(dic, FId.ToString());
                            UserInfo userinfo = new UserInfo();

                            userinfo.Pid = FId;
                            userinfo.Openid = dic.openid;
                            userinfo.NickName = dic.nickname;
                            userinfo.UserName = dic.nickname;
                            userinfo.Password = Users.CreateUserPassword("Migewan123", "1");
                            userinfo.Avatar = dic.headimgurl;
                            userinfo.UserLevel = 0;
                            userinfo.PayCredits = 0;
                            userinfo.RankCredits = 0;
                            userinfo.VerifyEmail = 0;
                            userinfo.VerifyMobile = 0;
                            userinfo.MallAGid = 1;//非管理员组


                            userinfo.LastVisitIP = WebHelper.GetIP();
                            //userinfo.LastVisitRgId = Regions.GetRegionByIP(userinfo.LastVisitIP);
                            userinfo.LastVisitTime = DateTime.Now;
                            userinfo.RegisterIP = WebHelper.GetIP();
                            userinfo.Salt = Randoms.CreateRandomValue(6);

                            userinfo.RegisterTime = DateTime.Now;
                            uuid=Users.CreateUser(userinfo);
                            PartUserInfo info = Users.GetPartUserById(FId);
                            if (info != null)
                            {
                                try
                                {
                                    string msgContent = "恭喜您由[No." + info.Userno + "]推荐,成为创梦星火第" + (18000 + uuid) + "位会员,您的编号为[No." + (1000000 + uuid) + "]";
                                    GetPage("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + IsExistAccess_Token2(), GetSubcribePostData(userinfo.Openid, msgContent));
                                    msgContent = "您成功推荐了会员:[" + userinfo.NickName + "] .会员编号:[No." + (1000000 + uuid) + "],成为创梦星火第" + (18000 + uuid) + "位会员";
                                    GetPage("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + IsExistAccess_Token2(), GetSubcribePostData(info.Openid, msgContent));
                                    BMALog.Instance.Write("会员:" + userinfo.NickName + ". 编号:" + (1000000 + uuid) + " .openid:" + info.Openid + "上级:" + info.NickName + " .上级openid:" + info.Openid);
                                }
                                catch (Exception ex)
                                {
                                    BMALog.Instance.Write("微信erro:" + ex.ToString());
                                }
                            }
                            
                        }
                        else
                        {

                            UserInfo userinfo = new UserInfo();

                            userinfo.Pid = 3;
                            userinfo.Openid = dic.openid;
                            userinfo.NickName = dic.nickname;
                            userinfo.UserName = dic.nickname;
                            userinfo.Password = Users.CreateUserPassword("Migewan123", "1");
                            userinfo.Avatar = dic.headimgurl;
                            userinfo.UserLevel = 0;
                            userinfo.PayCredits = 0;
                            userinfo.RankCredits = 0;
                            userinfo.VerifyEmail = 0;
                            userinfo.VerifyMobile = 0;
                            userinfo.Salt = Randoms.CreateRandomValue(6);
                            userinfo.LastVisitIP = WebHelper.GetIP();
                            userinfo.MallAGid = 1;//非管理员组
                            //userinfo.LastVisitRgId = Regions.GetRegionByIP(userinfo.LastVisitIP);
                            userinfo.LastVisitTime = DateTime.Now;
                            userinfo.RegisterIP = WebHelper.GetIP();

                            userinfo.RegisterTime = DateTime.Now;
                            uuid= Users.CreateUser(userinfo);
                            //wxdb.AddWeiXinUserInfo(dic);//将关注者的信息 插入到数据库
                        }
                    }
                    else//如果存在 用户重新关注后 修改状态
                    {
                        //UserBll.upState(UserOpenId, 0, FId);
                    }
                    //BrnMall.Core.BLL.YX_Event entBll = new BrnMall.Core.BLL.YX_Event();
                    //DataTable dt = new DataTable();
                    //object weixinsubscribe =BrnMall.Core.Common. CacheHelper.GetCache("weixinsubscribe");//
                    //if (weixinsubscribe != null)
                    //{
                    //    dt = (DataTable)weixinsubscribe;
                    //}
                    //else
                    //{
                    //    entBll = entBll.GetList(" EventName='subscribe'").Tables[0];
                    //    dt = wxdb.getEvnet("subscribe");
                    //    if (dt != null)
                    //    {
                    //        BrnMall.Core.Common.CacheHelper.SetCache("weixinsubscribe", dt, TimeSpan.FromMinutes(60));
                    //    }
                    //}
                    //string msgContent = "欢迎加入创梦星火!"; //textdt.Rows[0]["msgContent"].ToString();
                    //                                 // CustomApi.SendText(IsExistAccess_Token2(), UserOpenId, msgContent);
                    //GetPage("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + IsExistAccess_Token2(), GetSubcribePostData(UserOpenId, msgContent));
                    //推送给上三层
                    
                    CreateMenu();//创建菜单
                }
                if (!string.IsNullOrEmpty(EventName) && EventName.Trim().ToUpper() == "SCAN")//扫描二维码事件 用户已关注时的事件推送
                {
                    //DataTable dt = new DataTable();
                    //object weixinSCAN = BrnMall.Core.Common.CacheHelper.GetCache("weixinSCAN");//设置单击菜单缓存  防止多次访问数据库
                    //if (weixinSCAN != null)
                    //{
                    //    dt = (DataTable)weixinSCAN;
                    //}
                    //else
                    //{
                    //    //dt = wxdb.getEvnet("SCAN");
                    //    if (dt != null)
                    //    {
                    //        BrnMall.Core.Common.CacheHelper.SetCache("weixinSCAN", dt, TimeSpan.FromMinutes(60));
                    //    }
                    //}
                    //if (dt != null)
                    //{

                    //    string msgType = dt.Rows[0]["msgType"].ToString();//用于匹配那张消息表：例如，YX_text  YX_news   YX_image等
                    //    string Id = dt.Rows[0]["Id"].ToString();//
                    //    string tableName = "";//表名
                    //    switch (msgType)
                    //    {
                    //        case "1": tableName = "YX_text"; break;
                    //        case "2": tableName = "YX_news"; break;
                    //        case "3": tableName = "YX_image"; break;
                    //        case "4": tableName = "YX_voice"; break;
                    //        case "5": tableName = "YX_video"; break;
                    //        case "6": tableName = "YX_music"; break;
                    //    }
                    //    if (tableName == "YX_text")//发送文本消息
                    //    {
                    //        DataTable textdt = new DataTable();

                    //        object text_dt = BrnMall.Core.Common.CacheHelper.GetCache("txtSCAN");//设置主菜单缓存  防止多次访问数据库V1003_TODAY_MUSIC
                    //        if (text_dt != null)
                    //        {
                    //            textdt = (DataTable)text_dt;
                    //        }
                    //        else
                    //        {
                    //            //textdt = wxdb.GetClickMenusMsg(tableName, Id, "SCAN");
                    //            if (textdt != null)
                    //            {
                    //                BrnMall.Core.Common.CacheHelper.SetCache("txtSCAN", textdt, TimeSpan.FromMinutes(60));
                    //            }
                    //        }

                    //        if (textdt != null)
                    //        {
                    //            string msgContent = textdt.Rows[0]["msgContent"].ToString();
                    //            GetPage("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + IsExistAccess_Token2(), GetSubcribePostData(UserOpenId, msgContent));
                    //        }
                    //    }
                    //    if (tableName == "YX_news")//发送图文消息
                    //    {
                    //        DataTable newsdt = new DataTable();
                    //        object news_dt = BrnMall.Core.Common.CacheHelper.GetCache("newsSCAN");//设置主菜单缓存  防止多次访问数据库V1003_TODAY_MUSIC
                    //        if (news_dt != null)
                    //        {

                    //            newsdt = (DataTable)news_dt;
                    //        }
                    //        else
                    //        {
                    //            //newsdt = wxdb.GetClickMenusMsg(tableName, Id);
                    //            if (newsdt != null)
                    //            {
                    //                BrnMall.Core.Common.CacheHelper.SetCache("newsSCAN", newsdt, TimeSpan.FromMinutes(60));
                    //            }
                    //        }

                    //        if (newsdt != null)
                    //        {
                    //            string newsTitle = newsdt.Rows[0]["newsTitle"].ToString();
                    //            string newsDescription = newsdt.Rows[0]["newsDescription"].ToString();
                    //            string newsPicUrl = newsdt.Rows[0]["newsPicUrl"].ToString();
                    //            string newsUrl = newsdt.Rows[0]["newsUrl"].ToString();
                    //            Senparc.Weixin.MP.Entities.Article arc = new Senparc.Weixin.MP.Entities.Article();
                    //            arc.Title = newsTitle;
                    //            arc.Description = newsDescription;
                    //            arc.PicUrl = newsPicUrl;
                    //            arc.Url = newsUrl;
                    //            List<Senparc.Weixin.MP.Entities.Article> list = new List<Senparc.Weixin.MP.Entities.Article>();
                    //            list.Add(arc);
                    //            CustomApi.SendNews(IsExistAccess_Token2(), UserOpenId, list);
                    //        }
                    //    }
                    //}
                }
                #endregion
            }
            catch (Exception ex)
            {
                BMALog.Instance.Write("插入数据" + ex.ToString());
            }
            if (!string.IsNullOrEmpty(EventName) && EventName.Trim().ToLower() == "unsubscribe")//取消订阅。
            {
                //UserBll.upState(UserOpenId, 1, 0);//修改当前状态

            }
            if (!string.IsNullOrEmpty(MsgType) && MsgType.Trim().ToLower() == "voice")
            {

            }
            if (!string.IsNullOrEmpty(MsgType) && MsgType.Trim().ToLower() == "image")
            {
                string touser = UserOpenId;
                string fromuser = EventOrNews_XML.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText;
                string CreateTime = EventOrNews_XML.SelectSingleNode("xml").SelectSingleNode("CreateTime ").InnerText;
                Response.Write(GetKeFuXML(touser, fromuser, CreateTime));
            }
            #region TExt
            if (!string.IsNullOrEmpty(MsgType) && MsgType.Trim().ToLower() == "text")
            {


            }
            #endregion
            if (!string.IsNullOrEmpty(MsgType) && MsgType.Trim().ToLower() == "transfer_customer_service")
            {
            }
            if (!string.IsNullOrEmpty(EventName) && EventName.Trim().ToUpper() == "LOCATION")
            {
                //上报地理位置事件
                //content = "/:rose";
                //WeiXinLocation wx = GetUserLocation(EventOrNews_XML);
                //string errcodeMsg = GetPage("http://api.map.baidu.com/geocoder/v2/?ak=" + baiduAk + "&callback=renderReverse&location=" + wx.Latitude1 + "," + wx.Longitude1 + "&output=json&pois=1", "");//调用百度地图API
                //errcodeMsg = errcodeMsg.Replace("renderReverse&&renderReverse(", "");
                //errcodeMsg = errcodeMsg.Replace(")", "");//将百度API返回的数据 转化为Json格式
                //BrnMall.Core.Common.LogHelper.WriteLog(errcodeMsg);
                //object cityobj = GetJsonValue(errcodeMsg, "city");
                //string city = cityobj == null ? "" : GetJsonValue(errcodeMsg, "city").ToString();
                //if (city == "")
                //{
                //    content = "暂时无法为您提供服务。";
                //}
                //else
                //{
                //    city = city.Replace("市", "");//将 苏州市转化为苏州  北京市 转化为北京 方便接口webservice调用
                //}
            }
            #region CliCK
            if (!string.IsNullOrEmpty(EventName) && EventName.Trim().ToLower() == "click")//菜单单击事件
            {

                DataTable dt = new DataTable();
                object menuclickdt = BrnMall.Core.Common.CacheHelper.GetCache("menuclickdt");//设置单击菜单缓存  防止多次访问数据库
                if (menuclickdt != null)
                {
                    dt = (DataTable)menuclickdt;
                }
                else
                {
                    //dt = wxdb.GetClickMenus();
                    if (dt != null)
                    {
                        BrnMall.Core.Common.CacheHelper.SetCache("menuclickdt", dt, TimeSpan.FromMinutes(60));
                    }
                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(EventName) && EventKey.Trim().ToUpper() == dt.Rows[i]["WX_MenusKey_URL"].ToString().ToUpper())
                    {
                        string flat1 = dt.Rows[i]["flat1"].ToString();//用于匹配那张消息表：例如，YX_text  YX_news   YX_image等
                        string Id = dt.Rows[i]["Id"].ToString();//
                        string tableName = "";//表名
                        switch (flat1)
                        {
                            case "1": tableName = "YX_text"; break;
                            case "2": tableName = "YX_news"; break;
                            case "3": tableName = "YX_image"; break;
                            case "4": tableName = "YX_voice"; break;
                            case "5": tableName = "YX_video"; break;
                            case "6": tableName = "YX_music"; break;
                        }
                        if (tableName == "YX_text")//发送文本消息
                        {
                            DataTable textdt = new DataTable();

                            object text_dt = BrnMall.Core.Common.CacheHelper.GetCache(EventKey);//设置主菜单缓存  防止多次访问数据库V1003_TODAY_MUSIC
                            if (text_dt != null)
                            {
                                textdt = (DataTable)text_dt;
                            }
                            else
                            {
                                //textdt = wxdb.GetClickMenusMsg(tableName, Id);
                                if (textdt != null)
                                {
                                    BrnMall.Core.Common.CacheHelper.SetCache(EventKey, textdt, TimeSpan.FromMinutes(60));
                                }
                            }

                            if (textdt != null)
                            {
                                string msgContent = textdt.Rows[0]["msgContent"].ToString();
                                GetPage("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + IsExistAccess_Token2(), GetSubcribePostData(UserOpenId, msgContent));
                            }
                        }
                        if (tableName == "YX_news")//发送图文消息
                        {
                            DataTable newsdt = new DataTable();
                            object news_dt = BrnMall.Core.Common.CacheHelper.GetCache(EventKey);//设置主菜单缓存  防止多次访问数据库V1003_TODAY_MUSIC
                            if (news_dt != null)
                            {
                                newsdt = (DataTable)news_dt;
                            }
                            else
                            {
                                //newsdt = wxdb.GetClickMenusMsg(tableName, Id);
                                if (newsdt != null)
                                {
                                    BrnMall.Core.Common.CacheHelper.SetCache(EventKey, newsdt, TimeSpan.FromMinutes(60));
                                }
                            }

                            if (newsdt != null)
                            {
                                string newsTitle = newsdt.Rows[0]["newsTitle"].ToString();
                                string newsDescription = newsdt.Rows[0]["newsDescription"].ToString();
                                string newsPicUrl = newsdt.Rows[0]["newsPicUrl"].ToString();
                                string newsUrl = newsdt.Rows[0]["newsUrl"].ToString();
                                Senparc.Weixin.MP.Entities.Article arc = new Senparc.Weixin.MP.Entities.Article();
                                arc.Title = newsTitle;
                                arc.Description = newsDescription;
                                arc.PicUrl = newsPicUrl;
                                arc.Url = newsUrl;
                                List<Senparc.Weixin.MP.Entities.Article> list = new List<Senparc.Weixin.MP.Entities.Article>();
                                list.Add(arc);
                                CustomApi.SendNews(IsExistAccess_Token2(), UserOpenId, list);
                            }
                        }
                        if (tableName == "YX_image")//发送图片消息
                        {
                            DataTable imagedt = new DataTable();

                            //imagedt = wxdb.GetClickMenusMsg(tableName, Id);

                            if (imagedt != null)
                            {

                            }
                        }
                        if (tableName == "YX_voice")//发送语音消息
                        {
                            DataTable voicedt = new DataTable();

                            // = wxdb.GetClickMenusMsg(tableName, Id);
                        }
                        if (tableName == "YX_video")//发送视频消息
                        {
                            DataTable videodt = new DataTable();
                            //videodt = wxdb.GetClickMenusMsg(tableName, Id);
                        }
                        if (tableName == "YX_music")//发送音乐消息
                        {
                            DataTable musicdt = new DataTable();
                            //musicdt = wxdb.GetClickMenusMsg(tableName, Id);
                        }

                    }
                }

            }
            #endregion

            if (!string.IsNullOrEmpty(EventName) && EventName.Trim().ToLower() == "scancode_push")//菜单单击事件
            {

            }

        }
        #endregion

        #region 创建菜单
        /// <summary>
        /// 创建菜单
        /// </summary>
        public void CreateMenu()
        {
            ButtonGroup bg = new ButtonGroup();
            DataTable dt = new DataTable();
            Services.WeixinHelp wxhp = new Services.WeixinHelp();
            object CreateMenudt = BrnMall.Core.Common.CacheHelper.GetCache("CreateMenudt");//设置主菜单缓存  防止多次访问数据库
            if (CreateMenudt != null)
            {
                dt = (DataTable)CreateMenudt;
            }
            else
            {
                //dt = wxdb.GetMainMenus();
                
                dt = wxhp.GetMainMenus();
                if (dt != null)
                {
                    BrnMall.Core.Common.CacheHelper.SetCache("CreateMenudt", dt, TimeSpan.FromMinutes(60));
                }
            }
            //
            DataTable childdt = new DataTable();
            object child_dt = BrnMall.Core.Common.CacheHelper.GetCache("childdt");//设置子菜单缓存  防止多次访问数据库V1003_TODAY_MUSIC
            if (child_dt != null)
            {
                childdt = (DataTable)child_dt;
            }
            else
            {
                childdt = wxhp.GetChildMenus();
                if (childdt != null)
                {
                    BrnMall.Core.Common.CacheHelper.SetCache("childdt", childdt, TimeSpan.FromMinutes(60));
                }
            }
            //
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string menuType = dt.Rows[i]["WX_MenuType"].ToString();
                    if (menuType != "0")//一级菜单类型不为0时，创建一级菜单 并赋予事件
                    {
                        if (menuType == "1")//click菜单
                        {
                            bg.button.Add(new SingleClickButton()
                            {
                                name = dt.Rows[i]["WX_menuName"].ToString(),
                                key = dt.Rows[i]["WX_MenusKey_URL"].ToString(),
                                type = ButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
                            });
                        }
                        else//view菜单
                        {
                            bg.button.Add(new SingleViewButton()
                            {
                                name = dt.Rows[i]["WX_menuName"].ToString(),
                                url = dt.Rows[i]["WX_MenusKey_URL"].ToString(),
                                type = ButtonType.view.ToString(),//默认已经设为此类型，这里只作为演示
                            });
                        }
                    }
                    else
                    {
                        var subButton = new SubButton()
                        {
                            name = dt.Rows[i]["WX_menuName"].ToString()
                        };

                        for (int j = 0; j < childdt.Rows.Count; j++)
                        {
                            if (childdt.Rows[j]["WX_Fid"].ToString().Trim() == dt.Rows[i]["Id"].ToString().Trim())
                            {
                                if (childdt.Rows[j]["WX_MenuType"].ToString() == "1")//click菜单
                                {
                                    subButton.sub_button.Add(new SingleClickButton()
                                    {
                                        key = childdt.Rows[j]["WX_MenusKey_URL"].ToString(),
                                        name = childdt.Rows[j]["WX_menuName"].ToString()
                                    });
                                }
                                else if (childdt.Rows[j]["WX_MenuType"].ToString() == "2")////view菜单
                                {
                                    subButton.sub_button.Add(new SingleViewButton()
                                    {
                                        url = childdt.Rows[j]["WX_MenusKey_URL"].ToString(),
                                        name = childdt.Rows[j]["WX_menuName"].ToString()
                                    });
                                }
                                else
                                {
                                    subButton.sub_button.Add(new SingleClickButton()
                                    {
                                        key = childdt.Rows[j]["WX_MenusKey_URL"].ToString(),
                                        name = childdt.Rows[j]["WX_menuName"].ToString()
                                    });
                                }
                            }
                        }
                        bg.button.Add(subButton);
                    }

                }
                var result = CommonApi.CreateMenu(IsExistAccess_Token2(), bg);
            }



        }
        #endregion

        #region 获取access_token
        /// <summary>  
        /// 根据当前日期 判断Access_Token 是否超期  如果超期返回新的Access_Token   否则返回之前的Access_Token  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        //public  string IsExistAccess_Token()
        //{
        //    string AccessToken = "";
        //    object Access_Token = CacheHelper.GetCache("Access_Token");//设置主菜单缓存  防止多次访问数据库V1003_TODAY_MUSIC
        //    if (Access_Token != null)
        //    {
        //        AccessToken = Access_Token.ToString();
        //    }
        //    else
        //    {
        //        AccessToken = AccessTokenContainer.GetToken(AppId, false);//重新获取Access_Token
        //        CacheHelper.SetCache("Access_Token", AccessToken, TimeSpan.FromMinutes(120));
        //    }
        //    return AccessToken;
        //}

        public  string IsExistAccess_Token2()
        {
            string AccessToken = "";
            BrnMall.Core.WeiXinConfig wxconfig = BrnMall.Core.BMAConfig.WeiXinConfig;
            object Access_Token = BrnMall.Core.Common.CacheHelper.GetCache("Access_Token");//设置主菜单缓存  防止多次访问数据库V1003_TODAY_MUSIC
            if (Access_Token != null)
            {
                //DateTime Tim = DateTime.Now;
                //string getsql = "select count(1) from TokenConfig where Id=1 and '" + Tim + "'>Tim";
                //object o = imp.GetSqlOne(CommandType.Text, getsql);
                //if (o.ToString().Trim() == "0")
                //{
                    AccessToken = Access_Token.ToString();
                //}
                //else
                //{
                    
                //    AccessToken = FirstAccess_Token(wxconfig.AppID, wxconfig.AppID);//重新获取Access_Token
                //    BrnMall.Core.Common.CacheHelper.SetCache("Access_Token", AccessToken, TimeSpan.FromMinutes(119));
                //    //string sql = "update TokenConfig set Tim='" + DateTime.Now.AddMinutes(3) + "'";
                //    //imp.GetSqlCount(CommandType.Text, sql);
                //}
            }
            else
            {
                AccessToken = FirstAccess_Token(wxconfig.AppID, wxconfig.AppSecret);//重新获取Access_Token
                BrnMall.Core.Common.CacheHelper.SetCache("Access_Token", AccessToken, TimeSpan.FromMinutes(60));
                //string sql = "update TokenConfig set Tim='" + DateTime.Now.AddMinutes(45) + "'";
                //imp.GetSqlCount(CommandType.Text, sql);
            }
            BrnMall.Core.Common.LogHelper.WriteLog("最新的TOKEN：" + AccessToken);
            return AccessToken;
        }

        /// <summary>
        /// 获取access_tokenaccess_token有效期目前为2个小时，需要定时去刷新，当调用微信公众平台的其他接口时，如果返回数值是：40001，即：获取access_token时AppSecret错误，或者access_token无效时，从新生成access_token
        /// </summary>
        /// <param name="Url"></param>
        public  string FirstAccess_Token(string appid, string Appsecret)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + Appsecret + "");
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                string Access_TokenJson = reader.ReadToEnd();  //获取的Json数据
                JObject obj = JObject.Parse(Access_TokenJson);
                string accessToken = obj["access_token"].ToString();
                return accessToken;
            }
        }
        #endregion

        #region 获取接收事件推送的XML结构
        /// <summary>
        /// 获取接收事件推送的XML结构
        /// </summary>
        /// <returns></returns>
        public  XmlDocument GetMsgXML()
        {
            Stream stream =  Request.InputStream;
            byte[] byteArray = new byte[stream.Length];
            stream.Read(byteArray, 0, (int)stream.Length);
            string postXmlStr = System.Text.Encoding.UTF8.GetString(byteArray);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(postXmlStr);
            return xmldoc;
        }

        #endregion

        #region 发送客服消息
        //1 发送文本消息
        //2 发送图片消息
        //3 发送语音消息
        //4 发送视频消息
        //5 发送音乐消息
        //6 发送图文消息
        /// <summary>
        /// 发送客服消息  
        /// </summary>
        /// <param name="posturl">请求的URL</param>
        /// <param name="postData">发送的数据 Json格式</param>
        /// <returns>json格式的字符串  通过获取Key为：errcode的值，判断accessToken是否过期，如果返回值为：40001 则accessToken无效，需要重新获取。实例代码：请参考WX_SendNews类</returns>
        public  string GetPage(string posturl, string postData)
        {
            //WX_SendNews news = new WX_SendNews(); 
            //posturl： news.Posturl;
            //postData：news.PostData;
            System.IO.Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...  
            try
            {
                // 设置参数  
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据  
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求  
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码  
                string content = sr.ReadToEnd();
                string err = string.Empty;

                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }
        #endregion

        #region 获取Json字符串某节点的值
        /// <summary>
        /// 获取Json字符串某节点的值
        /// </summary>
        public  string GetJsonValue(string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }

                    result = jsonStr.Substring(index, end - index);
                    result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                }
            }
            return result;
        }
        #endregion

        #region 获取用于换取二维码的Ticket
        /// <summary>
        /// 获取用于换取二维码（临时二维码和长久二维码）的Ticket  
        /// </summary>
        /// <param name="senceId">场景值ID</param>
        /// <param name="type">值为：long 时：代表长久性二维码  值为short时：代表临时二维码</param>
        /// <returns></returns>
        public  string GeterweimaTicket(int senceId, string type = "short")
        {
            string Ticket = "";
            CreateQrCodeResult re = new CreateQrCodeResult();
            try
            {
                if (type == "short")//临时二维码 
                {

                    re = QrCodeApi.Create(IsExistAccess_Token2(), 604800, senceId);
                }
                else
                {
                    re = QrCodeApi.Create(IsExistAccess_Token2(), 0, senceId);
                }
                Ticket = re.ticket;
            }
            catch
            {
                if (type == "short")//临时二维码 
                {

                    Ticket = CreateTicket(IsExistAccess_Token2());
                }
                else
                {
                    Ticket = CreateLongTicket(IsExistAccess_Token2());
                }
            }

            return Ticket;
        }

        #region 原方法

        /// <summary>  
        /// 创建二维码ticket  临时二维码
        /// </summary>  
        /// <returns></returns>  
        public  string CreateTicket(string TOKEN)
        {

            string result = "";
            string strJson = @"{""expire_seconds"": 604800, ""action_name"": ""QR_SCENE"", ""action_info"": {""scene"": {""scene_id"": 321}}}";
            string wxurl = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + TOKEN + "";
            result = GetPage(wxurl, strJson);
            result = GetJsonValue(result, "ticket");//获取票据
            ////LogHelper.WriteLog(result);
            return result;
        }

        /// <summary>
        /// 获取永久二维码Ticket
        /// </summary>
        /// <param name="TOKEN"></param>
        /// <returns></returns>
        public  string CreateLongTicket(string TOKEN)
        {

            string result = "";
            string strJson = @"{""action_name"": ""QR_LIMIT_SCENE"", ""action_info"": {""scene"": {""scene_id"": 123}}}";
            string wxurl = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + TOKEN + "";
            result = GetPage(wxurl, strJson);
            result = GetJsonValue(result, "ticket");//获取票据
            ////LogHelper.WriteLog(result);
            return result;
        }

        //{"ticket":"gQFw8DoAAAAAAAAAASxodHRwOi8vd2VpeGluLnFxLmNvbS9xL3pFZ3lETjdsMVNDcy1DRW9PMmE3AAIEP1TcVAMECAcAAA==","expire_seconds":1800,"url":"http:\/\/weixin.qq.com\/q\/zEgyDN7l1SCs-CEoO2a7"}
        /// <summary>
        /// 通过ticket换取二维码
        /// </summary>
        /// <param name="TICKET">票据</param>
        /// <param name="openId">二维码依照openId 命名</param>
        /// <param name="Pth">相对路径 @"\weixin\HuLu\erweima2"</param>
        /// <returns>下载二维码成功后的物理路径：D:\XXXXXX.com\kkk\erweima2\201503031044566511190.jpg</returns>
        public  string GetTicketImage(string TICKET, string openId, string Pth)
        {
            string content = string.Empty;
            string strpath = string.Empty;//生成的URL 也就是 关注的URL
            string savepath = string.Empty;//图片保存的路径

            string stUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" +  Server.UrlEncode(TICKET);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);

            req.Method = "GET";

            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                strpath = myResponse.ResponseUri.ToString();

                WebClient mywebclient = new WebClient();
                // @"
                savepath =  Server.MapPath(Pth) + "\\" + openId + "." + myResponse.ContentType.Split('/')[1].ToString();
                // //LogHelper.WriteLog(savepath);

                try
                {
                    mywebclient.DownloadFile(strpath, savepath);//下载生成的二维码图片
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                    BrnMall.Core.Common.LogHelper.WriteLog("错误了" + savepath);
                }


            }

            
            return savepath.ToString();


        }
        #endregion

        #endregion

        #region 跳转多客服XML结构
        /// <summary>
        /// 跳转多客服数据
        /// </summary>
        /// <returns></returns>
        /// 
        //<xml><ToUserName><![CDATA[ofyKns-7TTnU1LiNSnasxdERHXdE]]></ToUserName><FromUserName><![CDATA[jiuyuantang007]]></FromUserName><CreateTime>1425111485</CreateTime><MsgType><![CDATA[transfer_customer_service]]></MsgType><TransInfo><KfAccount>![CDATA[chenwolong@jiuyuantang007]]</KfAccount></TransInfo></xml> 
        /// <summary>
        /// 跳转多客服 随机分配客服
        /// </summary>
        /// <param name="touser">接收方帐号（收到的OpenID） </param>
        /// <param name="fromuser">开发者微信号 </param>
        /// <param name="CreateTime"> 	消息创建时间 （整型） </param>
        /// <returns></returns>
        public  string GetKeFuXML(string touser, string fromuser, string CreateTime)
        {
            string s = "<xml><ToUserName><![CDATA[" + touser + "]]></ToUserName><FromUserName><![CDATA[" + fromuser + "]]></FromUserName><CreateTime>" + CreateTime + "</CreateTime><MsgType><![CDATA[transfer_customer_service]]></MsgType></xml>";

            return s;
        }
        #endregion

        #region 关注发送文本Postdata
        /// <summary>
        /// 关注发送文本Postdata
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public  string GetSubcribePostData(string openId, string con)
        {
            return "{\"touser\":\"" + openId + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + con + "\"}}";
        }
        #endregion

        #region XML KEY
        /// <summary>
        /// XML KEY  通用方法
        /// </summary>
        /// <returns></returns>
        public  string GetXMLstrByKey(string Key, XmlDocument xml)
        {
            object strValue = xml.SelectSingleNode("xml").SelectSingleNode(Key).InnerText;
            if (strValue != null)
            {
                return strValue.ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 获取系统配置参数
        /// <summary>
        /// 获取公众平台AppId  获取之前 请先在后台配置
        /// </summary>
        /// <returns></returns>
        //public  string GetKeyValue(string key)
        //{
        //    string keyValue = "";
        //    object weixinAppId = BrnMall.Core.Common. CacheHelper.GetCache(key);//设置缓存  防止多次访问数据库
        //    if (weixinAppId != null)
        //    {
        //        keyValue = weixinAppId.ToString().Trim();
        //    }
        //    else
        //    {
        //        keyValue = bll.GetKeyValue(key);
        //        BrnMall.Core.Common.CacheHelper.SetCache(key, keyValue, TimeSpan.FromMinutes(30));
        //    }

        //    return keyValue;
        //}
        #endregion

        #region 微信系统配置
        /// <summary>
        /// 微信系统配置
        /// </summary>
        public  void insertSysconfig()
        {
            //string sql = "select count(*) from YX_sysConfigs";
            //int i = Convert.ToInt32(imp.GetSqlOne(CommandType.Text, sql));
            //if (i == 0)
            //{
            //    string appid = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('appid','appid','" + DateTime.Now + "')";
            //    string secret = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('secret','secret','" + DateTime.Now + "')";
            //    string URLToken = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('URLToken','Token','" + DateTime.Now + "')";
            //    string weixinnum = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('weixinnum','weixinnum','" + DateTime.Now + "')";
            //    string MchId = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('MchId','MchId','" + DateTime.Now + "')";
            //    string apisecret = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('apisecret','apisecret','" + DateTime.Now + "')";
            //    string duokefu = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('duokefu','false','" + DateTime.Now + "')";
            //    string Access_Token = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('Access_Token','Access_Token','" + DateTime.Now + "')";
            //    string Ticket = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('Ticket','Ticket','" + DateTime.Now + "')";
            //    string cardTicket = "insert into YX_sysConfigs(wxkey,wxkeyvalue,wxkeytime) values('cardTicket','cardTicket','" + DateTime.Now + "')";
            //    imp.GetSqlCount(CommandType.Text, appid);
            //    imp.GetSqlCount(CommandType.Text, secret);
            //    imp.GetSqlCount(CommandType.Text, URLToken);
            //    imp.GetSqlCount(CommandType.Text, weixinnum);
            //    imp.GetSqlCount(CommandType.Text, MchId);
            //    imp.GetSqlCount(CommandType.Text, apisecret);
            //    imp.GetSqlCount(CommandType.Text, duokefu);
            //    imp.GetSqlCount(CommandType.Text, Access_Token);
            //    imp.GetSqlCount(CommandType.Text, Ticket);
            //    imp.GetSqlCount(CommandType.Text, cardTicket);
            //}
        }


        #endregion

        #region 创建关注二维码


        public  string CreateTicket(string TOKEN, string UId)
        {

            string result = "";
            string strJson = @"{""expire_seconds"": 604800, ""action_name"": ""QR_SCENE"", ""action_info"": {""scene"": {""scene_id"": " + UId + "}}}";
            string wxurl = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + TOKEN + "";
            result = GetPage(wxurl, strJson);
            //LogHelper.WriteLog(result);
            result = GetJsonValue(result, "ticket");//获取票据
            ////LogHelper.WriteLog(result);
            return result;
        }


        //{"ticket":"gQFw8DoAAAAAAAAAASxodHRwOi8vd2VpeGluLnFxLmNvbS9xL3pFZ3lETjdsMVNDcy1DRW9PMmE3AAIEP1TcVAMECAcAAA==","expire_seconds":1800,"url":"http:\/\/weixin.qq.com\/q\/zEgyDN7l1SCs-CEoO2a7"}
        /// <summary>
        /// 通过ticket换取二维码
        /// </summary>
        /// <param name="TICKET">票据</param>
        /// <returns>下载二维码成功后的物理路径：D:\XXXXXX.com\kkk\erweima2\201503031044566511190.jpg</returns>
        public  string GetTicketImage(string TICKET, string openId)
        {
            string content = string.Empty;
            string strpath = string.Empty;//生成的URL 也就是 关注的URL
            string savepath = string.Empty;//图片保存的路径

            string stUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" +  Server.UrlEncode(TICKET);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);

            req.Method = "GET";

            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                strpath = myResponse.ResponseUri.ToString();

                WebClient mywebclient = new WebClient();
                // @"
                savepath =  Server.MapPath(@"\weixin\HuLu\erweima2") + "\\" + openId + "." + myResponse.ContentType.Split('/')[1].ToString();
                // //LogHelper.WriteLog(savepath);

                try
                {
                    mywebclient.DownloadFile(strpath, savepath);//下载生成的二维码图片
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                    BrnMall.Core.Common. LogHelper.WriteLog("错误了" + savepath);
                }


            }

            //LogHelper.WriteLog(savepath.ToString() + "都给我滚！");
            return savepath.ToString();


        }
        #endregion

        #region 微信获取用户的地理位置
        /// <summary>
        /// 上报地理位置事件推送
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        //private WeiXinLocation GetUserLocation(XmlDocument xml)
        //{
        //    WeiXinLocation wx = new WeiXinLocation();
        //    wx.ToUserName1 = xml.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText;
        //    wx.FromUserName1 = xml.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText;
        //    wx.MsgType1 = xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText;
        //    wx.Event1 = xml.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;
        //    wx.CreateTime1 = Convert.ToInt32(xml.SelectSingleNode("xml").SelectSingleNode("CreateTime").InnerText);
        //    //
        //    wx.Latitude1 = xml.SelectSingleNode("xml").SelectSingleNode("Latitude").InnerText;
        //    wx.Longitude1 = xml.SelectSingleNode("xml").SelectSingleNode("Longitude").InnerText;
        //    wx.Precision1 = xml.SelectSingleNode("xml").SelectSingleNode("Precision").InnerText;
        //    return wx;
        //}
        #endregion

        #region 获取关注者OpenId集合
        /// <summary>
        /// 获取关注用户列表
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="nextOpenId">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public  JArray GetUserList(string accessToken, string nextOpenId = null)
        {
            JArray arr = new JArray();

            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}", accessToken);
            if (!string.IsNullOrEmpty(nextOpenId))
            {
                url += "&next_openid=" + nextOpenId;
            }
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                string SorceData = reader.ReadToEnd();  //获取网页输出的数据
                JObject obj = JObject.Parse(SorceData);//转为Json对象
                string data = obj["data"].ToString();
                Dictionary<string, Object> obj2 = JsonConvert.DeserializeObject<Dictionary<string, Object>>(data);
                Object o = obj2["openid"];
                arr = JArray.FromObject(o);
            }

            return arr;
        }

        #endregion

        #region 获取用户的详细信息
        /// <summary>
        /// 获取用户的详细信息
        /// </summary>
        /// <param name="REFRESH_TOKEN">Token</param>
        /// <param name="OPENID">用户的OpenId</param>
        /// <returns>str</returns>
        public  Dictionary<string, object> Get_UserInfo(string REFRESH_TOKEN, string OPENID)
        {
            // Response.Write("获得用户信息REFRESH_TOKEN:" + REFRESH_TOKEN + "||OPENID:" + OPENID);  
            string UserInfo = GetJson("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + REFRESH_TOKEN + "&openid=" + OPENID);
            Dictionary<string, Object> obj2 = JsonConvert.DeserializeObject<Dictionary<string, Object>>(UserInfo);
            return obj2;
        }

        protected  string GetJson(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Encoding = Encoding.UTF8;
            string returnText = wc.DownloadString(url);

            if (returnText.Contains("errcode"))
            {
                //可能发生错误  
            }
            //Response.Write(returnText);  
            return returnText;
        }
        #endregion

        //[HttpPost]
        /// <summary>
        /// 用户协议
        /// </summary>
        /// <returns></returns>
        public string isreal()
        {
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                int uid = Core.Common.Utils.StrToInt(Request.Form["uid"]);
                Core.BLL.bma_users bll = new Core.BLL.bma_users();
                bll.UpdateIsReal(uid);
                return "";
            }
            else
            {
                return "页面不存在";
            }
        }
        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}