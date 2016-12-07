using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Deepleo.Weixin.SDK.Helpers;
using System.Collections.Specialized;
//using Deepleo.Web.Services;
using BrnMall.Services;
using Deepleo.Weixin.SDK.Pay;
using System.IO;
using Codeplex.Data;
using BrnMall.Services;
using BrnMall.Core;

using BrnMall.Services;
using BrnMall.Web.Framework;
using BrnMall.Web.Mobile.Models;

namespace BrnMall.Web.Mobile.Controllers
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WXPayController : Controller
    {
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
        public JsonResult Index()
        {
            var form = Request.Form;
            var sPara = GetRequestPost(form);
            WeiXinConfig wxconfig = BMAConfig.WeiXinConfig;
            if (sPara.Count <= 0)
            {
                throw new ArgumentNullException();
            }
            //LogWriter.Default.WriteInfo(form.ToString());//记录请求关键信息到日志中去
            Logs.Write(form.ToString());
            var out_trade_no = BrnMall.Core.Common.Utils.GenerateOutTradeNo(); //Guid.NewGuid().ToString();
            var domain = wxconfig.Domain;
            var body = sPara["body"];
            var detail = sPara["detail"];
            var attach = sPara["attach"];
            var fee_type = "CNY";
            var total_fee = int.Parse(sPara["total_fee"])*100;
            var trade_type = sPara["trade_type"];
            var spbill_create_ip = (trade_type == "APP" || trade_type == "NATIVE") ? Request.UserHostName : wxconfig.Spbill_create_ip;
            var time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
            var time_expire = DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss");//默认1个小时订单过期，开发者可自定义其他超时机制，原则上微信订单超时时间不超过2小时
            var goods_tag = sPara["goods_tag"];
            var notify_url = string.Format("{0}/mobile/WXPay/Notify", domain);//与下面的Notify对应，开发者可自定义其他url地址
            var product_id = sPara["product_id"];
            var openid = sPara["openid"];
            var partnerKey = wxconfig.PartnerKey;
            var content = WxPayAPI.UnifiedOrder(
                          wxconfig.AppID, wxconfig.Mch_id, wxconfig.Device_info, Util.CreateNonce_str(),
                          body, detail, attach, out_trade_no, fee_type, total_fee, spbill_create_ip, time_start, time_expire,
                          goods_tag, notify_url, trade_type, product_id, openid, partnerKey);

            if (content.return_code.Value == "SUCCESS" && content.result_code.Value == "SUCCESS")
            {
                var result = new
                {
                    prepay_id = content.prepay_id.Value,
                    trade_type = content.trade_type.Value,
                    sign = content.sign.Value,
                    nonce_str = content.nonce_str.Value,
                    return_code = content.result_code.Value,
                    return_msg = "",
                };
                return Json(result);
            }
            else
            {
                var result = new
                {
                    return_code = content.return_code.Value,
                    return_msg = content.return_msg.Value,
                };
                return Json(result);
            }

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
                var total_fee = int.Parse(sPara["total_fee"])*100;
                var trade_type = sPara["trade_type"];
                var spbill_create_ip = (trade_type == "APP" || trade_type == "NATIVE") ? Request.UserHostName : wxconfig.Spbill_create_ip;
                var time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
                var time_expire = DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss");//默认1个小时订单过期，开发者可自定义其他超时机制，原则上微信订单超时时间不超过2小时
                var goods_tag = sPara["goods_tag"];
                var notify_url = wxconfig.NOTIFY_URL;//string.Format("{0}/mob/WXPay/Notify", domain);//与下面的Notify对应，开发者可自定义其他url地址
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
        public string Notify()
        {

            WxPayAPIHelp.Notify not = new WxPayAPIHelp.Notify();
            WxPayAPIHelp.WxPayData notifyData = not.GetNotifyData();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayAPIHelp.WxPayData res = new WxPayAPIHelp.WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                BMALog.Instance.Write("The Pay result is error : " + res.ToXml());
                return res.ToXml();
                Response.Write(res.ToXml());
                Response.End();
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayAPIHelp.WxPayData res = new WxPayAPIHelp.WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                BMALog.Instance.Write("Order query failure : " + res.ToXml());
                return res.ToXml();
                Response.Write(res.ToXml());
                Response.End();
            }
            //查询订单成功
            else
            {
                WxPayAPIHelp.WxPayData res = new WxPayAPIHelp.WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                res.SetValue("out_trade_no", notifyData.GetValue("out_trade_no"));
                BMALog.Instance.Write("order query success : " + res.ToXml());
                try
                {
                    
                    var out_trade_no = notifyData.GetValue("out_trade_no");

                    //TODO 商户处理订单逻辑： 1.注意交易单不要重复处理；2.注意判断返回金额
                    var cash_fee = BrnMall.Core.Common.Utils.StrToInt(notifyData.GetValue("cash_fee"))/100;
                    //取得type
                    int strtype = 1;
                    string[] arrtype = out_trade_no.ToString().Split('_');
                    try
                    {
                        strtype = BrnMall.Core.Common.Utils.StrToInt(arrtype[1]);
                    }
                    catch
                    {
                        BMALog.Instance.Write("获取Type出错,订单号:" + out_trade_no + " 订单金额:" + cash_fee);
                    }
                    //TODO:postData中携带该次支付的用户相关信息，这将便于商家拿到openid，以便后续提供更好的售后服务，譬如：微信公众好通知用户付款成功。如果不提供服务则可以删除此代码
                    var openid = notifyData.GetValue("openid");

                    try
                    {
                        BrnMall.Core.BLL.SpHelp.SP_CheckPayOrder(out_trade_no.ToString(), openid.ToString(), cash_fee, strtype, 1);
                    }
                    catch (Exception ex)
                    {
                        BMALog.Instance.Write(ex.ToString());
                    }

                    // }

                }
                catch (Exception ex)
                {
                    BMALog.Instance.Write("发生错误 : " + ex.ToString());
                }

                return res.ToXml();
                Response.Write(res.ToXml());
                Response.End();
            }

        }
        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayAPIHelp.WxPayData req = new WxPayAPIHelp.WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayAPIHelp.WxPayData res = WxPayAPIHelp.WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
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
        //public string Notify()
        //{
        //    WeiXinConfig wxconfig = BMAConfig.WeiXinConfig;
        //    //var form = Request.QueryString;
        //    //var sPara = GetRequestPost(form);
        //    var requestContent = "";
        //    using (StreamReader sr = new StreamReader(Request.InputStream))
        //    {
        //        requestContent = sr.ReadToEnd();
        //    }
        //    BMALog.Instance.Write(requestContent);//记录请求关键信息到日志中去
        //    //bool isVerify = false;
        //    var sPara = GetRequestPostByXml(requestContent);
        //    BMALog.Instance.Write("有进来过");
        //    if (sPara.Count <= 0)
        //    {
        //        //throw new ArgumentNullException();
        //        //Content("进来了");
        //        return "进来了";
        //    }
        //    BMALog.Instance.Write("111:" + requestContent);
        //    if (sPara["return_code"] == "SUCCESS" && sPara["return_code"] == "SUCCESS")
        //    {
        //        var sign_type = sPara["sign_type"];
        //        var sign = sPara["sign"];
        //        var signValue = WxPayAPI.Sign(sPara, wxconfig.PartnerKey);
        //        bool isVerify = sign == signValue;
        //        BMALog.Instance.Write("isVerify:" + isVerify + " 返回签名:" + sign + " 本地签名:" + signValue);
        //        //if (isVerify)
        //        //{
        //            var out_trade_no = sPara["out_trade_no"];

        //            //TODO 商户处理订单逻辑： 1.注意交易单不要重复处理；2.注意判断返回金额
        //            var cash_fee = BrnMall.Core.Common.Utils.StrToInt(sPara["cash_fee"]);
        //            //取得type
        //            int strtype = 1;
        //            string[] arrtype = out_trade_no.Split('_');
        //            try
        //            {
        //                strtype = BrnMall.Core.Common.Utils.StrToInt(arrtype[1]);
        //            }
        //            catch
        //            {
        //                BMALog.Instance.Write("获取Type出错,订单号:" + out_trade_no + " 订单金额:" + cash_fee);
        //            }
        //            //TODO:postData中携带该次支付的用户相关信息，这将便于商家拿到openid，以便后续提供更好的售后服务，譬如：微信公众好通知用户付款成功。如果不提供服务则可以删除此代码
        //            var openid = sPara["openid"];

        //            try
        //            {
        //                BrnMall.Core.BLL.SpHelp.SP_CheckPayOrder(out_trade_no, openid, cash_fee, strtype, 1);
        //            }
        //            catch (Exception ex)
        //            {
        //                BMALog.Instance.Write(ex.ToString());
        //            }
        //            //return Content("success");
        //            return "success";
        //       // }
        //        //return Content("fail");
        //        return "fail";
        //    }
        //    else
        //    {
        //        // return Content("fail");
        //        return "fail";
        //    }
        //}

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
        #region 订单支付
        public ActionResult OrderPay()
        {

            return View();
        }
        #endregion
    }
}
