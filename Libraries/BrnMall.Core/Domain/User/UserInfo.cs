using System;

namespace BrnMall.Core
{
    /// <summary>
    /// 部分用户信息类
    /// </summary>
    public class PartUserInfo
    {
        private int _uid;//用户id
        private string _username = "";//用户名称
        private string _email = "";//用户邮箱
        private string _mobile = "";//用户手机
        private string _password = "";//用户密码
        private int _userrid;//用户等级id
        private int _storeid;//店铺id
        private int _mallagid;//商城管理员组id
        private string _nickname = "";//用户昵称
        private string _avatar;//用户头像
        private int _paycredits;//支付积分
        private int _rankcredits;//等级积分
        private int _verifyemail;//是否验证邮箱
        private int _verifymobile;//是否验证手机
        private DateTime _liftbantime = new DateTime(1900, 1, 1);//解禁时间
        private string _salt;//盐值
        private string _openid;//微信对应openid
        private int _userno;//用户编号,自增长
        private int _pid;//父级ID
        private int _userlevel;//用户等级
        private DateTime _addtime;//加入时间
        private int _layernum;//用户等级
        private int _isreal;//用户等级
        /// <summary>
        ///用户id
        /// </summary>
        public int Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        ///用户名称
        /// </summary>
        public string UserName
        {
            set { _username = value.TrimEnd(); }
            get { return _username; }
        }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email
        {
            set { _email = value.TrimEnd(); }
            get { return _email; }
        }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string Mobile
        {
            set { _mobile = value.TrimEnd(); }
            get { return _mobile; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password
        {
            set { _password = value.TrimEnd(); }
            get { return _password; }
        }
        ///<summary>
        ///用户等级id
        ///</summary>
        public int UserRid
        {
            get { return _userrid; }
            set { _userrid = value; }
        }
        /// <summary>
        /// 店铺id
        /// </summary>
        public int StoreId
        {
            get { return _storeid; }
            set { _storeid = value; }
        }
        ///<summary>
        ///商城管理员组id
        ///</summary>
        public int MallAGid
        {
            get { return _mallagid; }
            set { _mallagid = value; }
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName
        {
            set { _nickname = value.TrimEnd(); }
            get { return _nickname; }
        }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value.TrimEnd(); }
        }
        ///<summary>
        ///支付积分
        ///</summary>
        public int PayCredits
        {
            get { return _paycredits; }
            set { _paycredits = value; }
        }
        /// <summary>
        /// 等级积分
        /// </summary>
        public int RankCredits
        {
            get { return _rankcredits; }
            set { _rankcredits = value; }
        }
        /// <summary>
        /// 是否验证邮箱
        /// </summary>
        public int VerifyEmail
        {
            get { return _verifyemail; }
            set { _verifyemail = value; }
        }
        /// <summary>
        /// 是否验证手机
        /// </summary>
        public int VerifyMobile
        {
            get { return _verifymobile; }
            set { _verifymobile = value; }
        }
        /// <summary>
        /// 解禁时间
        /// </summary>
        public DateTime LiftBanTime
        {
            get { return _liftbantime; }
            set { _liftbantime = value; }
        }
        ///<summary>
        ///盐值
        ///</summary>
        public string Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }

        ///<summary>
        ///微信openid
        ///</summary>
        public string Openid
        {
            get { return _openid; }
            set { _openid = value; }
        }
        ///<summary>
        ///父级ID
        ///</summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        ///<summary>
        ///用户编号
        ///</summary>
        public int Userno
        {
            get { return _userno; }
            set { _userno = value; }
        }
        ///<summary>
        ///用户等级
        ///</summary>
        public int UserLevel
        {
            get { return _userlevel; }
            set { _userlevel = value; }
        }
        ///<summary>
        ///添加时间
        ///</summary>
        public DateTime Addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        ///<summary>
        ///层级
        ///</summary>
        public int Layernum
        {
            get { return _layernum; }
            set { _layernum = value; }
        }
        ///<summary>
        ///是否阅读了用户协议
        ///</summary>
        public int IsReal
        {
            get { return _isreal; }
            set { _isreal = value; }
        }
    }

