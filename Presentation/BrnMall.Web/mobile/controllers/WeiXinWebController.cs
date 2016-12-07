using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Data;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using BrnMall.Web.Mobile.Models;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs.QrCode;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.TenPayLibV3;
using System.IO;

using Deepleo.Weixin.SDK.Helpers;
using System.Collections.Specialized;
//using Deepleo.Web.Services;
using BrnMall.Services;
using Deepleo.Weixin.SDK.Pay;
using Deepleo.Weixin.SDK.JSSDK;
using System.Text;
using BrnMall.Web.Mobile.Models;
namespace BrnMall.Web.Mobile.Controllers
{
    public class WeiXinWebController : BaseMobileController
    {

        #region 微信
        /// <summary>
        /// 微信二维码
        /// </summary>
        public ActionResult WxQRCode()
        {

            if (WorkContext.UserLevel <= 0)
                return PromptView("请先提升您的等级");

            string pth1 = "~/mobile/Qcode/" + WorkContext.Openid.ToString() + ".jpeg";
            string pth2 = "~/mobile/Qcode/" + WorkContext.Openid.ToString() + ".jpg";
            //LogHelper.WriteLog("二维码：" + pth1); LogHelper.WriteLog("二维码：" + pth2);
            //if (File.Exists(pth1)||File.Exists(pth2))
            //{
            //    File.Delete(pth1); File.Delete(pth2);
            //    LogHelper.WriteLog("二维码222222");
            //}
            CreateQrCodeResult re = new CreateQrCodeResult();
            re = QrCodeApi.Create(WeiXinHelp.IsExistAccess_Token2(), 604000, Convert.ToInt32(WorkContext.Uid));
            string tickets = re.ticket;
            //string quserno = WorkContext.Userno.ToString();
            WeiXinHelp.GetTicketImage(tickets, WorkContext.Openid.ToString(), @"\mobile\Qcode");
            string picPth = @"~/mobile/Qcode/" + WorkContext.Openid.ToString() + ".jpeg";
            WxQRCodeModel model = new WxQRCodeModel();
            model.picPth = "";
            if (!System.IO.File.Exists(picPth))
            {
                // LogHelper.WriteLog("二维码JPG");
                {
                    model.picPth = @"/mobile/Qcode/" + WorkContext.Openid.ToString() + ".jpg";
                }
            }
            WebClient mywebclient = new WebClient();
            // @"
            string savepath = Server.MapPath("~/mobile/Aavatar/") + WorkContext.Openid + ".jpg";
            // //LogHelper.WriteLog(savepath);

            //try
            //{
            if (!System.IO.File.Exists(savepath))
            {
                mywebclient.DownloadFile(WorkContext.Avatar, savepath);
            }

            //下载生成的二维码图片
            //}
            //catch (Exception ex)
            //{
            //    // ex.ToString();
            //    // BrnMall.Core.Common.LogHelper.WriteLog("错误了" + savepath);
            //}
            Core.ImageHelp.DrawImage("", "", (float)1.0, "", "", "", WorkContext.NickName, WorkContext.Openid);
            model.picPth = @"/mobile/Qcode/new/" + WorkContext.Openid.ToString() + ".jpg";
            return View(model);
        }
        /// <summary>
        /// 微信二维码
        /// </summary>
        public string WxQRCode1()
        {
            //WebRequest wreq = WebRequest.Create(WorkContext.Avatar);
            //HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
            //Stream s = wresp.GetResponseStream();
            //System.Drawing.Image img;
            //img = System.Drawing.Image.FromStream(s);
            //img.Save(Server.MapPath("~/Aavatar/" + WorkContext.Openid+".jpg"), ImageFormat.Jpeg);   //保存
            WebClient mywebclient = new WebClient();
            // @"
           string savepath = Server.MapPath("~/mobile/Aavatar/") + WorkContext.Openid + ".jpg";
            // //LogHelper.WriteLog(savepath);

            //try
            //{
            if (!System.IO.File.Exists(savepath))
            {
                mywebclient.DownloadFile(WorkContext.Avatar, savepath);
            }
            
            //下载生成的二维码图片
            //}
            //catch (Exception ex)
            //{
            //    // ex.ToString();
            //    // BrnMall.Core.Common.LogHelper.WriteLog("错误了" + savepath);
            //}
            return Core.ImageHelp.DrawImage("", "", (float)1.0, "", "", "", WorkContext.NickName, WorkContext.Openid);
            //return View();
        }
        public ActionResult UserUpdate()
        {

            Models.UserUpdateModel model = new UserUpdateModel();
            if (WebHelper.IsGet())
            {
                if (WorkContext.UserLevel < 3)
                {
                    WeiXinConfig wxconfig = BMAConfig.WeiXinConfig;
                    var appId = wxconfig.AppID; //WeixinConfig.AppID;
                    var nonceStr = Util.CreateNonce_str();
                    var timestamp = DateTime.Now.Ticks.ToString().Substring(0, 10);
                    var domain = wxconfig.Domain;
                    var url = Request.Url.ToString().Replace("#", "");// domain + Request.Url.PathAndQuery;

                    var jsTickect = WeiXinHelp.IsExistjsapi_ticket();
                    var string1 = "";
                    var signature = WeiXinHelp.GetjsSDK_Signature(nonceStr, jsTickect, timestamp, url); //JSAPI.GetSignature(jsTickect, nonceStr, timestamp, url, out string1);
                    var userAgent = Request.UserAgent;
                    var userVersion = userAgent.Substring(userAgent.LastIndexOf("/") + 1);//微信版本号高于或者等于5.0才支持微信支付
                    model = new UserUpdateModel
                    {
                        appId = appId,
                        nonceStr = nonceStr,
                        signature = signature,
                        timestamp = timestamp,
                        jsapiTicket = jsTickect,
                        string1 = string1,
                        userAgent = userAgent,
                        userVersion = userVersion,
                    };
                    return View(model);
                    //return View(model);
                }

                else
                {
                    return PromptView("/mob/ucenter", "你已是钻石会员");
                }
            }
            else
            {
                int paytype = WebHelper.GetFormInt("paytype");
                int payfee = 1;
                switch (paytype)
                {
                    case 1:
                        if (WorkContext.UserLevel >= 1)
                        {
                            return AjaxResult("", "已是银卡会员");
                        }
                        payfee = 100;
                        break;
                    case 2:
                        if (WorkContext.UserLevel >= 2)
                        {
                            return AjaxResult("error", "已是金卡会员");
                        }
                        payfee = 200;
                        if (WorkContext.UserLevel == 0)
                        {
                            paytype = 4;
                            payfee = 300;
                        }
                        break;
                    case 3:
                        if (WorkContext.UserLevel >= 3)
                        {
                            return AjaxResult("error", "已是钻石会员");
                        }
                        payfee = 300;
                        if (WorkContext.UserLevel == 1)

                        {
                            paytype = 5;
                            payfee = 500;
                        }
                        if (WorkContext.UserLevel == 0)

                        {
                            paytype = 6;
                            payfee = 600;
                        }
                        break;
                    default:
                        return AjaxResult("error", "请选择卡类型");

                        break;


                }


                //return AjaxResult("success", WebHelper.GetRequestData("http://localhost:6174/mob/weixinweb/Notify", "body=" + paytype.ToString() + "&payfee=" + payfee));
                // return AjaxResult("success", GetpayJson(paytype, payfee));
                return View();
            }
            //model.appid = "100";
            //return View(model);
        }
        #endregion

