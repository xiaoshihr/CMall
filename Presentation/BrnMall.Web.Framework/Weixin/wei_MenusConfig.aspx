<%@ Page Title="" Language="C#" MasterPageFile="sysSite.Master" AutoEventWireup="true"
    CodeBehind="wei_MenusConfig.aspx.cs" Inherits="qiaojiaren.Manger.wei_MenusConfig" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function EditEvent(o) {
            var id = o.id;
            var fid = $("#fid").val();
            $.get("wei_MenusConfig.aspx?action=checkMenu&id=" + id + "&da=" + new Date(), function (data) {
                if (data == "error") {
                    swal("系统正忙，请稍后重试！");
                    return;
                }
                else if (data == "NO") {
                    swal("非CLICK类型的菜单不拥有添加事件！");
                    return;
                }
                else {
                    $.get("wei_MenusConfig.aspx?action=cekevent2&id=" + id + "&da=" + new Date(), function (data) {
                        if (data == "NO") {//没有配置微事件  前去配置
                            window.location.href = "wei_Event.aspx?id=" + id + "&menuFid=" + fid + "";
                            return;
                        }
                        else {
                            window.location.href = "wei_EventEdit.aspx?id=" + id + "&menuFid=" + fid + "";
                        }
                    })

                }
            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
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
                        消息类型
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
                                <a href="JavaScript:void(0)" id="<%#Eval("Id")%>" style="color: Blue" onclick="EditEvent(this)">
                                    [配置微菜单]</a> 
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr style="height: 25px">
                    <td colspan="22" class="tds" id="pageList">
                        <webdiyer:aspnetpager id="AspNetPager1" runat="server" pagesize="15" horizontalalign="right"
                            width="100%" style="font-size: 15px;" alwaysshow="true" firstpagetext="首页" lastpagetext="尾页"
                            nextpagetext="下一页" prevpagetext="上一页" submitbuttontext="Go" submitbuttonclass="submitBtn"
                            custominfostyle="font-size:14px;text-align:left;" inputboxstyle="width:25px; border:1px solid #999999; text-align:center; "
                            textbeforeinputbox="转到第" textafterinputbox="页 " pageindexboxtype="TextBox" showpageindexbox="Always"
                            textafterpageindexbox="页" textbeforepageindexbox="转到" font-size="15px" showcustominfosection="Left"
                            custominfosectionwidth="28.1%" pagingbuttonspacing="3px" custominfohtml="<div style='white-space: nowrap;'>共<font color='#ff0000'>%PageCount%</font>页，第<font color='#ff0000'>%CurrentPageIndex%</font>页，共<font color='#ff0000'>%RecordCount%</font>条数据</div>"
                            onpagechanged="AspNetPager1_PageChanged">
                        </webdiyer:aspnetpager>
                    </td>
                </tr>
            </tfoot>
        </table>
        <div style="height: 25px;">
        </div>
    </fieldset>
</asp:Content>
