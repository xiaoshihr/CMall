<%@ Page Title="" Language="C#" MasterPageFile="sysSite.Master" AutoEventWireup="true" CodeBehind="wei_EventEdit.aspx.cs" Inherits="qiaojiaren.Manger.wei_EventEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/administration/Manger/js/jquery-1.7.2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            var eventType = $("#hideventType").val();
            $(".fieldset").css("display", "none");
            if (eventType == "1") {//text类型事件
                $("#fld_text").css("display", "block");
                $("#msgtype").val('0');
                var Ary = document.getElementById("newsType").options;
                for (var i = 0; i < Ary.length; i++) {
                    if (Ary[i].value == "0") {
                        Ary[i].selected = 'selected';
                    }
                }

            }
            if (eventType == "2") {//news类型事件
                $("#fld_news").css("display", "block");
                $("#msgtype").val('1');
                var Ary = document.getElementById("newsType").options;
                for (var i = 0; i < Ary.length; i++) {
                    if (Ary[i].value == "1") {
                        Ary[i].selected = 'selected';
                    }
                }
            }
        });

        function coik(o) {
            var Ary = document.getElementById("newsType").options;
            var vle = "";
            for (var i = 0; i < Ary.length; i++) {
                if (Ary[i].selected) {
                    vle = Ary[i].value;
                }
            }
            if (vle == "0") {
                $(".fieldset").css("display", "none");
                $("#fld_text").css("display", "block");
                $("#msgtype").val('0');
            }
            if (vle == "1") {
                $(".fieldset").css("display", "none");
                $("#fld_news").css("display", "block");
                $("#msgtype").val('1');
            }
        }

        function AddText() {
            var msgContent = $("#msgContent").val();
            if (msgContent.trim() == "") {
                swal("请输入需要发送的文本内容！");
                return false;
            }
            else {
                $("#action").val('text');
            }
        }

        function AddNews() {
            var newsTitle = $("#newsTitle").val();
            var newsDescription = $("#newsDescription").val();
            var newsPicUrl = $("#newsPicUrl").val();
            var newsUrl = $("#newsUrl").val();
            if (newsTitle.trim() == "") {
                swal("请输入图文标题！");
                return false;
            }
            else if (newsDescription.trim() == "") {
                swal("请输入图文描述！");
                return false;
            }
            else if (newsPicUrl.trim() == "") {
                swal("请输入图片地址！");
                return false;
            }
            else if (newsUrl.trim() == "") {
                swal("请输入图文链接！");
                return false;
            }
            else {
                $("#action").val('news');
            }
        }

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <input id="EventId" name="EventId" value="<%=Request.QueryString["id"] %>" type="hidden" />
    <input id="hideventType" name="hideventType" type="hidden" value="<%=eventType %>" />
    <input id="msgtype" name="msgtype" type="hidden" />
    <div style="height: 50px; line-height: 50px;">
        消息类型：<select id="newsType" name="newsType" onchange="coik(this)">
            <option value="0">文本消息</option>
            <option value="1">图文消息</option>
        </select>
        <span class="help-inline">注：目前仅支持文本消息和图文消息</span>
    </div>
    <fieldset id="fld_text" class="fieldset" style="display: block;">
        <table>
            <tr class="trrs">
                <td align="left">
                    <span>文本内容：</span>
                </td>
                <td>
                    <textarea id="msgContent" name="msgContent" cols="40" rows="6" style="font-size:small;"><%=textMol.msgContent %></textarea>&nbsp;&nbsp;<span
                        class="help-inline">注：文本消息输入/n代表换行！</span>
                </td>
            </tr>
        </table>
        <div class="form-actions clearfix" style="margin-left: 50px;">
            <a href="javascript:;" class="btn button-previous" onclick="window.history.go(-1)"><i
                class="m-icon-swapleft"></i>Back </a>
            <input id="Submit3" type="submit" value="submit" class="btn green button-submit"
                onclick="return AddText()" /><i class="m-icon-swapright m-icon-white"></i>
        </div>
    </fieldset>
    <fieldset id="fld_news" class="fieldset" style="display: none;">
        <table>
            <tr class="trrs">
                <td align="left">
                    <span>图文标题：</span>
                </td>
                <td>
                    <input id="newsTitle" name="newsTitle" type="text" class="txb" value="<%=newsMol.newsTitle %>" />
                </td>
            </tr>
            <tr class="trrs">
                <td align="left">
                    <span>图文描述：</span>
                </td>
                <td>
                    <textarea id="newsDescription" name="newsDescription" cols="40" rows="6" style="font-size:small;"><%=newsMol.newsDescription%></textarea>
                </td>
            </tr>
            <tr class="trrs">
                <td align="left">
                    <span>图片地址：</span>
                </td>
                <td>
                    <input id="newsPicUrl" name="newsPicUrl" type="text" class="txb" value="<%=newsMol.newsPicUrl %>" />
                </td>
            </tr>
            <tr class="trrs">
                <td align="left">
                    <span>图文链接：</span>
                </td>
                <td>
                     <input id="newsUrl" name="newsUrl" type="text" class="txb" value="<%=newsMol.newsUrl %>" />
                </td>
            </tr>
        </table>
        <div class="form-actions clearfix" style="margin-left: 50px;">
            <a href="javascript:;" class="btn button-previous" onclick="window.history.go(-1)"><i
                class="m-icon-swapleft"></i>Back </a>
            <input id="Submit1" type="submit" value="submit" class="btn green button-submit"
                onclick="return AddNews();" /><i class="m-icon-swapright m-icon-white"></i>
        </div>
        
    </fieldset>
</asp:Content>