        public string getpv()
        {
            return BrnMall.Core.Common.HttpHelper.PostUrl("http://localhost:6174/mob/weixinweb/Notify", "body=body&detail=detail&attach=attach&trade_type=JSAPI&goods_tag=goods_tag&product_id=product_id&total_fee=100&openid=oD-R9wbVoaX-B-7kmk7sz1nz_-bc", Encoding.UTF8);
        }
        /// <summary>
        /// 生成prepay_id
        /// 
        /// POST参数：
        /// body(商品描述)
        /// detail(商品详情)
        /// total_fee(总金额,只能是整数，单位：分)
        /// trade_type：APP，Native,JSAPI
        /// goods_tag(商品标记):代金券或立减优惠功能的参数
        /// product_id(商品ID):trade_type=NATIVE，此参数必传。此id为二维码中包含的商品ID，商户自行定义.
        /// openid(用户标识):trade_type=JSAPI，此参数必传，用户在商户appid下的唯一标识。下单前需要调用【网页授权获取用户信息】接口获取到用户的Openid。 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult WxPay()
        //public HttpResponseMessage WxPay()
        {
            if (WebHelper.IsGet())
            {
                var result = new
                {
                    return_code = "erro",
                    return_msg = "请求错误",
                };
                return Json(result);
                //return new HttpResponseMessage { Content = new StringContent(result.ToString(), System.Text.Encoding.UTF8, "application/json") };
            }
            else
            {
                var form = Request.Form;
                var sPara = GetRequestPost(form);
                WeiXinConfig wxconfig = BMAConfig.WeiXinConfig;
                if (sPara.Count <= 0)
                {
                    throw new ArgumentNullException();
                }
                //LogWriter.Default.WriteInfo(form.ToString());//记录请求关键信息到日志中去
                BMALog.Instance.Write(form.ToString());
                var out_trade_no = BrnMall.Core.Common.Utils.GenerateOutTradeNo() + "_" + sPara["paytype"]; //Guid.NewGuid().ToString();
                var domain = wxconfig.Domain;
                var body = sPara["body"];
                var detail = sPara["detail"];
                var attach = sPara["attach"];
                var fee_type = "CNY";
                var total_fee = int.Parse(sPara["total_fee"]);
                var trade_type = sPara["trade_type"];
                var spbill_create_ip = (trade_type == "APP" || trade_type == "NATIVE") ? Request.UserHostName : wxconfig.Spbill_create_ip;
                var time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
                var time_expire = DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss");//默认1个小时订单过期，开发者可自定义其他超时机制，原则上微信订单超时时间不超过2小时
                var goods_tag = sPara["goods_tag"];
                var notify_url = string.Format("{0}/mob/Wxpay/Notify", domain);//与下面的Notify对应，开发者可自定义其他url地址
                var product_id = sPara["product_id"];
                var openid = sPara["openid"];
                var partnerKey = wxconfig.PartnerKey;
                var content = WxPayAPI.UnifiedOrder(
                              wxconfig.AppID, wxconfig.Mch_id, wxconfig.Device_info, Util.CreateNonce_str(),
                              body, detail, attach, out_trade_no, fee_type, total_fee, spbill_create_ip, time_start, time_expire,
                              goods_tag, notify_url, trade_type, product_id, openid, partnerKey);
                BMALog.Instance.Write("url:" + notify_url + "|||con:" + content);
                if (content.return_code.Value == "SUCCESS" && content.result_code.Value == "SUCCESS")
                {
                    WxPayAPIHelp.WxPayData jsApiParam = new WxPayAPIHelp.WxPayData();
                    string payTimeSamp = DateTime.Now.Ticks.ToString().Substring(0, 10);
                    jsApiParam.SetValue("appId", wxconfig.AppID);
                    jsApiParam.SetValue("timeStamp", payTimeSamp);
                    jsApiParam.SetValue("nonceStr", content.nonce_str.Value);
                    jsApiParam.SetValue("package", "prepay_id=" + content.prepay_id.Value);
                    jsApiParam.SetValue("signType", "MD5");

                    var result = new
                    {
                        prepay_id = content.prepay_id.Value,
                        trade_type = content.trade_type.Value,
                        sign = content.sign.Value,
                        sign1 = jsApiParam.MakeSign(),
                        timestamp = payTimeSamp,
                        nonce_str = content.nonce_str.Value,
                        return_code = content.result_code.Value,
                        return_msg = "",
                    };
                    //string result = "{\"return_code\":\"SUCCESS\",\"sc\":\"123\"}";
                    return Json(result);
                    //return new HttpResponseMessage { Content = new StringContent(result.ToString(), System.Text.Encoding.UTF8, "application/json") };
                }
                else
                {
                    var result = new
                    {
                        return_code = content.return_code.Value,
                        return_msg = content.return_msg.Value,
                    };
                    return Json(result);
                    //return new HttpResponseMessage { Content = new StringContent(result.ToString(), System.Text.Encoding.UTF8, "application/json") };
                }
            }

        }

