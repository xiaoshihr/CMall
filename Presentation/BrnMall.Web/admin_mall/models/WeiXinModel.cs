using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;

namespace BrnMall.Web.MallAdmin.Models
{

    public class SendBagListModel
    {
       public List<Core.Model.SendBag> SBagList { get; set; }
        public PageModel PageModel { get; set; }
        public string UserName { get; set; }
        public int Sentype { get; set; }
        public int PageNumber { get; set; }
        public int Status { get; set; }
        public List<SelectListItem> sentypeList { get; set; }
        public List<SelectListItem> statusList { get; set; }
    }
    public class PayOrderListModel
    {
        public List<Core.Model.PayOrder> PatOrderList { get; set; }
        public PageModel PageModel { get; set; }
        public string UserName { get; set; }
        public int PayType { get; set; }
    }
}
