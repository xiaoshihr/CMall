using System;
using System.Data;
using System.Collections.Generic;

using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;


namespace BrnMall.Web.Mobile.Models
{
     public class WeiXinWebModel
    {
     }

    #region 微信
    /// <summary>
    /// 二维码
    /// </summary>
    public class WxQRCodeModel
    {
        public string picPth { get; set; }
    }
    /// <summary>
    /// 成为代理商
    /// </summary>
    public class UserUpdateModel
    {
        public string appId { set; get; }
        public string timestamp { set; get; }
        public string nonceStr { set; get; }
        public string jsapiTicket { set; get; }
        public string signature { set; get; }
        public string string1 { set; get; }

        public string userAgent { set; get; }
        public string userVersion { set; get; }

    }
    /// <summary>
    /// 成为代理商
    /// </summary>
    public class SendBagModel
    {
        public List<Core.Model.SendBag> SendBagList { set; get; }
        public int Sendtype { set; get; }
        public PageModel PageModel { set; get; }

    }
    /// <summary>
    /// 我的推荐人
    /// </summary>
    public class MyParentModel
    {
        // public List<Core.Model.bma_users> MyParentList { set; get; }
        public DataTable MyParentDT { get; set; }
    }
    /// <summary>
    /// 我的推荐人
    /// </summary>
    public class MyParentUpdateModel
    {
        // public List<Core.Model.bma_users> MyParentList { set; get; }
        //public DataTable MyParentDT { get; set; }
    }
    //#endregion
    /// <summary>
    /// 订单支付
    /// </summary>
    public class OrderPayModel
    {

        
        
        public string OidList { get; set; }
        public decimal AllSurplusMoney { get; set; }
        public List<OrderInfo> OrderList { get; set; }
        public PluginInfo PayPlugin { get; set; }
        public string appId { set; get; }
        public string timestamp { set; get; }
        public string nonceStr { set; get; }
        public string jsapiTicket { set; get; }
        public string signature { set; get; }
        public string string1 { set; get; }

        public string userAgent { set; get; }
        public string userVersion { set; get; }

        public string prepay_id { get; set; }
        public string paySign { get; set; }
        public string nonce_str { get; set; }
        
    }
    #endregion
}
