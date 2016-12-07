/**  版本信息模板在安装目录下，可自行修改。
* PayOrder.cs
*
* 功 能： N/A
* 类 名： PayOrder
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/4/7 星期四 14:13:25   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace BrnMall.Core.Model
{
	/// <summary>
	/// 用户表
	/// </summary>
	[Serializable]
	public partial class PayOrder
	{
		public PayOrder()
		{}
		#region Model
		private int _id;
        private string _orderno;

        private int? _uid;
		private int? _pice;
		private int? _type;
		private DateTime? _addtime;
		private DateTime? _paytime;
		private int? _paystatus;
        private string _username;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Pice
		{
			set{ _pice=value;}
			get{return _pice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Addtime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Paytime
		{
			set{ _paytime=value;}
			get{return _paytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PayStatus
		{
			set{ _paystatus=value;}
			get{return _paystatus;}
		}
        /// <summary>
		/// 
		/// </summary>
		public string UserName        {
            set { _username = value; }
            get { return _username; }
        }

        #endregion Model

    }
}

