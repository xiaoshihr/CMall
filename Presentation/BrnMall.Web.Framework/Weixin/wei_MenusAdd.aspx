<%@ Page Title="" Language="C#" MasterPageFile="sysSite.Master" AutoEventWireup="true"
    CodeBehind="wei_MenusAdd.aspx.cs" Inherits="qiaojiaren.Manger.wei_MenusAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .required
        {
            color: red;
        }
        .txb
        {
            width: 248px;
        }
        .txb2
        {
            width: 262px;
        }
    </style>
    <script src="/administration/Manger/js/jquery-1.7.2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function addMenus() {
            var fid = $("#fid").val();//导航
            var menuName = $("#WX_menuName").val();
            var WX_Fid = $("#WX_Fid").val();
            var WX_MenuType = $("#WX_MenuType").val();
            var WX_MenusKey_URL = $("#WX_MenusKey_URL").val();
            var flat2 = $("#flat2").val();
            if (menuName.trim() == "") {
                swal("请输入菜单名称。");
                $("#WX_menuName").focus();
                return ;
            }
            else {
                var bat = { "WX_menuName": menuName, "WX_Fid": WX_Fid, "WX_MenuType": WX_MenuType, "WX_MenusKey_URL": WX_MenusKey_URL, "flat2": flat2 };
                $.post("wei_MenusAdd.aspx?action=action", bat, function (data) {
                    if (data == "NO") {
                        swal("微信主菜单最多为三个！");
                        return;
                    }
                    else if (data == "NOO") {
                        swal("主菜单的子菜单最多五个！");
                        return;
                    }
                    else if (data == "error") {
                        swal("系统正忙，请稍后重试！");
                        return;
                    }
                    else {
                        alert("操作已成功！");
                        window.location.href = "wei_MenusAdd.aspx?menuFid="+fid;
                    }
                })
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="tab-content">
                                <fieldset>
                                    <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
                                        <tr >
                                            <td align="left">
                                                <span class="spans">菜单名称：</span>
                                            </td>
                                            <td>
                                                <input id="WX_menuName" name="WX_menuName" type="text" class="txb" />&nbsp;&nbsp;<span class="help-inline">注：一级菜单最多4个汉字，二级菜单最多8个汉字</span>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td align="left">
                                                <span class="spans">菜单类型：</span>
                                            </td>
                                            <td>
                                                <select id="WX_MenuType" name="WX_MenuType" class="txb2">
                                                    <option value="0">--请选择--</option>
                                                    <option value="1">单击类型</option>
                                                    <option value="2">链接类型</option>
                                                   
                                                </select>&nbsp;&nbsp;<span class="help-inline">注：请根据需求，认真选择，谢谢...</span>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td align="left">
                                                <span class="spans">菜单取值：</span>
                                            </td>
                                            <td>
                                                <input id="WX_MenusKey_URL" name="WX_MenusKey_URL" type="text" class="txb" />&nbsp;&nbsp;<span class="help-inline">注：请填写(除主菜单外，不允许为空！)大写字母（不允许包含小写字符）。</span>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td align="left">
                                                <span class="spans">父亲菜单：</span>
                                            </td>
                                            <td>
                                                <%=SelCon %>&nbsp;&nbsp;<span class="help-inline">注：不选择，默认为一级菜单！一级菜单最多3个，二级菜单最多5个，请勿超出数量限制</span>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td align="left">
                                                <span class="spans">菜单排序：</span>
                                            </td>
                                            <td>
                                                <input id="flat2" name="flat2" type="text" class="txb" value="0" />&nbsp;&nbsp;<span class="help-inline">注：会根据排序的值的从大到小进行排序。</span>
                                            </td>
                                        </tr>
                                    </table>
                                   
                                </fieldset>
                            </div>
                            <div class="form-actions clearfix">
                                <a href="javascript:;" class="btn button-previous" onclick="window.history.go(-1)"><i
                                    class="m-icon-swapleft"></i>Back </a>
                                <input id="button1" type="button" value="submit" class="btn green button-submit"
                                    onclick="addMenus()" /><i class="m-icon-swapright m-icon-white"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
