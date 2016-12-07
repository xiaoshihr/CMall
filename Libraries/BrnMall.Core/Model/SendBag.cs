/**  版本信息模板在安装目录下，可自行修改。
* SendBag.cs
*
* 功 能： N/A
* 类 名： SendBag
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
	public partial class SendBag
	{
		public SendBag()
		{}
		#region Model
		private int _id;
        private string _mchbillno;
        private int? _pice;
		private int? _senderid;
		private int? _receiverid;
		private int? _sendtype;
		private int? _status;
        private string _noncestr;
        private string _paysign;
        private DateTime? _addtime;
		private DateTime? _sendtime;
        private Model.bma_users _modeluser;
        
        private int _isnotsend;
        private int _oid;
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
		public string Mchbillno
        {
            set { _mchbillno = value; }
            get { return _mchbillno; }
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
		public int? SenderID
		{
			set{ _senderid=value;}
			get{return _senderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReceiverID
		{
			set{ _receiverid=value;}
			get{return _receiverid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SendType
		{
			set{ _sendtype=value;}
			get{return _sendtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
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
		public DateTime? SendTime
		{
			set{ _sendtime=value;}
			get{return _sendtime;}
		}
        /// <summary>
		/// 
		/// </summary>
		public string Noncestr
        {
            set { _noncestr = value; }
            get { return _noncestr; }
        }
        /// <summary>
		/// 
		/// </summary>
		public string PaySign
        {
            set { _paysign = value; }
            get { return _paysign; }
        }
        /// <summary>
		/// 
		/// </summary>
		public int IsNotSend
        {
            set { _isnotsend = value; }
            get { return _isnotsend; }
        }
        /// <summary>
		/// 
		/// </summary>
		public int OID
        {
            set { _oid = value; }
            get { return _oid; }
        }
        /// <summary>
		/// 
		/// </summary>
		public Model.bma_users ModelUser
        {
            set { _modeluser = value; }
            get { return _modeluser; }
        }
       
        #endregion Model

    }
}

