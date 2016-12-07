<%@ Page Title="" Language="C#" MasterPageFile="sysSite.Master" AutoEventWireup="true"
    CodeBehind="wei_MsgAdd.aspx.cs" Inherits="qiaojiaren.Manger.wei_MsgAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .spans {
            font-size: small;
        }

      
    </style>
    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Addmsgs() {
            var EventName = $("#EventName").val();
            var msgType = $("#msgType").val();
            var KeyWorld = $("#KeyWorld").val();
            if (EventName == "0") {
                swal("事件类型不能为空！");
                return false;
            }
            else if (msgType == "0") {
                swal("消息类型不能为空！");
                return false;
            }
            else if (EventName == "keyword") {
                if (KeyWorld.trim() == "") {
                    swal("当事件类型为关键词时：关键词不能为空！");
                    $("#KeyWorld").focus();
                    return false;
                }
                else {
                    $("#action").val('action');
                }
            }
            else {
                $("#action").val('action');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <table>
            <tr>
                <td >
                    <span class="spans">事件类型：</span>
                </td>
                <td>
                    <select id="EventName" name="EventName" class="txb">
                        <option value="0">--请选择--</option>
                        <option value="subscribe">关注事件</option>
                        <option value="SCAN">已关注事件</option>
                        <option value="keyword">关键词事件</option>
                        <option value="image">图片事件</option>
                        <option value="voice">语音事件</option>
                        <option value="location">地理位置事件</option>
                      
                    </select>&nbsp;&nbsp;<span class="help-inline">注：目前基本事件的回复仅支持文本和图文</span>
                </td>
            </tr>
            <tr>
                <td >
                    <span class="spans">消息类型：</span>
                </td>
                <td>
                    <select id="msgType" name="msgType" class="txb">
                        <option value="0">--请选择--</option>
                        <option value="1">发送文本</option>
                        <option value="2">发送图文</option>
                       
                    </select>&nbsp;&nbsp;<span class="help-inline">注：目前基本事件的回复仅支持文本和图文</span>
                </td>
            </tr>
            <tr>
                <td >
                    <span class="spans">关键词值：</span>
                </td>
                <td>
                    <input id="KeyWorld" name="KeyWorld" type="text" style="width:205px;"/>&nbsp;&nbsp;<span class="help-inline">注：事件类型为：‘关键词事件’时：必须填写！</span>
                </td>
            </tr>
        </table>
        <div class="form-actions clearfix" style="margin-left: 50px;">
            <a href="javascript:;" class="btn button-previous" onclick="window.history.go(-1)"><i
                class="m-icon-swapleft"></i>Back </a>
            <input id="Submit2" type="submit" value="submit" class="btn green button-submit"
                onclick="return Addmsgs()" /><i class="m-icon-swapright m-icon-white"></i>
        </div>
        
        <div style="height: 25px;">
        </div>
    </fieldset>
</asp:Content>