        /// <summary>
        /// 公共API => 支付结果通用通知
        /// http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=9_7
        /// 微信支付回调,不需要证书 
        /// 
        /// 应用场景 
        /// 支付完成后，微信会把相关支付结果和用户信息发送给商户，商户需要接收处理，并返回应答。 
        /// 对后台通知交互时，如果微信收到商户的应答不是成功或超时，微信认为通知失败，微信会通过一定的策略（如30分钟共8次）定期重新发起通知，尽可能提高通知的成功率，但微信不保证通知最终能成功。 
        /// 由于存在重新发送后台通知的情况，因此同样的通知可能会多次发送给商户系统。商户系统必须能够正确处理重复的通知。 
        /// 推荐的做法是，当收到通知进行处理时，首先检查对应业务数据的状态，判断该通知是否已经处理过，如果没有处理过再进行处理，如果处理过直接返回结果成功。在对业务数据进行状态检查和处理之前，要采用数据锁进行并发控制，以避免函数重入造成的数据混乱。 
        /// 技术人员可登进微信商户后台扫描加入接口报警群。 
        /// </summary>
        /// <returns></returns>
       // [HttpPost]
        public string Notify()
        {
            WeiXinConfig wxconfig = BMAConfig.WeiXinConfig;
            var form = Request.QueryString;
            var sPara = GetRequestPost(form);
            if (sPara.Count <= 0)
            {
                //throw new ArgumentNullException();
                //Content("进来了");
                return "进来了";
            }
            BMALog.Instance.Write("111:" + form.ToString());
            if (sPara["return_code"] == "SUCCESS" && sPara["return_code"] == "SUCCESS")
            {
                var sign_type = sPara["sign_type"];
                var sign = sPara["sign"];
                var signValue = WxPayAPI.Sign(sPara, wxconfig.PartnerKey);
                bool isVerify = sign == signValue;
                if (isVerify)
                {
                    var out_trade_no = sPara["out_trade_no"];

                    //TODO 商户处理订单逻辑： 1.注意交易单不要重复处理；2.注意判断返回金额
                    var cash_fee = BrnMall.Core.Common.Utils.StrToInt(sPara["cash_fee"]);
                    //取得type
                    int strtype = 1;
                    string[] arrtype = out_trade_no.Split('_');
                    try
                    {
                        strtype = BrnMall.Core.Common.Utils.StrToInt(arrtype[1]);
                    }
                    catch
                    {
                        BMALog.Instance.Write("获取Type出错,订单号:" + out_trade_no + " 订单金额:" + cash_fee);
                    }
                    //TODO:postData中携带该次支付的用户相关信息，这将便于商家拿到openid，以便后续提供更好的售后服务，譬如：微信公众好通知用户付款成功。如果不提供服务则可以删除此代码
                    var openid = sPara["openid"];

                    try
                    {
                        BrnMall.Core.BLL.SpHelp.SP_CheckPayOrder(out_trade_no, openid, cash_fee, strtype, 1);
                    }
                    catch (Exception ex)
                    {
                        BMALog.Instance.Write(ex.ToString());
                    }
                    //return Content("success");
                    return "success";
                }
                //return Content("fail");
                return "fail";
            }
            else
            {
                // return Content("fail");
                return "fail";
            }
        }
        /// <summary>
        /// 微信退款完成后的回调
        /// </summary>
        /// <returns></returns>
        public ActionResult Refund()
        {
            WeiXinConfig wxconfig = BMAConfig.WeiXinConfig;
            var requestContent = "";
            using (StreamReader sr = new StreamReader(Request.InputStream))
            {
                requestContent = sr.ReadToEnd();
            }
            Logs.Write(requestContent);
            // LogWriter.Default.WriteInfo(requestContent);//记录请求关键信息到日志中去
            bool isVerify = false;
            var sPara = GetRequestPostByXml(requestContent);
            if (sPara.Count <= 0)
            {
                throw new ArgumentNullException();
            }
            var sign = sPara["sign"];
            var retcode = sPara["retcode"];
            if (retcode != "0")
            {
                isVerify = false;
            }
            else
            {
                var signValue = WxPayAPI.Sign(sPara, wxconfig.PartnerKey);
                isVerify = sign == signValue;
            }
            if (!isVerify)
            {
                return Content("fail");
            }
            //TODO:商户处理订单逻辑

            return Content("success");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetRequestPostByXml(string xmlString)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(xmlString);

            var nodes = document.ChildNodes[1].ChildNodes;

            foreach (System.Xml.XmlNode item in nodes)
            {
                dic.Add(item.Name, item.InnerText);
            }
            return dic;
        }

