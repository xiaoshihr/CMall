using System;
using System.Data;
using System.Collections.Generic;

using BrnMall.Core.Model;
namespace BrnMall.Core.BLL
{
	/// <summary>
	/// YX_sysConfigs
	/// </summary>
	public partial class YX_sysConfigs
	{
		private readonly BrnMall.Core.DAL.YX_sysConfigs dal=new BrnMall.Core.DAL.YX_sysConfigs();
		public YX_sysConfigs()
		{}
		#region  BasicMethod

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
		public int  Add(BrnMall.Core.Model.YX_sysConfigs model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(BrnMall.Core.Model.YX_sysConfigs model)
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
		public BrnMall.Core.Model.YX_sysConfigs GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
	

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
		public List<BrnMall.Core.Model.YX_sysConfigs> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<BrnMall.Core.Model.YX_sysConfigs> DataTableToList(DataTable dt)
		{
			List<BrnMall.Core.Model.YX_sysConfigs> modelList = new List<BrnMall.Core.Model.YX_sysConfigs>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BrnMall.Core.Model.YX_sysConfigs model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
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

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 根据键，获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetKeyValue(string key)
        {
            return dal.GetKeyValue(key);
        }

        /// <summary>
        /// // 是否存在该记录 不存在  则插入配置   存在则更新配置
        /// </summary>
        /// <param name="wxkey"></param>
        /// <returns></returns>
        public void isExists(string wxkey, string wxkeyvalue)
        {

            dal.isExists(wxkey, wxkeyvalue);
        }
		#endregion  ExtensionMethod
	}
}

