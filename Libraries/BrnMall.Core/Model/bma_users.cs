using System;
namespace BrnMall.Core.Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Serializable]
    public partial class bma_users
    {
        public bma_users()
        { }
        #region Model
        private int _uid;
        private string _username = "";
        private string _email = "";
        private string _mobile = "";
        private string _password = "";
        private int _userrid = 0;
        private int _storeid = 0;
        private int _mallagid = 0;
        private string _nickname = "";
        private string _avatar = "";
        private int _paycredits = 0;
        private int _rankcredits = 0;
        private int _verifyemail = 0;
        private int _verifymobile = 0;
        private DateTime _liftbantime = Convert.ToDateTime("1900-1-1 00:00:00");
        private string _salt = "";
        private string _openid;
        private int? _pid;
        private int? _userno;
        private int? _userlevel;
        private int? _layernum;
        private int _isreal;
        /// <summary>
        /// 用户id
        /// </summary>
        public int uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 用户等级id
        /// </summary>
        public int userrid
        {
            set { _userrid = value; }
            get { return _userrid; }
        }
        /// <summary>
        /// 店铺id
        /// </summary>
        public int storeid
        {
            set { _storeid = value; }
            get { return _storeid; }
        }
        /// <summary>
        /// 商城管理员组id
        /// </summary>
        public int mallagid
        {
            set { _mallagid = value; }
            get { return _mallagid; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar
        {
            set { _avatar = value; }
            get { return _avatar; }
        }
        /// <summary>
        /// 支付积分
        /// </summary>
        public int paycredits
        {
            set { _paycredits = value; }
            get { return _paycredits; }
        }
        /// <summary>
        /// 等级积分
        /// </summary>
        public int rankcredits
        {
            set { _rankcredits = value; }
            get { return _rankcredits; }
        }
        /// <summary>
        /// 是否验证邮箱
        /// </summary>
        public int verifyemail
        {
            set { _verifyemail = value; }
            get { return _verifyemail; }
        }
        /// <summary>
        /// 是否验证手机
        /// </summary>
        public int verifymobile
        {
            set { _verifymobile = value; }
            get { return _verifymobile; }
        }
        /// <summary>
        /// 解禁时间
        /// </summary>
        public DateTime liftbantime
        {
            set { _liftbantime = value; }
            get { return _liftbantime; }
        }
        /// <summary>
        /// 盐值
        /// </summary>
        public string salt
        {
            set { _salt = value; }
            get { return _salt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Openid
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Userno
        {
            set { _userno = value; }
            get { return _userno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserLevel
        {
            set { _userlevel = value; }
            get { return _userlevel; }
        }
        public int? Layernum
        {
            set { _layernum = value; }
            get { return _layernum; }
        }
        public int IsReal
        {
            set {  _isreal= value; }
            get { return _isreal; }
        }
        #endregion Model

    }
}