        /// <summary>
        /// 并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        private SortedDictionary<string, string> GetRequestPost(NameValueCollection form)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();

            // Get names of all forms into a string array.
            String[] requestItem = form.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpUtility.UrlDecode(form[requestItem[i]], Encoding.UTF8));
            }

            return sArray;
        }
        #region 红包
        public ActionResult SendBag()
        {
            if (WorkContext.UserLevel <= 0)
                return PromptView("请先提升您的等级!");
            Core.BLL.SendBag sendBll = new Core.BLL.SendBag();

            if (WebHelper.IsGet())
            {
                int type = WebHelper.GetQueryInt("Sendtype", 1);
                int page = WebHelper.GetQueryInt("page");
                int count = 0;

                List<Core.Model.SendBag> list = sendBll.GetModelList(20, page, " Receiverid=" + WorkContext.Uid + " and status=0 and Sendtype=" + type, "id", out count);
                PageModel pageModel = new PageModel(20, page, count);
                SendBagModel model = new SendBagModel
                {
                    PageModel = pageModel,
                    SendBagList = list,
                    Sendtype = type
                };
                return View(model);
            }
            else
            {
                try
                {
                    lock(this)
                    {
                        WeiXinConfig wxconfig = BMAConfig.WeiXinConfig;
                        int sendid = WebHelper.GetFormInt("sendid");
                        List<Core.Model.SendBag> listsendbag = sendBll.GetModelList(" status=0 and id=" + sendid);
                        if (listsendbag.Count > 0 && listsendbag[0].ReceiverID == WorkContext.Uid )
                        {
                            
                            string Noncestr = "";
                            string paysign = "";
                            try
                            {
                                int bcount = sendBll.GetRecordCount(" status=1 and ReceiverID="+ WorkContext.Uid+ " and sendtype="+WorkContext.UserLevel);
                                if (WorkContext.UserLevel == 1)
                                {
                                    if (bcount >= 3)
                                    {
                                        return AjaxResult("error", "您领取的红包已达到上限,请先提升您的等级!");
                                    }
                                }
                                if (WorkContext.UserLevel == 2)
                                {
                                    if (bcount >= 9)
                                    {
                                        return AjaxResult("error", "您领取的红包已达到上限,请先提升您的等级!");
                                    }
                                }
                                
                                if (listsendbag[0].IsNotSend == 0)
                                {


                                    NormalRedPackResult nop = RedPackApi.SendNormalRedPack(listsendbag[0].Mchbillno, wxconfig.AppID, wxconfig.Mch_id, wxconfig.PartnerKey, Server.MapPath(wxconfig.SSLCERT_PATH), WorkContext.Openid, "创梦星火", wxconfig.Spbill_create_ip, listsendbag[0].Pice.Value * 100,
                                        "更上一层楼", "创梦帮扶活动", "祝您更上一层楼", out Noncestr, out paysign);

                                    if (nop.err_code_des == "")
                                    {
                                        listsendbag[0].Status = 1;
                                        listsendbag[0].SendTime = DateTime.Now;
                                        sendBll.Update(listsendbag[0]);
                                        return AjaxResult("success", "发送成功");
                                    }
                                    else
                                    {
                                        return AjaxResult("error", nop.err_code_des);
                                    }
                                }
                                else
                                {
                                    return AjaxResult("error", "该红包处于冻结状态,暂时无法领取.");
                                }
                            }
                            catch (Exception ex)
                            {
                                return AjaxResult("error", ex.ToString());
                            }

                        }
                        else
                        {
                            return AjaxResult("error", "红包不存在或已发送");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return AjaxResult("error", ex.ToString());
                }
            }
        }

        #endregion
        #region post测试


        public string posttest()
        {
            return BrnMall.Core.Common.HttpHelper.PostUrl("http://localhost:6174/mob/weixinweb/MyParentUpdate", "txtMyParent=1000005", Encoding.UTF8);
        }
        #endregion
        #region 推荐人
        /// <summary>
        /// 我的推荐人
        /// </summary>
        /// <returns></returns>
        public ActionResult MyParent()
        {
            DataTable MyParentdt = BrnMall.Core.BLL.SProcedure.GetAllParentByFun(WorkContext.Uid, 3, " and loclevel>0").Tables[0];
            MyParentModel model = new MyParentModel()
            {
                MyParentDT = MyParentdt
            };
            return View(model);
        }
        public ActionResult MyParentUpdate()
        {
            if (WorkContext.Pid == 3)
            {
                if (WebHelper.IsGet())
                {
                    return View();
                }
                else
                {
                    int paruserno = WebHelper.GetFormInt("txtMyParent");
                    if (paruserno > 0)
                    {
                        BrnMall.Core.BLL.bma_users userbll = new Core.BLL.bma_users();
                        //父级信息
                        List<Core.Model.bma_users> userlist = userbll.GetModelList(" userno=" + paruserno);
                        if (userlist.Count > 0)
                        {
                            if (userlist[0].uid > 3)
                            {
                                try
                                {
                                    Core.BLL.SpHelp.SP_UpdateMyParent(WorkContext.Uid, userlist[0].uid, userlist[0].Layernum.Value + 1);
                                    return AjaxResult("success", "更新推荐人成功");
                                }
                                catch (Exception ex)
                                {
                                    return AjaxResult("success", ex.ToString());
                                }
                            }
                            else
                            {
                                return AjaxResult("erro", "推荐人受限");
                            }
                        }
                        else
                        {
                            return AjaxResult("erro", "输入的推荐人用户编号不存在");
                        }
                    }
                    else
                    {
                        return AjaxResult("erro", "参数错误");
                    }
                    //return AjaxResult("success","修改成功");
                }
            }
            else
            {
                return PromptView("你已有了推荐人,无需修改");
            }
        }
        #endregion
        #region 支付订单
        public ActionResult OrderPay()
        {
            //订单id列表
            string oidList = WebHelper.GetQueryString("oidList");

            string paySystemName = "";
            decimal allSurplusMoney = 0M;
            List<OrderInfo> orderList = new List<OrderInfo>();
            foreach (string oid in StringHelper.SplitString(oidList))
            {
                //订单信息
                OrderInfo orderInfo = Orders.GetOrderByOid(TypeHelper.StringToInt(oid));
                if (orderInfo != null && orderInfo.Uid == WorkContext.Uid && orderInfo.OrderState == (int)OrderState.WaitPaying && orderInfo.PayMode == 1 && (paySystemName.Length == 0 || paySystemName == orderInfo.PaySystemName))
                    orderList.Add(orderInfo);
                else
                    return Redirect(Url.Action("index", "home"));

                paySystemName = orderInfo.PaySystemName;
                allSurplusMoney += orderInfo.SurplusMoney;
            }

            if (orderList.Count < 1 || allSurplusMoney == 0M)
                return Redirect(Url.Action("index", "home"));
            WeiXinConfig wxconfig = BMAConfig.WeiXinConfig;
            var appId = wxconfig.AppID; //WeixinConfig.AppID;
            var nonceStr = Util.CreateNonce_str();
            var timestamp = DateTime.Now.Ticks.ToString().Substring(0, 10);
            var domain = wxconfig.Domain;
            var url = Request.Url.ToString().Replace("#", "");// domain + Request.Url.PathAndQuery;

            var jsTickect = WeiXinHelp.IsExistjsapi_ticket();
            var string1 = "";
            var signature = WeiXinHelp.GetjsSDK_Signature(nonceStr, jsTickect, timestamp, url); //JSAPI.GetSignature(jsTickect, nonceStr, timestamp, url, out string1);
            var userAgent = Request.UserAgent;
            var userVersion = userAgent.Substring(userAgent.LastIndexOf("/") + 1);

            //WeiXinConfig wxconfig = BMAConfig.WeiXinConfig;
            //Logs.Write(form.ToString());
            var out_trade_no = orderList[0].OSN; //Guid.NewGuid().ToString();
            //var domain = wxconfig.Domain;
            var body = "购买创梦星火商城商品";
            var detail = "购买创梦星火商城商品";
            var attach = "购买创梦星火商城商品";
            var fee_type = "CNY";
            var total_fee = Decimal.ToInt32(orderList[0].SurplusMoney)*100;// *100;
            var trade_type = "JSAPI";
            var spbill_create_ip = (trade_type == "APP" || trade_type == "NATIVE") ? Request.UserHostName : wxconfig.Spbill_create_ip;
            var time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
            var time_expire = DateTime.Now.AddDays(7).ToString("yyyyMMddHHmmss");//默认1个小时订单过期，开发者可自定义其他超时机制，原则上微信订单超时时间不超过2小时
            var goods_tag = "商品";
            var notify_url = wxconfig.NOTIFY_URL;//与下面的Notify对应，开发者可自定义其他url地址
            var product_id = orderList[0].Oid.ToString();
            var openid = WorkContext.Openid;
            var partnerKey = wxconfig.PartnerKey;
            var content = WxPayAPI.UnifiedOrder(
                          wxconfig.AppID, wxconfig.Mch_id, wxconfig.Device_info, Util.CreateNonce_str(),
                          body, detail, attach, out_trade_no, fee_type, total_fee, spbill_create_ip, time_start, time_expire,
                          goods_tag, notify_url, trade_type, product_id, openid, partnerKey);
            var prepay_id = "";
            //var trade_type = "";
            var paySign = "";
            //var timestamp = "";
            var nonce_str = "";
            var return_code = "";
            if (content.return_code.Value == "SUCCESS" && content.result_code.Value == "SUCCESS")
            {
                WxPayAPIHelp.WxPayData jsApiParam = new WxPayAPIHelp.WxPayData();
                string payTimeSamp = DateTime.Now.Ticks.ToString().Substring(0, 10);
                jsApiParam.SetValue("appId", wxconfig.AppID);
                jsApiParam.SetValue("timeStamp", payTimeSamp);
                jsApiParam.SetValue("nonceStr", content.nonce_str.Value);
                jsApiParam.SetValue("package", "prepay_id=" + content.prepay_id.Value);
                jsApiParam.SetValue("signType", "MD5");
                //prepay_id = content.prepay_id.Value;
                prepay_id = content.prepay_id.Value;
                trade_type = content.trade_type.Value;
                paySign = jsApiParam.MakeSign();

                timestamp = payTimeSamp;
                nonce_str = content.nonce_str.Value;
                return_code = content.result_code.Value;
            }
            else
            {
               return PromptView("错误编号:"+ content.return_code.Value + "错误信息:"+content.return_msg.Value);
            }
                OrderPayModel model = new OrderPayModel();
            model.OidList = oidList;
            model.AllSurplusMoney = allSurplusMoney;
            model.OrderList = orderList;
            model.PayPlugin = Plugins.GetPayPluginBySystemName(paySystemName);
            model.nonceStr = nonceStr;
            model.jsapiTicket = jsTickect;
            model.nonce_str = nonce_str;
            model.paySign = paySign;
            model.signature = signature;
            model.timestamp = timestamp;
            model.userAgent = userAgent;
            model.userVersion = userVersion;
            model.appId = appId;
            model.prepay_id = prepay_id;
            return View(model);

            //return PromptView("出错了!");
        }
            #endregion
        }
}