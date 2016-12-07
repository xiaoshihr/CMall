using System;
using System.Data;
using System.Collections.Generic;
using  BrnMall.Core.Common;
using BrnMall.Core.Model;
namespace BrnMall.Core.BLL
{
    /// <summary>
    /// 用户表
    /// </summary>
    public partial class bma_users
    {
        private readonly BrnMall.Core.DAL.bma_users dal = new BrnMall.Core.DAL.bma_users();
        public bma_users()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int uid, string username, string email, string mobile, int userrid, int storeid, int mallagid)
        {
            return dal.Exists(uid, username, email, mobile, userrid, storeid, mallagid);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BrnMall.Core.Model.bma_users model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BrnMall.Core.Model.bma_users model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 更新是否阅读了用户协议
        /// </summary>
        public bool UpdateIsReal(int uid)
        {
            return dal.UpdateIsReal(uid);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int uid)
        {

            return dal.Delete(uid);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int uid, string username, string email, string mobile, int userrid, int storeid, int mallagid)
        {

            return dal.Delete(uid, username, email, mobile, userrid, storeid, mallagid);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string uidlist)
        {
            return dal.DeleteList(uidlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BrnMall.Core.Model.bma_users GetModel(int uid)
        {

            return dal.GetModel(uid);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public BrnMall.Core.Model.bma_users GetModelByCache(int uid)
        {

            string CacheKey = "bma_usersModel-" + uid;
            object objModel =  BrnMall.Core.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(uid);
                    if (objModel != null)
                    {
                        int ModelCache =  BrnMall.Core.Common.ConfigHelper.GetConfigInt("ModelCache");
                         BrnMall.Core.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (BrnMall.Core.Model.bma_users)objModel;
        }

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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<BrnMall.Core.Model.bma_users> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<BrnMall.Core.Model.bma_users> DataTableToList(DataTable dt)
        {
            List<BrnMall.Core.Model.bma_users> modelList = new List<BrnMall.Core.Model.bma_users>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                BrnMall.Core.Model.bma_users model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

        #endregion  ExtensionMethod
    }
}

