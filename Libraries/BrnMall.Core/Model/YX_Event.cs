using System;
namespace BrnMall.Core.Model
{
	/// <summary>
	/// YX_Event:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YX_Event
	{
		public YX_Event()
		{}
		#region Model
		private int _id;
		private string _eventname;
		private int? _msgtype;
		private string _keyvalue;
		private string _keyworld;
		private string _remark1;
		private string _remark2;
		private string _remark3;
		private int? _flat1=0;
		private int? _flat2=0;
		private string _remark4;
		private string _remark5;
		private string _remark6;
		private int? _flat7;
		private int? _flat8;
		private DateTime? _regtim1;
		private DateTime? _regtim2;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EventName
		{
			set{ _eventname=value;}
			get{return _eventname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? msgType
		{
			set{ _msgtype=value;}
			get{return _msgtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string keyValue
		{
			set{ _keyvalue=value;}
			get{return _keyvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string KeyWorld
		{
			set{ _keyworld=value;}
			get{return _keyworld;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark1
		{
			set{ _remark1=value;}
			get{return _remark1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark2
		{
			set{ _remark2=value;}
			get{return _remark2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark3
		{
			set{ _remark3=value;}
			get{return _remark3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? flat1
		{
			set{ _flat1=value;}
			get{return _flat1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? flat2
		{
			set{ _flat2=value;}
			get{return _flat2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark4
		{
			set{ _remark4=value;}
			get{return _remark4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark5
		{
			set{ _remark5=value;}
			get{return _remark5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark6
		{
			set{ _remark6=value;}
			get{return _remark6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? flat7
		{
			set{ _flat7=value;}
			get{return _flat7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? flat8
		{
			set{ _flat8=value;}
			get{return _flat8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RegTim1
		{
			set{ _regtim1=value;}
			get{return _regtim1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RegTim2
		{
			set{ _regtim2=value;}
			get{return _regtim2;}
		}
		#endregion Model

	}
}

