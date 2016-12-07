
using System;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using BrnMall.Web.MallAdmin.Models;
namespace BrnMall.Web.MallAdmin.Controllers
{
    public class WeiXinController : BaseMallAdminController
    {
        public ActionResult SendBagList(string userName="",int sentype=0,int pageNumber = 1,int status=-1, int pageSize = 15)
        {

            StringBuilder sbwhere = new StringBuilder();
            sbwhere.Append(" 1=1 ");
            if (userName != "")
            {
                sbwhere.AppendFormat(" and ( username like '%{0}%' or userno like '%{0}%') ", userName);
            }
            List<SelectListItem> sentypeList = new List<SelectListItem>();
            sentypeList.Add(new SelectListItem() { Text = "全部", Value = "0" });
            sentypeList.Add(new SelectListItem() { Text = "1级", Value = "1" });
            sentypeList.Add(new SelectListItem() { Text = "2级", Value = "2" });
            sentypeList.Add(new SelectListItem() { Text = "3级", Value = "3" });
            List<SelectListItem> statusList= new List<SelectListItem>();
            statusList.Add(new SelectListItem() { Text = "全部", Value = "-1" });
            statusList.Add(new SelectListItem() { Text = "未领取", Value = "0" });
            statusList.Add(new SelectListItem() { Text = "已领取", Value = "1" });
            if (sentype >0)
            {
                sbwhere.AppendFormat(" and Sendtype={0} ", sentype);
            }
            if (status >= 0)
            {
                sbwhere.AppendFormat(" and status={0} ", status);
            }
            int count = 0;
            Core.BLL.SendBag BagBll = new Core.BLL.SendBag();
            List<Core.Model.SendBag> Baglist = BagBll.GetModelListByS(pageSize, pageNumber, sbwhere.ToString(), "id desc",out count);
            PageModel pageModel = new PageModel(pageSize, pageNumber, count);
            SendBagListModel model = new SendBagListModel()
            {
                SBagList = Baglist,
                PageModel = pageModel,
                Sentype = sentype,
                Status=status,
                PageNumber=pageNumber,
                sentypeList=sentypeList,
                statusList=statusList,
                UserName=userName
            };
            return View(model);
        }
        public ActionResult PayOrderList(string userName = "",int paytype=-1, int pageNumber = 1, int pageSize = 15)
        {
            Core.BLL.PayOrder bllpo = new Core.BLL.PayOrder();
            StringBuilder sbwhere = new StringBuilder();
            sbwhere.Append(" 1=1 ");
            int count = 0;
            if (userName != "")
            {
                sbwhere.AppendFormat(" and ( username like '%{0}%' or userno like '%{0}%') ", userName);
            }
            List<Core.Model.PayOrder> listbllpo = bllpo.GetModelList(pageSize, pageNumber, sbwhere.ToString()," id desc",out count);
            PageModel pageModel = new PageModel(pageSize, pageNumber, count);
            PayOrderListModel model = new PayOrderListModel()
            {
                PageModel = pageModel,
                PatOrderList = listbllpo,
                PayType = paytype,
                UserName = userName
            };
            return View(model);
        } 
    }
}
