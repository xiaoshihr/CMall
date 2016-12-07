using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrnMall.Core
{
    [Serializable]
    public class WeiXinConfig : IConfigInfo
    {
        /// <summary>
        /// 微信相关配置
        /// </summary>
        
        private string _domain;//网站域名，必须80端口，不要以/结尾
        private string _token;//微信Token

        private string _encodingaeskey;//微信消息体加密对应的EncodingAESKey
        private string _appid;//
        private string _appsecret;//微信AppSecret
        private string _partnerkey;//用于微信支付的PartnerKey
        private string _mch_id;//用于微信支付的商户号
        private string _device_info;//用于微信支付的设备号
        private string _spbill_create_ip;//用于微信支付的服务端IP地址
        private string _sslcert_path;//证书路径,注意应该填写绝对路径
        private string _sslcert_password;//证书密码
        private string _notify_url;//支付回调URL
        /// <summary>
        /// 网站域名，必须80端口，不要以/结尾
        /// </summary>
        public string Domain
        {
            get { return _domain; }
            set { _domain = value; }
        }
        /// <summary>
        /// 微信Token
        /// </summary>
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
        ///<summary>
        ///微信消息体加密对应的EncodingAESKey
        /// </summary>
        public string EncodingAESKey
        {
            get { return _encodingaeskey; }
            set { _encodingaeskey = value; }
        }
        ///<summary>
        ///微信AppId
        /// </summary>
        public string AppID
        {
            get { return _appid; }
            set { _appid = value; }
        }

        /////<summary>
        /////微信AppId
        ///// </summary>
        //public string EncodingAESKey
        //{
        //    get { return _encodingaeskey; }
        //    set { _encodingaeskey = value; }
        //}
        ///<summary>
        ///微信AppSecret
        /// </summary>
        public string AppSecret
        {
            get { return _appsecret; }
            set { _appsecret = value; }
        }
        ///<summary>
        ///用于微信支付的PartnerKey
        /// </summary>
        public string PartnerKey
        {
            get { return _partnerkey; }
            set { _partnerkey = value; }
        }
        ///<summary>
        ///用于微信支付的商户号
        /// </summary>
        public string Mch_id
        {
            get { return _mch_id; }
            set { _mch_id = value; }
        }
        ///<summary>
        ///-用于微信支付的设备号
        /// </summary>
        public string Device_info
        {
            get { return _device_info; }
            set { _device_info = value; }
        }
        ///<summary>
        ///用于微信支付的服务端IP地址
        /// </summary>
        public string Spbill_create_ip
        {
            get { return _spbill_create_ip; }
            set { _spbill_create_ip = value; }
        }
        ///<summary>
        ///证书路径,注意应该填写绝对路径
        /// </summary>
        public string SSLCERT_PATH
        {
            get { return _sslcert_path; }
            set { _sslcert_path = value; }
        }
        ///<summary>
        ///证书密码
        /// </summary>
        public string Sslcert_Password
        {
            get { return _sslcert_password; }
            set { _sslcert_password = value; }
        }
        ///<summary>
        ///微信支付回调地址
        /// </summary>
        public string NOTIFY_URL
        {
            get { return _notify_url; }
            set { _notify_url = value; }
        }
    }
}
