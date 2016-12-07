using System;
using System.Data;
using System.Collections.Generic;

using BrnMall.Core.Model;
namespace BrnMall.Core.BLL
{
	/// <summary>
	/// YX_weiXinMenus
	/// </summary>
	public partial class YX_weiXinMenus
	{
		private readonly BrnMall.Core.DAL.YX_weiXinMenus dal=new BrnMall.Core.DAL.YX_weiXinMenus();
		public YX_weiXinMenus()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(BrnMall.Core.Model.YX_weiXinMenus model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(BrnMall.Core.Model.YX_weiXinMenus model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			return dal.Delete(Id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			return dal.DeleteList(Idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BrnMall.Core.Model.YX_weiXinMenus GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
	

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<BrnMall.Core.Model.YX_weiXinMenus> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<BrnMall.Core.Model.YX_weiXinMenus> DataTableToList(DataTable dt)
		{
			List<BrnMall.Core.Model.YX_weiXinMenus> modelList = new List<BrnMall.Core.Model.YX_weiXinMenus>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BrnMall.Core.Model.YX_weiXinMenus model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new BrnMall.Core.Model.YX_weiXinMenus();
					if(dt.Rows[n]["Id"]!=null && dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["WX_menuName"]!=null && dt.Rows[n]["WX_menuName"].ToString()!="")
					{
					model.WX_menuName=dt.Rows[n]["WX_menuName"].ToString();
					}
					if(dt.Rows[n]["WX_MenuType"]!=null && dt.Rows[n]["WX_MenuType"].ToString()!="")
					{
					model.WX_MenuType=dt.Rows[n]["WX_MenuType"].ToString();
					}
					if(dt.Rows[n]["WX_MenusKey_URL"]!=null && dt.Rows[n]["WX_MenusKey_URL"].ToString()!="")
					{
					model.WX_MenusKey_URL=dt.Rows[n]["WX_MenusKey_URL"].ToString();
					}
					if(dt.Rows[n]["WX_Fid"]!=null && dt.Rows[n]["WX_Fid"].ToString()!="")
					{
						model.WX_Fid=int.Parse(dt.Rows[n]["WX_Fid"].ToString());
					}
					if(dt.Rows[n]["WX_AddTime"]!=null && dt.Rows[n]["WX_AddTime"].ToString()!="")
					{
						model.WX_AddTime=DateTime.Parse(dt.Rows[n]["WX_AddTime"].ToString());
					}
					if(dt.Rows[n]["remark1"]!=null && dt.Rows[n]["remark1"].ToString()!="")
					{
					model.remark1=dt.Rows[n]["remark1"].ToString();
					}
					if(dt.Rows[n]["remark2"]!=null && dt.Rows[n]["remark2"].ToString()!="")
					{
					model.remark2=dt.Rows[n]["remark2"].ToString();
					}
					if(dt.Rows[n]["remark3"]!=null && dt.Rows[n]["remark3"].ToString()!="")
					{
					model.remark3=dt.Rows[n]["remark3"].ToString();
					}
					if(dt.Rows[n]["flat1"]!=null && dt.Rows[n]["flat1"].ToString()!="")
					{
						model.flat1=int.Parse(dt.Rows[n]["flat1"].ToString());
					}
					if(dt.Rows[n]["flat2"]!=null && dt.Rows[n]["flat2"].ToString()!="")
					{
						model.flat2=int.Parse(dt.Rows[n]["flat2"].ToString());
					}
					if(dt.Rows[n]["remark4"]!=null && dt.Rows[n]["remark4"].ToString()!="")
					{
					model.remark4=dt.Rows[n]["remark4"].ToString();
					}
					if(dt.Rows[n]["remark5"]!=null && dt.Rows[n]["remark5"].ToString()!="")
					{
					model.remark5=dt.Rows[n]["remark5"].ToString();
					}
					if(dt.Rows[n]["remark6"]!=null && dt.Rows[n]["remark6"].ToString()!="")
					{
					model.remark6=dt.Rows[n]["remark6"].ToString();
					}
					if(dt.Rows[n]["flat7"]!=null && dt.Rows[n]["flat7"].ToString()!="")
					{
						model.flat7=int.Parse(dt.Rows[n]["flat7"].ToString());
					}
					if(dt.Rows[n]["flat8"]!=null && dt.Rows[n]["flat8"].ToString()!="")
					{
						model.flat8=int.Parse(dt.Rows[n]["flat8"].ToString());
					}
					if(dt.Rows[n]["RegTim1"]!=null && dt.Rows[n]["RegTim1"].ToString()!="")
					{
						model.RegTim1=DateTime.Parse(dt.Rows[n]["RegTim1"].ToString());
					}
					if(dt.Rows[n]["RegTim2"]!=null && dt.Rows[n]["RegTim2"].ToString()!="")
					{
						model.RegTim2=DateTime.Parse(dt.Rows[n]["RegTim2"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

