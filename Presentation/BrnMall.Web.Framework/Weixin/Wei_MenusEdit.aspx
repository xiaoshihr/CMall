<%@ Page Title="" Language="C#" MasterPageFile="sysSite.Master" AutoEventWireup="true"
    CodeBehind="Wei_MenusEdit.aspx.cs" Inherits="qiaojiaren.Manger.Wei_MenusEdit" %>

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
    <script language="javascript" type="text/javascript">
        function addMenus() {
            var menuName = $("#WX_menuName").val();
            var WX_MenusKey_URL = $("#WX_MenusKey_URL").val();

            if (menuName.trim() == "") {
                swal("请输入菜单名称。");
                $("#WX_menuName").focus();
                return;
            }
            else {
                $("#action").val('action');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="menuId" name="menuId" value="<%=Request.QueryString["id"] %>" type="hidden" />
    <fieldset>
        <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
            <tr class="trrs">
                <td align="left">
                    <span class="spans">菜单名称：</span>
                </td>
                <td>
                    <input id="WX_menuName" name="WX_menuName" type="text" class="txb" value="<%=menuMol.WX_menuName %>" />&nbsp;&nbsp;<span class="help-inline">注：一级菜单最多4个汉字，二级菜单最多8个汉字</span>
                </td>
            </tr>
            <tr class="trrs">
                <td align="left">
                    <span class="spans">菜单类型：</span>
                </td>
                <td>
                    <%=MenuType %>
                </td>
            </tr>
            <tr class="trrs">
                <td align="left">
                    <span class="spans">菜单取值：</span>
                </td>
                <td>
                    <input id="WX_MenusKey_URL" name="WX_MenusKey_URL" type="text" class="txb" value="<%=menuMol.WX_MenusKey_URL %>" />&nbsp;&nbsp;<span class="help-inline">注：菜单的KEY值或者URL值。</span>
                </td>
            </tr>
            <tr class="trrs">
                <td align="left">
                    <span class="spans">父亲菜单：</span>
                </td>
                <td>
                    <%=fatherMenu  %>
                </td>
            </tr>
            <tr class="trrs">
                <td align="left">
                    <span class="spans">菜单排序：</span>
                </td>
                <td>
                    <input id="flat2" name="flat2" type="text" class="txb" value="<%=menuMol.flat2 %>" />&nbsp;&nbsp;<span class="help-inline">注：会根据排序的值的从大到小进行排序。</span>
                </td>
            </tr>
        </table>
        <div class="form-actions clearfix">
            <a href="javascript:;" class="btn button-previous" onclick="window.history.go(-1)"><i
                class="m-icon-swapleft"></i>Back </a>
            <input id="Submit1" type="submit" value="submit" class="btn green button-submit"
                onclick="return addMenus()" /><i class="m-icon-swapright m-icon-white"></i>
        </div>
    </fieldset>
</asp:Content>
