using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


using BrnMall.Core;
using BrnMall.Web.Framework;
namespace BrnMall.Web.Mobile.Models
{
    /// <summary>
    /// 登陆模型类
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 影子账号名
        /// </summary>
        public string ShadowName { get; set; }
        /// <summary>
        /// 是否允许记住用户
        /// </summary>
        public bool IsRemember { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
        /// <summary>
        /// 开放授权插件
        /// </summary>
        public List<PluginInfo> OAuthPluginList { get; set; }
    }

    /// <summary>
    /// 注册模型类
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 影子账号名
        /// </summary>
        public string ShadowName { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }

    /// <summary>
    /// 找回密码模型类
    /// </summary>
    public class FindPwdModel
    {
        /// <summary>
        /// 影子账号名
        /// </summary>
        public string ShadowName { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }

    /// <summary>
    /// 选择找回密码方式模型类
    /// </summary>
    public class SelectFindPwdTypeModel
    {
        public PartUserInfo PartUserInfo { get; set; }
    }

    /// <summary>
    /// 重置密码模型类
    /// </summary>
    public class ResetPwdModel
    {
        public string V { get; set; }
    }
    /// <summary>
    /// 编辑店铺模型类
    /// </summary>
    public class RegStoreModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        
        public string StoreName { get; set; }
        /// <summary>
        /// 区域id
        /// </summary>
       
        public int RegionId { get; set; }
        /// <summary>
        /// 等级id
        /// </summary>
      
        public int StoreRid { get; set; }
        /// <summary>
        /// 行业id
        /// </summary>
       
        public int StoreIid { get; set; }
        /// <summary>
        /// logo
        /// </summary>
        
        public string Logo { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        
        public string Mobile { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
       
        public string Phone { get; set; }
        /// <summary>
        /// qq
        /// </summary>
        
        public string QQ { get; set; }
        /// <summary>
        /// 阿里旺旺
        /// </summary>
        
        public string WW { get; set; }
        /// <summary>
        /// 状态(0代表营业,1代表关闭)
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 状态截止时间
        /// </summary>
        public string StateEndTime { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// Banner
        /// </summary>
       
        public string Banner { get; set; }
        /// <summary>
        /// 公告
        /// </summary>
       
        public string Announcement { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        
        public string Description { get; set; }

        
    }
    /// <summary>
    /// 店长模型类
    /// </summary>
    public class StoreKeeperModel
    {
        /// <summary>
        /// 店长类型(0代表个人,1代表公司)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 店长名称
        /// </summary>
        
        public string StoreKeeperName { get; set; }
        /// <summary>
        /// 标识号
        /// </summary>
        
        public string IdCard { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        
        public string Address { get; set; }
    }
}