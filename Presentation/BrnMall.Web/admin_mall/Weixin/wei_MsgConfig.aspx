<%@ Page Title="" Language="C#" MasterPageFile="~/Manger/sysSite.Master" AutoEventWireup="true"
    CodeBehind="wei_MsgConfig.aspx.cs" Inherits="qiaojiaren.Manger.wei_MsgConfig" %>
    <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function del(o) {
            if (confirm("确定要删除这条数据吗？")) {
                var id = o.id;
                $.get("wei_MsgConfig.aspx?action=del&id=" + id + "&da=" + new Date(), function (data) {
                    if (data == "OK") {
                        swal("删除成功！");
                        $(o).parent().parent().remove();
                    }
                    else {
                        swal("系统正忙，请稍后重试！");
                        return;
                    }
                })
            }
        }

        function addEvent(o) {
            var id = o.id;
            id = id.substr(3);
            var msgtype = o.title;
            var fid = $("#fid").val();
            window.location.href = "wei_xinEventMsg.aspx?id=" + id + "&msgtype=" + msgtype + "&menuFid=" + fid + "&da=" + new Date();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <table  class="table table-striped table-hover table-bordered" id="sample_editable_1">
            <thead>
                <tr style="background-image: url(images/trbg.jpg); height: 30px; line-height: 30px">
                    <td>
                        序 号
                    </td>
                    <td>
                        事件类型
                    </td>
                    <td>
                        消息类型
                    </td>
                    <td>
                        关键词
                    </td>
                    <td>
                        消息配置
                    </td>
                     <td>
                        消息类型
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
                                <%#GetEventType(Eval("EventName").ToString())%>
                            </td>
                            <td>
                                <%#GetMsgType(Eval("msgType").ToString())%>
                            </td>
                            <td>
                                <%#Eval("KeyWorld")%>
                            </td>
                            <td>
                                <%#Eval("flat1").ToString()=="0"?"未配置":"已配置"%>
                            </td>
                            <td>
                                <%#GetMsgType(Eval("msgType"))%>
                            </td>
                            <td>
                                <a href="JavaScript:void(0)" id="<%#Eval("Id")%>" onclick="del(this)">[删 除]</a>&nbsp;&nbsp;
                                <a href="JavaScript:void(0)" id="add<%#Eval("Id")%>" title="<%#Eval("msgType")%>"
                                    onclick="addEvent(this)">[配置消息]</a>
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
