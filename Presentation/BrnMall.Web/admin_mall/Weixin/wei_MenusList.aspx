<%@ Page Title="" Language="C#" MasterPageFile="sysSite.Master" AutoEventWireup="true"
    CodeBehind="wei_MenusList.aspx.cs" Inherits="qiaojiaren.Manger.wei_MenusList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function del(o) {
            if (confirm("此操作将删除所有以本目录为父目录的所有子目录！请慎重操作！是否继续？")) {
                var id = o.id;
                var fid = $("#fid").val();
                id = id.substr(3);
                $.get("wei_MenusList.aspx?action=del&id=" + id + "&da=" + new Date(), function (data) {
                    if (data == "OK") {
                        alert("删除成功！");
                        window.location.href = "wei_MenusList.aspx?menuFid=" + fid + "&da=" + new Date();
                    }
                    else {
                        alert("系统正忙，请稍后重试！");
                    }
                })
            }
        }

        function editMenus(o) {
            var id = o.id;
            var fid = $("#fid").val();
            window.location.href = "Wei_MenusEdit.aspx?menuFid=" + fid + "&id=" + id + "&da=" + new Date();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
      <p style="background-image: url(images/topbg.jpg); height: 35px; width: 100%">
          <asp:Button ID="Button1" runat="server" Text="清空缓存" CssClass="btn blue" 
              onclick="Button1_Click" />&nbsp;
           <asp:Button ID="Button2" runat="server" Text="更新菜单" CssClass="btn blue" OnClick="Button2_Click" 
               />
    </p>
        <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
            <thead>
                <tr style="background-image: url(images/trbg.jpg); height: 30px; line-height: 30px">
                    <td>
                        序号
                    </td>
                    <td>
                        菜单名称
                    </td>
                    <td>
                        菜单类型
                    </td>
                    <td>
                        菜单取值
                    </td>
                    <td>
                        父亲菜单
                    </td>
                    <td>
                        事件类型
                    </td>
                    <td>
                        添加时间
                    </td>
                    <td>
                        系统操作
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Container.ItemIndex+1 %>
                            </td>
                            <td>
                                <%#Eval("WX_menuName")%>
                            </td>
                            <td>
                                <%#GetMenusType(Eval("WX_MenuType"))%>
                            </td>
                            <td>
                                <a href="<%#Eval("WX_MenusKey_URL")%>" style="color: Black; text-decoration: blink;">
                                    <%#cutstr(Eval("WX_MenusKey_URL").ToString(),50)%></a>
                            </td>
                            <td>
                                <%#GetFatherMenus(Eval("WX_Fid"))%>
                            </td>
                            <td>
                                <%#GetMsgType(Eval("Flat1")) %>
                            </td>
                            <td>
                                <%#Eval("WX_AddTime", "{0:d}")%>
                            </td>
                            <td>
                                <a href="JavaScript:void(0)" id="del<%#Eval("Id")%>" style="color: Blue" onclick="del(this)">
                                    [删除]</a> <a href="JavaScript:void(0)" id="<%#Eval("Id")%>" style="color: Blue" onclick="editMenus(this)">
                                        [编辑]</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr style="height: 25px">
                    <td colspan="22" class="tds" id="pageList">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="15" HorizontalAlign="right"
                            Width="100%" Style="font-size: 15px;" AlwaysShow="true" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PrevPageText="上一页" SubmitButtonText="Go" SubmitButtonClass="submitBtn"
                            CustomInfoStyle="font-size:14px;text-align:left;" InputBoxStyle="width:25px; border:1px solid #999999; text-align:center; "
                            TextBeforeInputBox="转到第" TextAfterInputBox="页 " pageindexboxtype="TextBox" showpageindexbox="Always"
                            textafterpageindexbox="页" textbeforepageindexbox="转到" Font-Size="15px" ShowCustomInfoSection="Left"
                            CustomInfoSectionWidth="28.1%" PagingButtonSpacing="3px" CustomInfoHTML="<div style='white-space: nowrap;'>共<font color='#ff0000'>%PageCount%</font>页，第<font color='#ff0000'>%CurrentPageIndex%</font>页，共<font color='#ff0000'>%RecordCount%</font>条数据</div>"
                            OnPageChanged="AspNetPager1_PageChanged">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </tfoot>
        </table>
        <div style="height: 25px;">
        </div>
    </fieldset>
</asp:Content>
