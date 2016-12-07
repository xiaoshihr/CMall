using System;
using System.Collections.Generic;
using System.Web;

namespace BrnMall.WxPayAPI
{
    /**
    * 	配置账号信息
    */
    public class WxPayConfig
    {
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        */
        //public const string APPID = "wx290ac9e3ffa0b55b";
        //public const string MCHID = "1233410002";
        //public const string KEY = "zxzfbcw12398klem9wnhg9dkk03kjhwe";
        //public const string APPSECRET = "bd6b9bf651985eddcc1a5edf8fd76102";
        //public const string APPID = "wx290ac9e3ffa0b55b";
        //public const string MCHID = "1300538501";
        //public const string KEY = "zxzfbcw12398klem9wnhg9dkk03kjhwe";
        //public const string APPSECRET = "bd6b9bf651985eddcc1a5edf8fd76102";
        //public const string APPID = "wxf065e23821e5194d";
        //public const string MCHID = "1218320501";
        //public const string KEY = "​0d05b3f3b843bac29d7fae446b1764d4";
        ////public const string KEY = "​";
        //public const string APPSECRET = "e54db10dd7d58ebad1e6e58d82fd47ee";
        //公众号【帝峰茶业官网】

        //公众平台登录账号：chinadfcy 密码：difengtea



        //微信支付功能信息

        //公众号：帝峰茶业官网  微信支付功能信息

        //商户号(PartnerID)：​1218320501

        //商户名称：​福建帝峰生态茶业发展有限公司

        //登录密码：​111111

        //初始密钥(PartnerKey)：​0d05b3f3b843bac29d7fae446b1764d4

        //Appid：​wxf065e23821e5194d

        //AppSecret(应用密钥)：e54db10dd7d58ebad1e6e58d82fd47ee


        public const string APPID = "wxf065e23821e5194d";
        public const string MCHID = "1218320501";
        //public const string KEY = "difengchayedifengchayedifengchay";
        public const string KEY = "difengchayedifengchaye0123456789";
        public const string APPSECRET = "e54db10dd7d58ebad1e6e58d82fd47ee";
        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public const string SSLCERT_PATH = "cert/apiclient_cert.p12";
        public const string SSLCERT_PASSWORD = "1233410002";



        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        //public const string NOTIFY_URL = "http://paysdk.weixin.qq.com/example/ResultNotifyPage.aspx";
        public const string NOTIFY_URL = "http://g.zczbw.net/pay/ResultNotifyPage.aspx";//http://g.zczbw.net/pay/ResultNotifyPage.aspx
        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public const string IP = "8.8.8.8";


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public const string PROXY_URL = "http://10.152.18.220:8080";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public const int REPORT_LEVENL = 1;

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public const int LOG_LEVENL = 3;
    }
}