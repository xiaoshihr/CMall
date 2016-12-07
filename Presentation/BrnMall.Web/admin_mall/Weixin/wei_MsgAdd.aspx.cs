using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace qiaojiaren.Manger
{
    public partial class wei_MsgAdd : basePage
    {

        BrnMall.Core.BLL.YX_Event bll = new BrnMall.Core.BLL.YX_Event();
        BrnMall.Core.Model.YX_Event mol = new BrnMall.Core.Model.YX_Event();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["action"] == "action")
            {
                weixinEventAdd();
            }
        }

        #region 注册微信事件
        /// <summary>
        /// 注册微信事件
        /// </summary>
        protected void weixinEventAdd()
        {
            mol.EventName = Request.Form["EventName"];
            mol.msgType = Convert.ToInt32(Request.Form["msgType"]);
            mol.KeyWorld = Request.Form["KeyWorld"];
            if (bll.Add(mol) != 0)
            {
                CommonMethod.Alert("注册微信事件成功！", "wei_MsgConfig.aspx?menuFid="+Request.Form["fid"]);
            }
            else
            {
                CommonMethod.Alert("系统正忙，请稍后重试！");
            }
        }
        #endregion
    }
}