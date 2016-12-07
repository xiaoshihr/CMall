using System;
namespace BrnMall.Core.Model
{
	/// <summary>
	/// YX_Menus:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YX_Menus
	{
		public YX_Menus()
		{}
		#region Model
		private int _id;
		private string _menuname;
		private string _menupth;
		private int? _fid;
		private string _rightid;
		private DateTime? _addtime;
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
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string menuName
		{
			set{ _menuname=value;}
			get{return _menuname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string menuPth
		{
			set{ _menupth=value;}
			get{return _menupth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FID
		{
			set{ _fid=value;}
			get{return _fid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string rightID
		{
			set{ _rightid=value;}
			get{return _rightid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
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