    /// <summary>
    /// 完整用户信息
    /// </summary>
    public class UserInfo : PartUserInfo
    {
        private DateTime _lastvisittime = DateTime.Now;//最后访问时间
        private string _lastvisitip = "";//最后访问ip
        private int _lastvisitrgid = -1;//最后访问区域id
        private DateTime _registertime = DateTime.Now;//用户注册时间
        private string _registerip = "";//用户注册ip
        private int _registerrgid = -1;//用户注册区域id
        private int _gender = 0;//用户性别(0代表未知，1代表男，2代表女)
        private string _realname = "";//用户真实名称
        private DateTime _bday = DateTime.Now;//用户出生日期
        private string _idcard = "";//身份证号
        private int _regionid = 0;//区域id
        private string _address = "";//所在地
        private string _bio = "";//简介

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastVisitTime
        {
            set { _lastvisittime = value; }
            get { return _lastvisittime; }
        }
        /// <summary>
        /// 最后访问ip
        /// </summary>
        public string LastVisitIP
        {
            set { _lastvisitip = value; }
            get { return _lastvisitip; }
        }
        /// <summary>
        /// 最后访问区域id
        /// </summary>
        public int LastVisitRgId
        {
            set { _lastvisitrgid = value; }
            get { return _lastvisitrgid; }
        }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime RegisterTime
        {
            set { _registertime = value; }
            get { return _registertime; }
        }
        /// <summary>
        /// 用户注册ip
        /// </summary>
        public string RegisterIP
        {
            set { _registerip = value; }
            get { return _registerip; }
        }
        /// <summary>
        /// 用户注册区域id
        /// </summary>
        public int RegisterRgId
        {
            set { _registerrgid = value; }
            get { return _registerrgid; }
        }
        ///<summary>
        ///用户性别(0代表未知，1代表男，2代表女)
        ///</summary>
        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        /// <summary>
        /// 用户真实名称
        /// </summary>
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        ///<summary>
        ///用户出生日期
        ///</summary>
        public DateTime Bday
        {
            get { return _bday; }
            set { _bday = value; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard
        {
            set { _idcard = value; }
            get { return _idcard; }
        }
        ///<summary>
        ///区域id
        ///</summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        ///<summary>
        ///所在地
        ///</summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        ///<summary>
        ///简介
        ///</summary>
        public string Bio
        {
            get { return _bio.TrimEnd(); }
            set { _bio = value; }
        }
    }

    /// <summary>
    /// 用户细节信息类
    /// </summary>
    public class UserDetailInfo
    {
        private int _uid;//用户id
        private DateTime _lastvisittime = DateTime.Now;//最后访问时间
        private string _lastvisitip = "";//最后访问ip
        private int _lastvisitrgid = -1;//最后访问区域id
        private DateTime _registertime = DateTime.Now;//用户注册时间
        private string _registerip = "";//用户注册ip
        private int _registerrgid = -1;//用户注册区域id
        private int _gender = 0;//用户性别(0代表未知，1代表男，2代表女)
        private string _realname = "";//用户真实名称
        private DateTime _bday = DateTime.Now;//用户出生日期
        private string _idcard = "";//身份证号
        private int _regionid = 0;//区域id
        private string _address = "";//所在地
        private string _bio = "";//简介

        /// <summary>
        ///用户id
        /// </summary>
        public int Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastVisitTime
        {
            set { _lastvisittime = value; }
            get { return _lastvisittime; }
        }
        /// <summary>
        /// 最后访问ip
        /// </summary>
        public string LastVisitIP
        {
            set { _lastvisitip = value; }
            get { return _lastvisitip; }
        }
        /// <summary>
        /// 最后访问区域id
        /// </summary>
        public int LastVisitRgId
        {
            set { _lastvisitrgid = value; }
            get { return _lastvisitrgid; }
        }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime RegisterTime
        {
            set { _registertime = value; }
            get { return _registertime; }
        }
        /// <summary>
        /// 用户注册ip
        /// </summary>
        public string RegisterIP
        {
            set { _registerip = value; }
            get { return _registerip; }
        }
        /// <summary>
        /// 用户注册区域id
        /// </summary>
        public int RegisterRgId
        {
            set { _registerrgid = value; }
            get { return _registerrgid; }
        }
        ///<summary>
        ///用户性别(0代表未知，1代表男，2代表女)
        ///</summary>
        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        /// <summary>
        /// 用户真实名称
        /// </summary>
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        ///<summary>
        ///用户出生日期
        ///</summary>
        public DateTime Bday
        {
            get { return _bday; }
            set { _bday = value; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard
        {
            set { _idcard = value; }
            get { return _idcard; }
        }
        ///<summary>
        ///区域id
        ///</summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        ///<summary>
        ///所在地
        ///</summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        ///<summary>
        ///简介
        ///</summary>
        public string Bio
        {
            get { return _bio.TrimEnd(); }
            set { _bio = value; }
        }
    }
}
